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
using Retenciones.DAL;
using System.IO;
using System.Globalization;

namespace Retenciones.Controllers
{
    [Authorize]
    public class GestionCarruselesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: GestionCarruseles
        public ActionResult Index(string Gestionados)
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var usuario = User.Identity.GetUserName();
            var gestion = db.GestionCarruseles;

            if (Gestionados != null)
            {
                if (UserManager.IsInRole(User.Identity.GetUserId(), "Supervisor"))
                {
                    var gestionx = gestion.Where(x => x.FechaGestion != null).OrderByDescending(x => x.FechaGestion);
                    ViewBag.Gestionados = gestion.Where(x => x.FechaGestion != null).Count();
                    ViewBag.NoGestionados = gestion.Where(x => x.FechaGestion == null).Count();
                    ViewBag.CountCarruseles = gestion.Where(x => x.FechaGestion != null && x.Carrusel == true).Count();
                    ViewBag.CountNoCarruseles = gestion.Where(x => x.FechaGestion != null && x.Carrusel == false).Count();
                    return View(gestionx.ToList());
                }
                else
                {
                    var gestionx = gestion.Where(x => x.FechaGestion != null && x.Asesor==usuario).OrderByDescending(x => x.FechaGestion);
                    ViewBag.Gestionados = gestion.Where(x => x.FechaGestion != null).Count();
                    ViewBag.NoGestionados = gestion.Where(x => x.FechaGestion == null).Count();
                    ViewBag.CountCarruseles = gestion.Where(x => x.FechaGestion != null && x.Carrusel == true).Count();
                    ViewBag.CountNoCarruseles = gestion.Where(x => x.FechaGestion != null && x.Carrusel == false).Count();
                    return View(gestionx.ToList());
                }

            }
            else
            {
                if (UserManager.IsInRole(User.Identity.GetUserId(), "Supervisor"))
                {
                    var gestionx = gestion.AsEnumerable().Where(x => x.FechaGestion == null || x.Pendiente == true).OrderBy(x => DateTime.Parse(x.FechaGeneracion));
                    ViewBag.Gestionados = gestion.Where(x => x.FechaGestion != null).Count();
                    ViewBag.NoGestionados = gestion.Where(x => x.FechaGestion == null).Count();
                    ViewBag.CountCarruseles = gestion.Where(x => x.FechaGestion != null && x.Carrusel == true).Count();
                    ViewBag.CountNoCarruseles = gestion.Where(x => x.FechaGestion != null && x.Carrusel == false).Count();
                    return View(gestionx.ToList());
                }
                else
                {
                    var gestionx = gestion.AsEnumerable().Where(x => x.FechaGestion == null || x.Pendiente == true).OrderBy(x => DateTime.Parse(x.FechaGeneracion));
                    ViewBag.Gestionados = gestion.Where(x => x.FechaGestion != null).Count();
                    ViewBag.NoGestionados = gestion.Where(x => x.FechaGestion == null).Count();
                    ViewBag.CountCarruseles = gestion.Where(x => x.FechaGestion != null && x.Carrusel == true).Count();
                    ViewBag.CountNoCarruseles = gestion.Where(x => x.FechaGestion != null && x.Carrusel == false).Count();
                    return View(gestionx.ToList());
                }

            }
        }

        // GET: GestionCarruseles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GestionCarruseles gestionCarruseles = db.GestionCarruseles.Find(id);
            if (gestionCarruseles == null)
            {
                return HttpNotFound();
            }
            return View(gestionCarruseles);
        }

        // GET: Import
        [Authorize(Roles = "Supervisor")]
        public ActionResult Import()
        {
            return View();
        }

        // POST: Import
        [HttpPost]
        public ActionResult Import(FormCollection formCollection)
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
                    var gestioncarruselesList = new List<GestionCarruseles>();

                    using (var package = new ExcelPackage(file.InputStream))
                    {
                        var currentSheet = package.Workbook.Worksheets;
                        var workSheet = currentSheet.First();
                        var noOfCol = workSheet.Dimension.End.Column;
                        var noOfRow = workSheet.Dimension.End.Row;

                        var basecreada = new Base();
                        basecreada.Nombre = "base_carruseles_" + DateTime.Now.ToString("yyyyMMddHHmmss");
                        basecreada.FechaImportacion = DateTime.Now;
                        basecreada.Tipo = "CARRUSEL";
                        basecreada.Estado = "ACTIVO";
                        db.Base.Add(basecreada);
                        db.SaveChanges();

                        //int rowFiltro = 0;
                        //int rowDuplicados = 0;
                        //int rowImportados = 0;

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

                        //for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                        //{
                        //    if ((workSheet.Cells[rowIterator, 12].Value == null ? string.Empty : workSheet.Cells[rowIterator, 12].Value.ToString()).Contains("SOLICITUD") && (workSheet.Cells[rowIterator, 13].Value == null ? string.Empty : workSheet.Cells[rowIterator, 13].Value.ToString()) == "Paquetes Masivos")
                        //    {
                        //        string sotbaja = workSheet.Cells[rowIterator, 7].Value == null ? string.Empty : workSheet.Cells[rowIterator, 7].Value.ToString();
                        //        int countcliente = db.GestionCarruseles.Where(x => x.SOTBaja == sotbaja).Count();

                        //        if (countcliente == 0)
                        //        {
                        //            rowImportados++;
                        //            var gestioncarruseles = new GestionCarruseles();
                        //            gestioncarruseles.ProyectoAntiguo = workSheet.Cells[rowIterator, 1].Value == null ? string.Empty : workSheet.Cells[rowIterator, 1].Value.ToString();
                        //            gestioncarruseles.CodigoSGAAntiguo = workSheet.Cells[rowIterator, 4].Value == null ? string.Empty : workSheet.Cells[rowIterator, 4].Value.ToString();
                        //            gestioncarruseles.CodigoSGANuevo = workSheet.Cells[rowIterator, 4].Value == null ? string.Empty : workSheet.Cells[rowIterator, 4].Value.ToString();
                        //            gestioncarruseles.ClienteAntiguo = workSheet.Cells[rowIterator, 5].Value == null ? string.Empty : workSheet.Cells[rowIterator, 5].Value.ToString();
                        //            gestioncarruseles.ClienteNuevo = workSheet.Cells[rowIterator, 5].Value == null ? string.Empty : workSheet.Cells[rowIterator, 5].Value.ToString();
                        //            gestioncarruseles.SOTBaja = workSheet.Cells[rowIterator, 7].Value == null ? string.Empty : workSheet.Cells[rowIterator, 7].Value.ToString();
                        //            gestioncarruseles.FechaGeneracion = workSheet.Cells[rowIterator, 9].Value == null ? string.Empty : workSheet.Cells[rowIterator, 9].Value.ToString();
                        //            gestioncarruseles.DireccionAntiguo = workSheet.Cells[rowIterator, 24].Value == null ? string.Empty : workSheet.Cells[rowIterator, 24].Value.ToString();
                        //            gestioncarruseles.DistritoAntiguo = workSheet.Cells[rowIterator, 38].Value == null ? string.Empty : workSheet.Cells[rowIterator, 38].Value.ToString();
                        //            gestioncarruseles.DistritoNuevo = workSheet.Cells[rowIterator, 38].Value == null ? string.Empty : workSheet.Cells[rowIterator, 38].Value.ToString();
                        //            gestioncarruseles.DepartamentoAntiguo = workSheet.Cells[rowIterator, 40].Value == null ? string.Empty : workSheet.Cells[rowIterator, 40].Value.ToString();
                        //            gestioncarruseles.DepartamentoNuevo = workSheet.Cells[rowIterator, 40].Value == null ? string.Empty : workSheet.Cells[rowIterator, 40].Value.ToString();
                        //            gestioncarruseles.Pendiente = false;
                        //            gestioncarruseles.Carrusel = false;
                        //            gestioncarruseles.BaseId = basecreada.Id;
                        //            listGestionCarruseles.Add(gestioncarruseles);
                        //        }
                        //        else
                        //        {
                        //            rowDuplicados++;
                        //        }
                        //    }
                        //    else
                        //    {
                        //        rowFiltro++;
                        //    }
                        //}

                        List<GestionCarruseles> listGestionCarruseles = new List<GestionCarruseles>();

                        foreach (DataRow row in dt.Rows)
                        {
                            if ((row.ItemArray[11].ToString().Contains("SOLICITUD")) && (row.ItemArray[12].ToString() == "Paquetes Masivos"))
                            {
                                //string sotbaja = row.ItemArray[6].ToString();
                                //int countcliente = db.GestionCarruseles.Where(x => x.SOTBaja == sotbaja).Count();

                                //if (countcliente == 0)
                                //{
                                    GestionCarruseles gestionCarruseles = new GestionCarruseles();
                                    gestionCarruseles.ProyectoAntiguo = row.ItemArray[0].ToString();
                                    gestionCarruseles.CodigoSGAAntiguo = row.ItemArray[3].ToString();
                                    gestionCarruseles.CodigoSGANuevo = row.ItemArray[3].ToString();
                                    gestionCarruseles.ClienteAntiguo = row.ItemArray[4].ToString();
                                    gestionCarruseles.ClienteNuevo = row.ItemArray[4].ToString();
                                    gestionCarruseles.SOTBaja = row.ItemArray[6].ToString();
                                    gestionCarruseles.FechaGeneracion = row.ItemArray[8].ToString();
                                    gestionCarruseles.DireccionAntiguo = row.ItemArray[23].ToString();
                                    gestionCarruseles.DistritoAntiguo = row.ItemArray[37].ToString();
                                    gestionCarruseles.DistritoNuevo = row.ItemArray[37].ToString();
                                    gestionCarruseles.DepartamentoAntiguo = row.ItemArray[39].ToString();
                                    gestionCarruseles.DepartamentoNuevo = row.ItemArray[39].ToString();
                                    gestionCarruseles.Pendiente = false;
                                    gestionCarruseles.Carrusel = false;
                                    gestionCarruseles.BaseId = basecreada.Id;
                                    listGestionCarruseles.Add(gestionCarruseles);
                                
                            }
                        }

                        GestionCarruselesDAL gestionCarruselesDAL = new GestionCarruselesDAL();
                        int resultado = gestionCarruselesDAL.ImportCliente(listGestionCarruseles);

                        if (resultado == -1)
                        {
                            throw new Exception("Error al importar a bd.");
                        }

                        //int total = noOfRow - 1;
                        //ViewBag.Analizados = total.ToString();
                        //ViewBag.Importados = rowImportados.ToString();
                        //ViewBag.Descartados = rowFiltro.ToString();
                        //ViewBag.Duplicados = rowDuplicados.ToString();
                    }
                }
            }
            catch (Exception e)
            {
                ViewBag.Error = "Error: " + e.Message;
            }            
            return View();
        }

        // GET: Exportar
        [Authorize(Roles = "Supervisor")]
        public ActionResult Export()
        {
            return View();
        }

        // POST: Exportar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Export(DateTime FechaInicio, DateTime FechaFin)
        {
            ExcelPackage package = new ExcelPackage();
            var ws = package.Workbook.Worksheets.Add("Index");

            GestionCarruselesDAL gestioncarruselesdal = new GestionCarruselesDAL();
            DataTable dt = new DataTable();
            dt = gestioncarruselesdal.ExportarCarruseles(FechaInicio, FechaFin);

            ws.Cells["A1"].LoadFromDataTable(dt, true);

            var memoryStream = new MemoryStream();
            package.SaveAs(memoryStream);

            string fileName = "reporte_carruseles.xlsx";
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            memoryStream.Position = 0;
            return File(memoryStream, contentType, fileName);
        }

        //// GET: GestionCarruseles/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: GestionCarruseles/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,CodigoSGAAntiguo,ClienteAntiguo,DireccionAntiguo,DistritoAntiguo,DepartamentoAntiguo,ProyectoAntiguo,TipoAplicativoAntiguo,IncidenciaAntiguo,InteraccionAntiguo,FechaInstalacionNuevo,CodigoSGANuevo,ClienteNuevo,SOTAltaNuevo,DireccionNuevo,DistritoNuevo,DepartamentoNuevo,ProyectoNuevo,ParentescoNuevo,Observacion,Pendiente,Asesor,IP,FechaGestion,BaseId")] GestionCarruseles gestionCarruseles)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.GestionCarruseles.Add(gestionCarruseles);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(gestionCarruseles);
        //}

        // GET: GestionCarruseles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GestionCarruseles gestionCarruseles = db.GestionCarruseles.Find(id);
            if (gestionCarruseles == null)
            {
                return HttpNotFound();
            }
            return View(gestionCarruseles);
        }

        // POST: GestionCarruseles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FechaGeneracion,SOTBaja,CodigoSGAAntiguo,ClienteAntiguo,DireccionAntiguo,DistritoAntiguo,DepartamentoAntiguo,ProyectoAntiguo,TipoAplicativoAntiguo,IncidenciaAntiguo,InteraccionAntiguo,FechaInstalacionNuevo,CodigoSGANuevo,ClienteNuevo,SOTAltaNuevo,DireccionNuevo,DistritoNuevo,DepartamentoNuevo,ProyectoNuevo,ParentescoNuevo,Observacion,Pendiente,Asesor,IP,FechaGestion,BaseId,FechaModificacion,Carrusel")] GestionCarruseles gestionCarruseles)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(gestionCarruseles).State = EntityState.Modified;

                var entry = db.Entry(gestionCarruseles);
                entry.State = EntityState.Modified;

                entry.Property(e => e.FechaGeneracion).IsModified = false;
                entry.Property(e => e.SOTBaja).IsModified = false;
                entry.Property(e => e.CodigoSGAAntiguo).IsModified = false;
                entry.Property(e => e.ClienteAntiguo).IsModified = false;
                entry.Property(e => e.DireccionAntiguo).IsModified = false;
                entry.Property(e => e.DistritoAntiguo).IsModified = false;
                entry.Property(e => e.DepartamentoAntiguo).IsModified = false;
                entry.Property(e => e.ProyectoAntiguo).IsModified = false;
                entry.Property(e => e.BaseId).IsModified = false;

                if (gestionCarruseles.FechaGestion == null)
                {
                    gestionCarruseles.FechaGestion = DateTime.Now;
                    gestionCarruseles.FechaModificacion = DateTime.Now;
                    gestionCarruseles.Asesor = User.Identity.GetUserName();
                    gestionCarruseles.IP = Request.UserHostAddress;
                }

                else
                {
                    gestionCarruseles.FechaModificacion = DateTime.Now;
                    entry.Property(e => e.FechaGestion).IsModified = false;
                    entry.Property(e => e.Asesor).IsModified = false;
                    entry.Property(e => e.IP).IsModified = false;
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gestionCarruseles);
        }

        // GET: GestionCarruseles/Delete/5
        [Authorize(Roles = "Supervisor")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GestionCarruseles gestionCarruseles = db.GestionCarruseles.Find(id);
            if (gestionCarruseles == null)
            {
                return HttpNotFound();
            }
            return View(gestionCarruseles);
        }

        // POST: GestionCarruseles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GestionCarruseles gestionCarruseles = db.GestionCarruseles.Find(id);
            db.GestionCarruseles.Remove(gestionCarruseles);
            db.SaveChanges();
            return RedirectToAction("Index");
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
