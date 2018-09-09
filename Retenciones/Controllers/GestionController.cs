using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Retenciones.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using OfficeOpenXml;
using System.IO;
using Retenciones.DAL;
using System.Collections.ObjectModel;

namespace Retenciones.Controllers
{
    [Authorize]
    public class GestionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Gestion
        public ActionResult Index(DateTime? fecha1, DateTime? fecha2)
        {
            var usuario = User.Identity.GetUserName();
            var gestion = db.Gestions.Include(g => g.MotivoFinal).Include(g => g.MotivoInicial).Include(g => g.SeDeriva).Include(g => g.UsuarioDeriva);

            if (fecha1 == null) { fecha1 = DateTime.Today; }
            if (fecha2 == null) { fecha2 = DateTime.Today; }

            if (User.IsInRole("Supervisor"))
            {
                var gestionx = gestion.Where(x => DbFunctions.TruncateTime(x.FechaGestion) >= fecha1 && DbFunctions.TruncateTime(x.FechaGestion) <= fecha2).OrderByDescending(x => x.FechaGestion);
                ViewBag.Fecha1 = fecha1.Value.ToString("dd/MM/yyyy");
                ViewBag.Fecha2 = fecha2.Value.ToString("dd/MM/yyyy");
                return View(gestionx.ToList());
            }
            else
            {
                GestionDAL gestiondal = new GestionDAL();
                DataTable dt = new DataTable();
                dt = gestiondal.Avance(usuario);

                if (dt.Rows.Count > 0)
                {
                    ViewBag.Avance = "Retenidos: " + dt.Rows[0][0].ToString() + " | Bajas: " + dt.Rows[0][1].ToString() + " | Herencias: " + dt.Rows[0][2].ToString() +
                                    " | Porcentaje: " + dt.Rows[0][3].ToString() + " | Valla: " + dt.Rows[0][4].ToString();
                }

                var gestionx = gestion.Where(x => x.Asesor == usuario && (DbFunctions.TruncateTime(x.FechaGestion) >= fecha1 && DbFunctions.TruncateTime(x.FechaGestion) <= fecha2)).OrderByDescending(x => x.FechaGestion);
                ViewBag.Fecha1 = fecha1.Value.ToString("dd/MM/yyyy");
                ViewBag.Fecha2 = fecha2.Value.ToString("dd/MM/yyyy");
                return View(gestionx.ToList());
            }  
        }       

