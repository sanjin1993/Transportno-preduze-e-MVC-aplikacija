namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Akcija",
                c => new
                    {
                        AkcijaId = c.Int(nullable: false, identity: true),
                        Adresa = c.String(),
                        Vrijeme = c.DateTime(nullable: false),
                        DispozicijaId = c.Int(nullable: false),
                        TipAkcijeId = c.Int(nullable: false),
                        DrzavaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AkcijaId)
                .ForeignKey("dbo.Drzava", t => t.DrzavaId)
                .ForeignKey("dbo.Dispozicija", t => t.DispozicijaId)
                .ForeignKey("dbo.TipAkcije", t => t.TipAkcijeId)
                .Index(t => t.DispozicijaId)
                .Index(t => t.TipAkcijeId)
                .Index(t => t.DrzavaId);
            
            CreateTable(
                "dbo.Dispozicija",
                c => new
                    {
                        DispozicijaId = c.Int(nullable: false, identity: true),
                        RowGuid = c.String(),
                        Cijena = c.Double(nullable: false),
                        DatumPlacanja = c.DateTime(nullable: false),
                        DatumDispozicije = c.DateTime(nullable: false),
                        DatumIspostave = c.DateTime(nullable: false),
                        Primalac = c.String(),
                        DodatneInformacije = c.String(),
                        DrzavaOdId = c.Int(nullable: false),
                        DrzavaDoId = c.Int(nullable: false),
                        AdresaOd = c.String(),
                        AdresaDo = c.String(),
                        KlijentId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.DispozicijaId)
                .ForeignKey("dbo.Klijent", t => t.KlijentId)
                .ForeignKey("dbo.Drzava", t => t.DrzavaDoId)
                .ForeignKey("dbo.Drzava", t => t.DrzavaOdId)
                .Index(t => t.DrzavaOdId)
                .Index(t => t.DrzavaDoId)
                .Index(t => t.KlijentId);
            
            CreateTable(
                "dbo.Drzava",
                c => new
                    {
                        DrzavaId = c.Int(nullable: false, identity: true),
                        Naziv = c.String(),
                        Kod = c.String(),
                    })
                .PrimaryKey(t => t.DrzavaId);
            
            CreateTable(
                "dbo.Klijent",
                c => new
                    {
                        KlijentId = c.Int(nullable: false, identity: true),
                        Naziv = c.String(),
                        Mail = c.String(),
                        Telefon = c.String(),
                        Adresa = c.String(),
                        IdBroj = c.String(),
                        Fax = c.String(),
                        DrzavaId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.KlijentId)
                .ForeignKey("dbo.Drzava", t => t.DrzavaId)
                .Index(t => t.DrzavaId);
            
            CreateTable(
                "dbo.Instradacija",
                c => new
                    {
                        InstradacijaId = c.Int(nullable: false, identity: true),
                        UlaznaCarinarnica = c.Int(),
                        IzlaznaCarinarnica = c.Int(),
                        Datum = c.DateTime(nullable: false),
                        VoziloId = c.Int(nullable: false),
                        VozacId = c.Int(nullable: false),
                        PrikljucnoVoziloId = c.Int(nullable: false),
                        DispozicijaId = c.Int(nullable: false),
                        StatusInstradacijeId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.InstradacijaId)
                .ForeignKey("dbo.Dispozicija", t => t.DispozicijaId)
                .ForeignKey("dbo.PrikljucnoVozilo", t => t.PrikljucnoVoziloId)
                .ForeignKey("dbo.Vozilo", t => t.VoziloId)
                .ForeignKey("dbo.StatusInstradacije", t => t.StatusInstradacijeId)
                .ForeignKey("dbo.Vozac", t => t.VozacId)
                .Index(t => t.VoziloId)
                .Index(t => t.VozacId)
                .Index(t => t.PrikljucnoVoziloId)
                .Index(t => t.DispozicijaId)
                .Index(t => t.StatusInstradacijeId);
            
            CreateTable(
                "dbo.PrikljucnoVozilo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BrojSasije = c.String(),
                        BrojOsovina = c.Int(nullable: false),
                        RegistarskeOznake = c.String(),
                        Nosivost = c.Int(nullable: false),
                        Cijena = c.Double(nullable: false),
                        Visina = c.Single(nullable: false),
                        Duzina = c.Single(nullable: false),
                        Tezina = c.Int(nullable: false),
                        DatumRegistracije = c.DateTime(nullable: false),
                        TipPrikljucnogId = c.Int(nullable: false),
                        StatusVozilaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StatusVozila", t => t.StatusVozilaId)
                .ForeignKey("dbo.TipPrikljucnog", t => t.TipPrikljucnogId)
                .Index(t => t.TipPrikljucnogId)
                .Index(t => t.StatusVozilaId);
            
            CreateTable(
                "dbo.Odrzavanje",
                c => new
                    {
                        OdrzavanjeId = c.Int(nullable: false, identity: true),
                        Datum = c.DateTime(nullable: false),
                        Kilometraza = c.Int(nullable: false),
                        Troskovi = c.Double(nullable: false),
                        Detaljno = c.String(),
                        VoziloId = c.Int(),
                        TipOdrzavanjaId = c.Int(nullable: false),
                        PrikljucnoVoziloId = c.Int(),
                    })
                .PrimaryKey(t => t.OdrzavanjeId)
                .ForeignKey("dbo.PrikljucnoVozilo", t => t.PrikljucnoVoziloId)
                .ForeignKey("dbo.TipOdrzavanja", t => t.TipOdrzavanjaId)
                .ForeignKey("dbo.Vozilo", t => t.VoziloId)
                .Index(t => t.VoziloId)
                .Index(t => t.TipOdrzavanjaId)
                .Index(t => t.PrikljucnoVoziloId);
            
            CreateTable(
                "dbo.TipOdrzavanja",
                c => new
                    {
                        TipOdrzavanjaId = c.Int(nullable: false, identity: true),
                        Naziv = c.String(),
                    })
                .PrimaryKey(t => t.TipOdrzavanjaId);
            
            CreateTable(
                "dbo.Vozilo",
                c => new
                    {
                        VoziloId = c.Int(nullable: false, identity: true),
                        BrojSasije = c.String(),
                        RegistarskeOznake = c.String(),
                        Proizvodzac = c.String(),
                        Model = c.String(),
                        Nosivost = c.Int(nullable: false),
                        Cijena = c.Double(nullable: false),
                        Kilometraza = c.Int(nullable: false),
                        DatumRegistracije = c.DateTime(nullable: false),
                        GodinaProizvodnje = c.Int(nullable: false),
                        TipVozilaId = c.Int(nullable: false),
                        StatusVozilaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.VoziloId)
                .ForeignKey("dbo.StatusVozila", t => t.StatusVozilaId)
                .ForeignKey("dbo.TipVozila", t => t.TipVozilaId)
                .Index(t => t.TipVozilaId)
                .Index(t => t.StatusVozilaId);
            
            CreateTable(
                "dbo.StatusVozila",
                c => new
                    {
                        StatusVozilaId = c.Int(nullable: false, identity: true),
                        Naziv = c.String(),
                    })
                .PrimaryKey(t => t.StatusVozilaId);
            
            CreateTable(
                "dbo.TipVozila",
                c => new
                    {
                        TipVozilaId = c.Int(nullable: false, identity: true),
                        Naziv = c.String(),
                    })
                .PrimaryKey(t => t.TipVozilaId);
            
            CreateTable(
                "dbo.TipPrikljucnog",
                c => new
                    {
                        TipPrikljucnogId = c.Int(nullable: false, identity: true),
                        Naziv = c.String(),
                    })
                .PrimaryKey(t => t.TipPrikljucnogId);
            
            CreateTable(
                "dbo.StatusInstradacije",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Naziv = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Trosak",
                c => new
                    {
                        TrosakId = c.Int(nullable: false, identity: true),
                        Ukupno = c.Double(nullable: false),
                        TipTroskaId = c.Int(nullable: false),
                        InstradacijaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TrosakId)
                .ForeignKey("dbo.Instradacija", t => t.InstradacijaId)
                .ForeignKey("dbo.TipTroska", t => t.TipTroskaId)
                .Index(t => t.TipTroskaId)
                .Index(t => t.InstradacijaId);
            
            CreateTable(
                "dbo.TipTroska",
                c => new
                    {
                        TipTroskaId = c.Int(nullable: false, identity: true),
                        Naziv = c.String(),
                    })
                .PrimaryKey(t => t.TipTroskaId);
            
            CreateTable(
                "dbo.Zaposlenik",
                c => new
                    {
                        ZaposlenikId = c.Int(nullable: false, identity: true),
                        Ime = c.String(),
                        Prezime = c.String(),
                        JMBG = c.String(),
                        DatumZaposlenja = c.DateTime(nullable: false),
                        DatumOtkaza = c.DateTime(),
                        Adresa = c.String(),
                        Telefon = c.String(),
                        Email = c.String(),
                        UlogaId = c.Int(nullable: false),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.ZaposlenikId)
                .ForeignKey("dbo.Uloga", t => t.UlogaId)
                .Index(t => t.UlogaId);
            
            CreateTable(
                "dbo.KarticaVozac",
                c => new
                    {
                        KarticaVozacId = c.Int(nullable: false, identity: true),
                        DatumKoristenja = c.DateTime(nullable: false),
                        KolicinaLitara = c.Double(nullable: false),
                        UkupanIznos = c.Double(nullable: false),
                        VozacId = c.Int(nullable: false),
                        BenzinskaPumpaId = c.Int(nullable: false),
                        Vozac_ZaposlenikId = c.Int(),
                    })
                .PrimaryKey(t => t.KarticaVozacId)
                .ForeignKey("dbo.BenzinskaPumpa", t => t.BenzinskaPumpaId)
                .ForeignKey("dbo.Vozac", t => t.Vozac_ZaposlenikId)
                .Index(t => t.BenzinskaPumpaId)
                .Index(t => t.Vozac_ZaposlenikId);
            
            CreateTable(
                "dbo.BenzinskaPumpa",
                c => new
                    {
                        BenzinskaPumpaId = c.Int(nullable: false, identity: true),
                        Adresa = c.String(),
                    })
                .PrimaryKey(t => t.BenzinskaPumpaId);
            
            CreateTable(
                "dbo.KarticaZaposlenik",
                c => new
                    {
                        KarticaZaposlenikId = c.Int(nullable: false, identity: true),
                        Iznos = c.Single(nullable: false),
                        Datum = c.DateTime(nullable: false),
                        ZaposlenikId = c.Int(nullable: false),
                        KarticaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.KarticaZaposlenikId)
                .ForeignKey("dbo.Kartica", t => t.KarticaId)
                .ForeignKey("dbo.Zaposlenik", t => t.ZaposlenikId)
                .Index(t => t.ZaposlenikId)
                .Index(t => t.KarticaId);
            
            CreateTable(
                "dbo.Kartica",
                c => new
                    {
                        KarticaId = c.Int(nullable: false, identity: true),
                        TrenutniIznos = c.Single(nullable: false),
                        Aktivna = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.KarticaId);
            
            CreateTable(
                "dbo.Odsustvo",
                c => new
                    {
                        OdsustvoId = c.Int(nullable: false, identity: true),
                        DatumOd = c.DateTime(nullable: false),
                        DatumDo = c.DateTime(nullable: false),
                        TipOdsustvaId = c.Int(nullable: false),
                        ZaposlenikId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OdsustvoId)
                .ForeignKey("dbo.TipOdsustva", t => t.TipOdsustvaId)
                .ForeignKey("dbo.Zaposlenik", t => t.ZaposlenikId)
                .Index(t => t.TipOdsustvaId)
                .Index(t => t.ZaposlenikId);
            
            CreateTable(
                "dbo.TipOdsustva",
                c => new
                    {
                        TipOdsustvaId = c.Int(nullable: false, identity: true),
                        Naziv = c.String(),
                    })
                .PrimaryKey(t => t.TipOdsustvaId);
            
            CreateTable(
                "dbo.Uloga",
                c => new
                    {
                        UlogaId = c.Int(nullable: false, identity: true),
                        Naziv = c.String(),
                    })
                .PrimaryKey(t => t.UlogaId);
            
            CreateTable(
                "dbo.StatusVozaca",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Naziv = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.VozacKategorije",
                c => new
                    {
                        VozacId = c.Int(nullable: false),
                        VozackaKategorijaId = c.Int(nullable: false),
                        Vozac_ZaposlenikId = c.Int(),
                    })
                .PrimaryKey(t => new { t.VozacId, t.VozackaKategorijaId })
                .ForeignKey("dbo.Vozac", t => t.Vozac_ZaposlenikId)
                .ForeignKey("dbo.VozackaKategorija", t => t.VozackaKategorijaId)
                .Index(t => t.VozackaKategorijaId)
                .Index(t => t.Vozac_ZaposlenikId);
            
            CreateTable(
                "dbo.VozackaKategorija",
                c => new
                    {
                        VozackaKategorijaId = c.Int(nullable: false, identity: true),
                        Naziv = c.String(),
                    })
                .PrimaryKey(t => t.VozackaKategorijaId);
            
            CreateTable(
                "dbo.Stavka",
                c => new
                    {
                        StavkaId = c.Int(nullable: false, identity: true),
                        Naziv = c.String(),
                        Kolicina = c.Int(nullable: false),
                        DispozicijaId = c.Int(nullable: false),
                        KolicinaTipId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StavkaId)
                .ForeignKey("dbo.Dispozicija", t => t.DispozicijaId)
                .ForeignKey("dbo.KolicinaTip", t => t.KolicinaTipId)
                .Index(t => t.DispozicijaId)
                .Index(t => t.KolicinaTipId);
            
            CreateTable(
                "dbo.KolicinaTip",
                c => new
                    {
                        KolicinaTipId = c.Int(nullable: false, identity: true),
                        Naziv = c.String(),
                    })
                .PrimaryKey(t => t.KolicinaTipId);
            
            CreateTable(
                "dbo.TipAkcije",
                c => new
                    {
                        TipAkcijeId = c.Int(nullable: false, identity: true),
                        Naziv = c.String(),
                    })
                .PrimaryKey(t => t.TipAkcijeId);
            
            CreateTable(
                "dbo.Dobavljac",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Naziv = c.String(),
                        Adresa = c.String(),
                        Telefon = c.String(),
                        ZaposlenikId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Zaposlenik", t => t.ZaposlenikId)
                .Index(t => t.ZaposlenikId);
            
            CreateTable(
                "dbo.Nabavka",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Datum = c.DateTime(nullable: false),
                        DobavljacId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Dobavljac", t => t.DobavljacId)
                .Index(t => t.DobavljacId);
            
            CreateTable(
                "dbo.NabavkaStavka",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Naziv = c.Int(nullable: false),
                        Cijena = c.Single(nullable: false),
                        NabavkaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Nabavka", t => t.NabavkaId)
                .Index(t => t.NabavkaId);
            
            CreateTable(
                "dbo.Vozac",
                c => new
                    {
                        ZaposlenikId = c.Int(nullable: false),
                        BrojVozacke = c.String(),
                        RokVazenjaDozvole = c.DateTime(nullable: false),
                        ADRLicenca = c.Boolean(nullable: false),
                        DatumLjekarskog = c.DateTime(nullable: false),
                        StatusVozacaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ZaposlenikId)
                .ForeignKey("dbo.Zaposlenik", t => t.ZaposlenikId)
                .ForeignKey("dbo.StatusVozaca", t => t.StatusVozacaId)
                .Index(t => t.ZaposlenikId)
                .Index(t => t.StatusVozacaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Vozac", "StatusVozacaId", "dbo.StatusVozaca");
            DropForeignKey("dbo.Vozac", "ZaposlenikId", "dbo.Zaposlenik");
            DropForeignKey("dbo.Dobavljac", "ZaposlenikId", "dbo.Zaposlenik");
            DropForeignKey("dbo.NabavkaStavka", "NabavkaId", "dbo.Nabavka");
            DropForeignKey("dbo.Nabavka", "DobavljacId", "dbo.Dobavljac");
            DropForeignKey("dbo.Akcija", "TipAkcijeId", "dbo.TipAkcije");
            DropForeignKey("dbo.Akcija", "DispozicijaId", "dbo.Dispozicija");
            DropForeignKey("dbo.Stavka", "KolicinaTipId", "dbo.KolicinaTip");
            DropForeignKey("dbo.Stavka", "DispozicijaId", "dbo.Dispozicija");
            DropForeignKey("dbo.VozacKategorije", "VozackaKategorijaId", "dbo.VozackaKategorija");
            DropForeignKey("dbo.VozacKategorije", "Vozac_ZaposlenikId", "dbo.Vozac");
            DropForeignKey("dbo.Zaposlenik", "UlogaId", "dbo.Uloga");
            DropForeignKey("dbo.Odsustvo", "ZaposlenikId", "dbo.Zaposlenik");
            DropForeignKey("dbo.Odsustvo", "TipOdsustvaId", "dbo.TipOdsustva");
            DropForeignKey("dbo.KarticaZaposlenik", "ZaposlenikId", "dbo.Zaposlenik");
            DropForeignKey("dbo.KarticaZaposlenik", "KarticaId", "dbo.Kartica");
            DropForeignKey("dbo.KarticaVozac", "Vozac_ZaposlenikId", "dbo.Vozac");
            DropForeignKey("dbo.KarticaVozac", "BenzinskaPumpaId", "dbo.BenzinskaPumpa");
            DropForeignKey("dbo.Instradacija", "VozacId", "dbo.Vozac");
            DropForeignKey("dbo.Trosak", "TipTroskaId", "dbo.TipTroska");
            DropForeignKey("dbo.Trosak", "InstradacijaId", "dbo.Instradacija");
            DropForeignKey("dbo.Instradacija", "StatusInstradacijeId", "dbo.StatusInstradacije");
            DropForeignKey("dbo.PrikljucnoVozilo", "TipPrikljucnogId", "dbo.TipPrikljucnog");
            DropForeignKey("dbo.Vozilo", "TipVozilaId", "dbo.TipVozila");
            DropForeignKey("dbo.Vozilo", "StatusVozilaId", "dbo.StatusVozila");
            DropForeignKey("dbo.PrikljucnoVozilo", "StatusVozilaId", "dbo.StatusVozila");
            DropForeignKey("dbo.Odrzavanje", "VoziloId", "dbo.Vozilo");
            DropForeignKey("dbo.Instradacija", "VoziloId", "dbo.Vozilo");
            DropForeignKey("dbo.Odrzavanje", "TipOdrzavanjaId", "dbo.TipOdrzavanja");
            DropForeignKey("dbo.Odrzavanje", "PrikljucnoVoziloId", "dbo.PrikljucnoVozilo");
            DropForeignKey("dbo.Instradacija", "PrikljucnoVoziloId", "dbo.PrikljucnoVozilo");
            DropForeignKey("dbo.Instradacija", "DispozicijaId", "dbo.Dispozicija");
            DropForeignKey("dbo.Dispozicija", "DrzavaOdId", "dbo.Drzava");
            DropForeignKey("dbo.Dispozicija", "DrzavaDoId", "dbo.Drzava");
            DropForeignKey("dbo.Klijent", "DrzavaId", "dbo.Drzava");
            DropForeignKey("dbo.Dispozicija", "KlijentId", "dbo.Klijent");
            DropForeignKey("dbo.Akcija", "DrzavaId", "dbo.Drzava");
            DropIndex("dbo.Vozac", new[] { "StatusVozacaId" });
            DropIndex("dbo.Vozac", new[] { "ZaposlenikId" });
            DropIndex("dbo.NabavkaStavka", new[] { "NabavkaId" });
            DropIndex("dbo.Nabavka", new[] { "DobavljacId" });
            DropIndex("dbo.Dobavljac", new[] { "ZaposlenikId" });
            DropIndex("dbo.Stavka", new[] { "KolicinaTipId" });
            DropIndex("dbo.Stavka", new[] { "DispozicijaId" });
            DropIndex("dbo.VozacKategorije", new[] { "Vozac_ZaposlenikId" });
            DropIndex("dbo.VozacKategorije", new[] { "VozackaKategorijaId" });
            DropIndex("dbo.Odsustvo", new[] { "ZaposlenikId" });
            DropIndex("dbo.Odsustvo", new[] { "TipOdsustvaId" });
            DropIndex("dbo.KarticaZaposlenik", new[] { "KarticaId" });
            DropIndex("dbo.KarticaZaposlenik", new[] { "ZaposlenikId" });
            DropIndex("dbo.KarticaVozac", new[] { "Vozac_ZaposlenikId" });
            DropIndex("dbo.KarticaVozac", new[] { "BenzinskaPumpaId" });
            DropIndex("dbo.Zaposlenik", new[] { "UlogaId" });
            DropIndex("dbo.Trosak", new[] { "InstradacijaId" });
            DropIndex("dbo.Trosak", new[] { "TipTroskaId" });
            DropIndex("dbo.Vozilo", new[] { "StatusVozilaId" });
            DropIndex("dbo.Vozilo", new[] { "TipVozilaId" });
            DropIndex("dbo.Odrzavanje", new[] { "PrikljucnoVoziloId" });
            DropIndex("dbo.Odrzavanje", new[] { "TipOdrzavanjaId" });
            DropIndex("dbo.Odrzavanje", new[] { "VoziloId" });
            DropIndex("dbo.PrikljucnoVozilo", new[] { "StatusVozilaId" });
            DropIndex("dbo.PrikljucnoVozilo", new[] { "TipPrikljucnogId" });
            DropIndex("dbo.Instradacija", new[] { "StatusInstradacijeId" });
            DropIndex("dbo.Instradacija", new[] { "DispozicijaId" });
            DropIndex("dbo.Instradacija", new[] { "PrikljucnoVoziloId" });
            DropIndex("dbo.Instradacija", new[] { "VozacId" });
            DropIndex("dbo.Instradacija", new[] { "VoziloId" });
            DropIndex("dbo.Klijent", new[] { "DrzavaId" });
            DropIndex("dbo.Dispozicija", new[] { "KlijentId" });
            DropIndex("dbo.Dispozicija", new[] { "DrzavaDoId" });
            DropIndex("dbo.Dispozicija", new[] { "DrzavaOdId" });
            DropIndex("dbo.Akcija", new[] { "DrzavaId" });
            DropIndex("dbo.Akcija", new[] { "TipAkcijeId" });
            DropIndex("dbo.Akcija", new[] { "DispozicijaId" });
            DropTable("dbo.Vozac");
            DropTable("dbo.NabavkaStavka");
            DropTable("dbo.Nabavka");
            DropTable("dbo.Dobavljac");
            DropTable("dbo.TipAkcije");
            DropTable("dbo.KolicinaTip");
            DropTable("dbo.Stavka");
            DropTable("dbo.VozackaKategorija");
            DropTable("dbo.VozacKategorije");
            DropTable("dbo.StatusVozaca");
            DropTable("dbo.Uloga");
            DropTable("dbo.TipOdsustva");
            DropTable("dbo.Odsustvo");
            DropTable("dbo.Kartica");
            DropTable("dbo.KarticaZaposlenik");
            DropTable("dbo.BenzinskaPumpa");
            DropTable("dbo.KarticaVozac");
            DropTable("dbo.Zaposlenik");
            DropTable("dbo.TipTroska");
            DropTable("dbo.Trosak");
            DropTable("dbo.StatusInstradacije");
            DropTable("dbo.TipPrikljucnog");
            DropTable("dbo.TipVozila");
            DropTable("dbo.StatusVozila");
            DropTable("dbo.Vozilo");
            DropTable("dbo.TipOdrzavanja");
            DropTable("dbo.Odrzavanje");
            DropTable("dbo.PrikljucnoVozilo");
            DropTable("dbo.Instradacija");
            DropTable("dbo.Klijent");
            DropTable("dbo.Drzava");
            DropTable("dbo.Dispozicija");
            DropTable("dbo.Akcija");
        }
    }
}
