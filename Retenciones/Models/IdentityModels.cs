using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Retenciones.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            userIdentity.AddClaim(new Claim("Fullname", Nombres.ToString()));
            userIdentity.AddClaim(new Claim("UsuarioE", UsuarioE.ToString()));
            return userIdentity;
        }

        [Required]
        [StringLength(200)]
        [Display(Name = "Nombres y Apellidos")]
        public string Nombres { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Usuario E")]
        public string UsuarioE { get; set; }

        [Required]
        [Display(Name = "Estado")]
        public bool Estado { get; set; }

    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Configure Code First to ignore PluralizingTableName convention 
            // If you keep this convention then the generated tables will have pluralized names. 
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<IdentityRole>().ToTable("AspNetRole");
            modelBuilder.Entity<IdentityUserRole>().ToTable("AspNetUserRole");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("AspNetUserLogin");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("AspNetUserClaim");
            modelBuilder.Entity<ApplicationUser>().ToTable("AspNetUser");
        }

        public DbSet<GestionCarruseles> GestionCarruseles { get; set; }

        public DbSet<Base> Base { get; set; }

        public System.Data.Entity.DbSet<Retenciones.Models.Gestion> Gestions { get; set; }

        public System.Data.Entity.DbSet<Retenciones.Models.MotivoFinal> MotivoFinals { get; set; }

        public System.Data.Entity.DbSet<Retenciones.Models.MotivoInicial> MotivoInicials { get; set; }

        public System.Data.Entity.DbSet<Retenciones.Models.SeDeriva> SeDerivas { get; set; }

        public System.Data.Entity.DbSet<Retenciones.Models.UsuarioDeriva> UsuarioDerivas { get; set; }

        public System.Data.Entity.DbSet<Retenciones.Models.Promocion> Promociones { get; set; }

        public System.Data.Entity.DbSet<Retenciones.Models.Ofrecimiento> Ofrecimientos { get; set; }

        public System.Data.Entity.DbSet<Retenciones.Models.AbonadosActivos> AbonadosActivos { get; set; }

        public System.Data.Entity.DbSet<Retenciones.Models.NcSGA> NcSGA { get; set; }

        public System.Data.Entity.DbSet<Retenciones.Models.NcSIAC> NcSIAC { get; set; }

        public System.Data.Entity.DbSet<Retenciones.Models.OccSIAC> OccSIAC { get; set; }

        public System.Data.Entity.DbSet<Retenciones.Models.PromoSGA> PromoSGA { get; set; }

        public System.Data.Entity.DbSet<Retenciones.Models.Ofrecimiento2> Ofrecimiento2 { get; set; }

        public System.Data.Entity.DbSet<Retenciones.Models.Edificio> Edificio { get; set; }
    }
}