        // GET: Gestion/Details
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gestion gestion = db.Gestions.Find(id);
            if (gestion == null)
            {
                return HttpNotFound();
            }
            return View(gestion);
        }

        // GET: Gestion/Create
        public ActionResult Create()
        {
            ViewBag.MotivoFinalId = new SelectList(new List<MotivoFinal>(), "Id", "Nombre");
            ViewBag.MotivoInicialId = new SelectList(db.MotivoInicials, "Id", "Nombre");
            ViewBag.SeDerivaId = new SelectList(db.SeDerivas, "Id", "Nombre");
            ViewBag.UsuarioDerivaId = new SelectList(new List<UsuarioDeriva>(), "Id", "Nombre");
            ViewBag.PromocionId = new SelectList(db.Promociones, "Id", "Nombre");
            ViewBag.OfrecimientoId = new SelectList(db.Ofrecimientos, "Id", "Nombre");

            ViewBag.AntiguedadOfre = new SelectList(db.Ofrecimiento2.Where(x => x.Campo == "AntiguedadOfre"), "Id", "Nombre");
            ViewBag.ProblemasTecOfre = new SelectList(db.Ofrecimiento2.Where(x => x.Campo == "ProblemasTecOfre"), "Id", "Nombre");
            ViewBag.ReportesFactOfre = new SelectList(db.Ofrecimiento2.Where(x => x.Campo == "ReportesFactOfre"), "Id", "Nombre");
            ViewBag.ServiciosOfre = new SelectList(db.Ofrecimiento2.Where(x => x.Campo == "ServiciosOfre"), "Id", "Nombre");
            ViewBag.DsctoRetencionesOfre = new SelectList(db.Ofrecimiento2.Where(x => x.Campo == "DsctoRetencionesOfre"), "Id", "Nombre");
            ViewBag.EstadoCuentaOfre = new SelectList(db.Ofrecimiento2.Where(x => x.Campo == "EstadoCuentaOfre"), "Id", "Nombre");

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var role = roleManager.FindByName("Supervisor").Users.First();

            ViewBag.Escoja = new SelectList(db.Users.Where(u => u.Roles.Select(r => r.RoleId).Contains(role.RoleId)), "UserName", "Nombres");

            return View();
        }

        // POST: Gestion/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Gestion gestion, string HiddenPromocionId, string HiddenOfrecimientoId)
        {
            if (ModelState.IsValid)
            {
                if (gestion.SOTMigracion != null)
                {
                    gestion.EstadoSOTMigracion = "EN EJECUCIÓN";
                }

                if (gestion.SOTTrasladoExternoSGA != null)
                {
                    gestion.EstadoSOTTrasladoExternoSGA = "EN EJECUCIÓN";
                }

                if (gestion.SOTVisitaTecnica != null)
                {
                    gestion.EstadoSOTVisitaTecnica = "EN EJECUCIÓN";
                }

                gestion.PromocionId = HiddenPromocionId;
                gestion.OfrecimientoId = HiddenOfrecimientoId;
                gestion.FechaGestion = DateTime.Now;
                gestion.Asesor = User.Identity.GetUserName();
                gestion.IP = Request.UserHostAddress;

                db.Gestions.Add(gestion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MotivoFinalId = new SelectList(new List<MotivoFinal>(), "Id", "Nombre", gestion.MotivoFinalId);
            ViewBag.MotivoInicialId = new SelectList(db.MotivoInicials, "Id", "Nombre", gestion.MotivoInicialId);
            ViewBag.SeDerivaId = new SelectList(db.SeDerivas, "Id", "Nombre", gestion.SeDerivaId);
            ViewBag.UsuarioDerivaId = new SelectList(new List<UsuarioDeriva>(), "Id", "Nombre", gestion.UsuarioDerivaId);
            ViewBag.PromocionId = new SelectList(db.Promociones, "Id", "Nombre", gestion.PromocionId);
            ViewBag.OfrecimientoId = new SelectList(db.Ofrecimientos, "Id", "Nombre",gestion.OfrecimientoId);

            ViewBag.AntiguedadOfre = new SelectList(db.Ofrecimiento2.Where(x => x.Campo == "AntiguedadOfre"), "Id", "Nombre",gestion.AntiguedadOfre);
            ViewBag.ProblemasTecOfre = new SelectList(db.Ofrecimiento2.Where(x => x.Campo == "ProblemasTecOfre"), "Id", "Nombre",gestion.ProblemasTecOfre);
            ViewBag.ReportesFactOfre = new SelectList(db.Ofrecimiento2.Where(x => x.Campo == "ReportesFactOfre"), "Id", "Nombre",gestion.ReportesFactOfre);
            ViewBag.ServiciosOfre = new SelectList(db.Ofrecimiento2.Where(x => x.Campo == "ServiciosOfre"), "Id", "Nombre",gestion.ServiciosOfre);
            ViewBag.DsctoRetencionesOfre = new SelectList(db.Ofrecimiento2.Where(x => x.Campo == "DsctoRetencionesOfre"), "Id", "Nombre",gestion.DsctoRetencionesOfre);
            ViewBag.EstadoCuentaOfre = new SelectList(db.Ofrecimiento2.Where(x => x.Campo == "EstadoCuentaOfre"), "Id", "Nombre",gestion.EstadoCuentaOfre);

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var role = roleManager.FindByName("Supervisor").Users.First();

            ViewBag.Escoja = new SelectList(db.Users.Where(u => u.Roles.Select(r => r.RoleId).Contains(role.RoleId)), "UserName", "Nombres",gestion.Escoja);
            return View(gestion);
        }

        // GET: Gestion/Dashboard
        [Authorize(Roles = "Supervisor")]
        public ActionResult Dashboard()
        {
            GestionDAL gestiondal = new GestionDAL();
            DataSet ds = new DataSet();
            ds = gestiondal.Dashboard();

            Response.AddHeader("Refresh", "60");

            return View(ds);
        }

        // GET: Gestion/Dashboard2
        [Authorize(Roles = "Supervisor")]
        public ActionResult Dashboard2()
        {
            return View();
        }

        // GET: Gestion/_PartialDashboard2
        public ActionResult GetPartialDashboard2(string FechaInicio, string FechaFin)
        {
            DateTime f1 = DateTime.ParseExact(FechaInicio, "dd/MM/yyyy", null);
            DateTime f2 = DateTime.ParseExact(FechaFin, "dd/MM/yyyy", null);

            GestionDAL gestiondal = new GestionDAL();
            DataSet ds = new DataSet();
            ds = gestiondal.Dashboard2(f1,f2);

            return PartialView("_PartialDashboard2", ds);
        }

        // GET: Gestion/_PartialPromociones
        public ActionResult GetClienteTablas(string cod_cli)
        {
            GestionDAL gestiondal = new GestionDAL();
            DataSet ds = new DataSet();
            ds = gestiondal.GetClienteTablas(cod_cli);

            return PartialView("_PartialPromociones", ds);
        }

        // GET: Gestion/_PartialEdificios
        public ActionResult GetListaEdificios(string Direccion, string Distrito)
        {
            var lista = db.Edificio.AsNoTracking().Where(x=>x.Direccion.Contains(Direccion) && x.Distrito.Contains(Distrito)).ToList();
            return PartialView("_PartialEdificios", lista);
        }

        // GET: Gestion/Import
        [Authorize(Roles = "Supervisor")]
        public ActionResult Import()
        {
            return View();
        }

        // POST: Gestion/Import
        [HttpPost]
        public ActionResult Import(FormCollection formCollection, string TipoBase)
        {
            string CurrentRow = "";

            try
            {
                HttpPostedFileBase file = Request.Files["UploadedFile"];
                if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
                {
                    string fileName = file.FileName;
                    string fileContentType = file.ContentType;
                    byte[] fileBytes = new byte[file.ContentLength];
                    var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                    using (var package = new ExcelPackage(file.InputStream))
                    {
                        var currentSheet = package.Workbook.Worksheets;
                        var workSheet = currentSheet.FirstOrDefault(f => f.View.TabSelected); //import active sheet
                        var noOfCol = workSheet.Dimension.End.Column;
                        var noOfRow = workSheet.Dimension.End.Row;

                        var basecreada = new Base();
                        basecreada.Nombre = "base_" + TipoBase + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");
                        basecreada.FechaImportacion = DateTime.Now;
                        basecreada.Tipo = TipoBase;
                        basecreada.Estado = "ACTIVO";
                        db.Base.Add(basecreada);
                        db.SaveChanges();

                        int rowImportados = 0;

                        if (TipoBase == "AbonadosActivos")
                        {
                            List<AbonadosActivos> listaabonadosactivos = new List<AbonadosActivos>();

                            for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                            {
                                CurrentRow = rowIterator.ToString();

                                rowImportados++;
                                var abonadosactivos = new AbonadosActivos();
                                abonadosactivos.CustomerID = workSheet.Cells[rowIterator, 2].Value == null ? string.Empty : workSheet.Cells[rowIterator, 2].Value.ToString();
                                abonadosactivos.CodigoCliente = workSheet.Cells[rowIterator, 3].Value == null ? string.Empty : workSheet.Cells[rowIterator, 3].Value.ToString();
                                abonadosactivos.NumSLC = workSheet.Cells[rowIterator, 5].Value == null ? string.Empty : workSheet.Cells[rowIterator, 5].Value.ToString();
                                abonadosactivos.FechaInicio = workSheet.Cells[rowIterator, 6].Value == null ? abonadosactivos.FechaInicio = null : DateTime.Parse(workSheet.Cells[rowIterator, 6].Text);
                                abonadosactivos.BaseId = basecreada.Id;
                                listaabonadosactivos.Add(abonadosactivos);
                            }

                            if (listaabonadosactivos.Count() > 10000)
                            {
                                //INSERT BY PARTS
                                var partitionlist = listaabonadosactivos.Select((x, i) => new { Index = i, Value = x }).GroupBy(x => x.Index / 10000).Select(x => x.Select(v => v.Value).ToList()).ToList();

                                foreach (var part in partitionlist)
                                {
                                    db.AbonadosActivos.AddRange(part);
                                    db.SaveChanges();
                                }
                            }

                            else
                            {
                                db.AbonadosActivos.AddRange(listaabonadosactivos);
                                db.SaveChanges();
                            }

                            int total = noOfRow - 1;
                            ViewBag.Analizados = total.ToString();
                            ViewBag.Importados = rowImportados.ToString();
                        }

                        else if (TipoBase == "NcSGA")
                        {
                            List<NcSGA> listancsga = new List<NcSGA>();

                            for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                            {
                                CurrentRow = rowIterator.ToString();

                                rowImportados++;
                                var ncsga = new NcSGA();

                                ncsga.CodigoCliente = workSheet.Cells[rowIterator, 1].Value == null ? string.Empty : workSheet.Cells[rowIterator, 1].Value.ToString();
                                ncsga.Documento = workSheet.Cells[rowIterator, 5].Value == null ? string.Empty : workSheet.Cells[rowIterator, 5].Value.ToString();
                                ncsga.FechaEmision = workSheet.Cells[rowIterator, 6].Value == null ? ncsga.FechaEmision = null : DateTime.Parse(workSheet.Cells[rowIterator, 6].Text);
                                ncsga.SubTotal = workSheet.Cells[rowIterator, 11].Value == null ? string.Empty : workSheet.Cells[rowIterator, 11].Value.ToString();
                                ncsga.AreaResponsable = workSheet.Cells[rowIterator, 19].Value == null ? string.Empty : workSheet.Cells[rowIterator, 19].Value.ToString();
                                ncsga.NroIncidencia = workSheet.Cells[rowIterator, 27].Value == null ? string.Empty : workSheet.Cells[rowIterator, 27].Value.ToString();
                                ncsga.UsuarioEmisor = workSheet.Cells[rowIterator, 28].Value == null ? string.Empty : workSheet.Cells[rowIterator, 28].Value.ToString();
                                ncsga.BaseId = basecreada.Id;
                                listancsga.Add(ncsga);
                            }

                            db.NcSGA.AddRange(listancsga);
                            db.SaveChanges();

                            int total = noOfRow - 1;
                            ViewBag.Analizados = total.ToString();
                            ViewBag.Importados = rowImportados.ToString();
                        }

                        else if (TipoBase == "NcSIAC")
                        {
                            List<NcSIAC> listancsiac = new List<NcSIAC>();

                            for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                            {
                                CurrentRow = rowIterator.ToString();

                                rowImportados++;
                                var ncsiac = new NcSIAC();

                                ncsiac.UsuarioGenerador = workSheet.Cells[rowIterator, 1].Value == null ? string.Empty : workSheet.Cells[rowIterator, 1].Value.ToString();
                                ncsiac.TipoRegistro = workSheet.Cells[rowIterator, 18].Value == null ? string.Empty : workSheet.Cells[rowIterator, 18].Value.ToString();
                                ncsiac.FechaEmision = workSheet.Cells[rowIterator, 20].Value == null ? ncsiac.FechaEmision = null : DateTime.Parse(workSheet.Cells[rowIterator, 20].Text);
                                ncsiac.MontoImputado = workSheet.Cells[rowIterator, 25].Value == null ? string.Empty : workSheet.Cells[rowIterator, 25].Value.ToString();
                                ncsiac.DocRef = workSheet.Cells[rowIterator, 30].Value == null ? string.Empty : workSheet.Cells[rowIterator, 30].Value.ToString();
                                ncsiac.CodigoSGA = workSheet.Cells[rowIterator, 33].Value == null ? string.Empty : workSheet.Cells[rowIterator, 33].Value.ToString();
                                ncsiac.BaseId = basecreada.Id;
                                listancsiac.Add(ncsiac);
                            }

                            db.NcSIAC.AddRange(listancsiac);
                            db.SaveChanges();

                            int total = noOfRow - 1;
                            ViewBag.Analizados = total.ToString();
                            ViewBag.Importados = rowImportados.ToString();
                        }

                        else if (TipoBase == "OccSIAC")
                        {
                            List<OccSIAC> listaoccsiac = new List<OccSIAC>();

                            for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                            {
                                if ((workSheet.Cells[rowIterator, 12].Value == null ? string.Empty : workSheet.Cells[rowIterator, 12].Value.ToString()) == "Ajuste (-)")
                                {
                                    CurrentRow = rowIterator.ToString();

                                    rowImportados++;
                                    var occsiac = new OccSIAC();

                                    occsiac.Interaccion = workSheet.Cells[rowIterator, 2].Value == null ? string.Empty : workSheet.Cells[rowIterator, 2].Value.ToString();
                                    occsiac.FechaRegistro = workSheet.Cells[rowIterator, 3].Value == null ? occsiac.FechaRegistro = null : DateTime.Parse(workSheet.Cells[rowIterator, 3].Text);
                                    occsiac.UsuarioRegistro = workSheet.Cells[rowIterator, 7].Value == null ? string.Empty : workSheet.Cells[rowIterator, 7].Value.ToString();
                                    occsiac.Concepto = workSheet.Cells[rowIterator, 13].Value == null ? string.Empty : workSheet.Cells[rowIterator, 13].Value.ToString();
                                    occsiac.MontoAjusteSinIGV = workSheet.Cells[rowIterator, 14].Value == null ? string.Empty : workSheet.Cells[rowIterator, 14].Value.ToString();
                                    occsiac.CodigoSGA = workSheet.Cells[rowIterator, 23].Value == null ? string.Empty : workSheet.Cells[rowIterator, 23].Value.ToString();
                                    occsiac.BaseId = basecreada.Id;
                                    listaoccsiac.Add(occsiac);
                                }
                            }

                            db.OccSIAC.AddRange(listaoccsiac);
                            db.SaveChanges();

                            int total = noOfRow - 1;
                            ViewBag.Analizados = total.ToString();
                            ViewBag.Importados = rowImportados.ToString();
                        }

                        else if (TipoBase == "PromoSGA")
                        {
                            List<PromoSGA> listapromosga = new List<PromoSGA>();

                            for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                            {
                                if ((workSheet.Cells[rowIterator, 5].Value == null ? string.Empty : workSheet.Cells[rowIterator, 5].Value.ToString()) == "Acceso Dedicado a Internet" ||
                                    (workSheet.Cells[rowIterator, 5].Value == null ? string.Empty : workSheet.Cells[rowIterator, 5].Value.ToString()) == "Paquetes Masivos")
                                {
                                    CurrentRow = rowIterator.ToString();

                                    rowImportados++;
                                    var promosga = new PromoSGA();

                                    promosga.CodigoCliente = workSheet.Cells[rowIterator, 1].Value == null ? string.Empty : workSheet.Cells[rowIterator, 1].Value.ToString();
                                    promosga.FechaRegistro = workSheet.Cells[rowIterator, 3].Value == null ? promosga.FechaRegistro = null : DateTime.Parse(workSheet.Cells[rowIterator, 3].Text);
                                    promosga.Descripcion = workSheet.Cells[rowIterator, 7].Value == null ? string.Empty : workSheet.Cells[rowIterator, 7].Value.ToString();
                                    promosga.Promocion = workSheet.Cells[rowIterator, 11].Value == null ? string.Empty : workSheet.Cells[rowIterator, 11].Value.ToString();
                                    promosga.Usuario = workSheet.Cells[rowIterator, 17].Value == null ? string.Empty : workSheet.Cells[rowIterator, 17].Value.ToString();                                    promosga.BaseId = basecreada.Id;
                                    listapromosga.Add(promosga);
                                }
                            }

                            db.PromoSGA.AddRange(listapromosga);
                            db.SaveChanges();

                            int total = noOfRow - 1;
                            ViewBag.Analizados = total.ToString();
                            ViewBag.Importados = rowImportados.ToString();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                ViewBag.Error = "Verificar la fila " + CurrentRow + ". Mensaje de Error: " + e.Message;
            }
            return View();
        }

        // GET: Gestion/ImportEdificio
        [Authorize(Roles = "Supervisor")]
        public ActionResult ImportEdificio()
        {
            return View();
        }

        // POST: ImportEdificio
        [HttpPost]
        public ActionResult ImportEdificio(FormCollection formCollection)
        {
            try
            {
                HttpPostedFileBase file = Request.Files["UploadedFile"];
                if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
                {
                    string fileName = file.FileName;
                    string fileContentType = file.ContentType;
                    byte[] fileBytes = new byte[file.ContentLength];
                    var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));

                    using (var package = new ExcelPackage(file.InputStream))
                    {
                        var currentSheet = package.Workbook.Worksheets;
                        var workSheet = currentSheet.First();

                        DataTable dt = new DataTable();

                        dt.Rows.Clear();
                        dt.Columns.Clear();

                        var hasHeader = true;

                        foreach (var firstRowCell in workSheet.Cells[1, 1, 1, workSheet.Dimension.End.Column])
                        {
                            dt.Columns.Add(hasHeader ? firstRowCell.Text : string.Format("Column {0}", firstRowCell.Start.Column));
                        }
                        var startRow = hasHeader ? 2 : 1;
                        for (int rowNum = startRow; rowNum <= workSheet.Dimension.End.Row; rowNum++)
                        {
                            var wsRow = workSheet.Cells[rowNum, 1, rowNum, workSheet.Dimension.End.Column];
                            var row = dt.NewRow();
                            foreach (var cell in wsRow) row[cell.Start.Column - 1] = cell.Text;
                            dt.Rows.Add(row);
                        }

                        var basecreada = new Base();
                        basecreada.Nombre = "base_edificios_" + DateTime.Now.ToString("yyyyMMddHHmmss");
                        basecreada.FechaImportacion = DateTime.Now;
                        basecreada.Tipo = "EDIFICIO";
                        basecreada.Estado = "ACTIVO";
                        db.Base.Add(basecreada);
                        db.SaveChanges();

                        int importado = 0;

                        foreach (DataRow row in dt.Rows)
                        {
                            Edificio edificio = new Edificio();
                            edificio.Direccion = row.ItemArray[1].ToString();
                            edificio.Nodo = row.ItemArray[2].ToString();
                            edificio.EdificioCodigo = row.ItemArray[3].ToString();
                            edificio.Distrito = row.ItemArray[4].ToString();

                            try
                            {
                                edificio.FechaActivacion = DateTime.ParseExact(row.ItemArray[5].ToString(), "dd/MM/yy", System.Globalization.CultureInfo.InvariantCulture);
                            }
                            catch (Exception)
                            {
                                edificio.FechaActivacion = null;
                            }

                            edificio.BaseId = basecreada.Id;

                            GestionDAL gestionDAL = new GestionDAL();
                            int resultado = gestionDAL.ImportEdificio(edificio);

                            if (resultado == -1)
                            {
                                continue;
                            }

                            importado++;
                        }

                        ViewBag.Importados = "Se analizaron " + dt.Rows.Count.ToString() + " registros , de los cuales se importaron " + importado.ToString() + ".";
                    }
                }
            }
            catch (Exception e)
            {
                ViewBag.Error = "Error: " + e.Message;
            }
            return View();
        }

        // GET: Gestion/Exportar
        [Authorize(Roles = "Supervisor")]
        public ActionResult Export()
        {
            return View();
        }

        // POST: Gestion/Exportar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Export(DateTime FechaInicio, DateTime FechaFin)
        {
            ExcelPackage package = new ExcelPackage();
            var ws = package.Workbook.Worksheets.Add("Index");

            GestionDAL gestiondal = new GestionDAL();
            DataTable dt = new DataTable();
            dt = gestiondal.ExportarRetenciones(FechaInicio, FechaFin);

            ws.Cells["A1"].LoadFromDataTable(dt, true);

            var memoryStream = new MemoryStream();
            package.SaveAs(memoryStream);

            string fileName = "reporte_retenciones.xlsx";
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            memoryStream.Position = 0;
            return File(memoryStream, contentType, fileName);
        }

        //AJAX
        public IList<MotivoFinal> ListMotivoFinal(int id)
        {
            return db.MotivoFinals.Where(m => m.MotivoInicialId == id).ToList();
        }

        public JsonResult GetJsonMotivoFinal(int? id)
        {
            if (id == null)
            {
                id = 0;
            }
            var stateListt = this.ListMotivoFinal(Convert.ToInt32(id));
            var statesList = stateListt.Select(m => new SelectListItem()
            {
                Text = m.Nombre,
                Value = m.Id.ToString()
            });

            return Json(statesList, JsonRequestBehavior.AllowGet);
        }

        public IList<UsuarioDeriva> ListUsuarioDeriva(int id)
        {
            if (id == 3)
            {
                id = 2;
            }
            if (id == 4)
            {
                id = 1;
            }
            return db.UsuarioDerivas.Where(m => m.MotivoInicialId == id).ToList();
        }

        public JsonResult GetJsonUsuarioDeriva(int? id)
        {
            if (id == null)
            {
                id = 0;
            }
            var stateListt = this.ListUsuarioDeriva(Convert.ToInt32(id));
            var statesList = stateListt.Select(m => new SelectListItem()
            {
                Text = m.Nombre,
                Value = m.Id.ToString()
            });

            return Json(statesList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetClienteInfo(string cod_cli)
        {
            var cliente = db.AbonadosActivos.Where(x => x.CodigoCliente == cod_cli).FirstOrDefault();

            if (cliente == null)
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }

            else
            {
                return Json(cliente, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetOfrecimiento2(int id)
        {
            var ofrecimiento = db.Ofrecimiento2.Where(x => x.Id == id).FirstOrDefault();

            if (ofrecimiento == null)
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }

            else
            {
                return Json(ofrecimiento, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Gestion/CreateNew //EN PRUEBA
        public ActionResult CreateNew()
        {
            ViewBag.MotivoFinalId = new SelectList(new List<MotivoFinal>(), "Id", "Nombre");
            ViewBag.MotivoInicialId = new SelectList(db.MotivoInicials, "Id", "Nombre");
            ViewBag.SeDerivaId = new SelectList(db.SeDerivas, "Id", "Nombre");
            ViewBag.UsuarioDerivaId = new SelectList(new List<UsuarioDeriva>(), "Id", "Nombre");

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var role = roleManager.FindByName("Supervisor").Users.First();

            ViewBag.Escoja = new SelectList(db.Users.Where(u => u.Roles.Select(r => r.RoleId).Contains(role.RoleId)), "UserName", "Nombres");

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
