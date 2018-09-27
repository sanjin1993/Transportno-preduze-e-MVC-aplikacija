using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using TransportnoPreduzece.Data.Models;

namespace TransportnoPreduzece.Data.DAL
{
    public class TPContext : DbContext
    {


        public TPContext()
            : base("TPConnectionString")
        {
            Database.SetInitializer<TPContext>(new CreateDatabaseIfNotExists<TPContext>());
        }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Vozac>().ToTable("Vozac");


            modelBuilder.Entity<VozacKategorije>().HasKey(x => new { x.VozacId, x.VozackaKategorijaId });



        }

        public DbSet<Akcija> Akcije { get; set; }
        public DbSet<BenzinskaPumpa> BenzinskePumpe { get; set; }
        public DbSet<Dispozicija> Dispozicije { get; set; }
        public DbSet<Dobavljac> Dobavljaci { get; set; }
        public DbSet<Drzava> Drzave { get; set; }
        public DbSet<Instradacija> Instradacije { get; set; }
        public DbSet<Kartica> Kartice { get; set; }
        public DbSet<KarticaVozac> KarticaVozaci { get; set; }
        public DbSet<KarticaZaposlenik> KarticaZaposlenici { get; set; }
        public DbSet<Klijent> Klijenti { get; set; }
        public DbSet<KolicinaTip> KolicinaTipovi { get; set; }
        public DbSet<Nabavka> Nabavke { get; set; }
        public DbSet<NabavkaStavka> StavkeNabavke { get; set; }
        public DbSet<Odrzavanje> Odrzavanja { get; set; }
        public DbSet<Odsustvo> Odsustva { get; set; }
        public DbSet<PrikljucnoVozilo> PrikljucnaVozila { get; set; }
        public DbSet<StatusInstradacije> StatusInstradacije { get; set; }
        public DbSet<StatusVozaca> StatusiVozaca { get; set; }
        public DbSet<StatusVozila> StatusiVozila { get; set; }
        public DbSet<Stavka> Stavke { get; set; }
        public DbSet<TipAkcije> TipoviAkcije { get; set; }
        public DbSet<TipOdrzavanja> TipoviOdrzavanja { get; set; }
        public DbSet<TipOdsustva> TipoviOdsustva { get; set; }
        public DbSet<TipPrikljucnog> TipoviPrikljucnog { get; set; }
        public DbSet<TipTroska> TipoviTroskova { get; set; }
        public DbSet<TipVozila> TipoviVozila { get; set; }
        public DbSet<Trosak> Troskovi { get; set; }
        public DbSet<Uloga> Uloge { get; set; }
        public DbSet<Vozac> Vozaci { get; set; }
        public DbSet<VozackaKategorija> VozackeKategorije { get; set; }
        public DbSet<VozacKategorije> VozacKategorije { get; set; }
        public DbSet<Vozilo> Vozila { get; set; }
        public DbSet<Zaposlenik> Zaposlenici { get; set; }



    }
}