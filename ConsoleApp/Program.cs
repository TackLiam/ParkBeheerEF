using ParkBusinessLayer.Beheerders;
using ParkBusinessLayer.Model;
using ParkDataLayer.Model;
using ParkDataLayer.Repositories;
using System.Diagnostics.Contracts;

namespace ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionstring = "Data Source=MSI\\SQLEXPRESS;Initial Catalog=ParkBeheerDB;Integrated Security=True;TrustServerCertificate=True";
            ParkContext context = new ParkContext(connectionstring);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            HuizenRepositoryEF huizenRepo = new HuizenRepositoryEF(context);
            ContractenRepositoryEF contractRepo  = new ContractenRepositoryEF(context); 
            HuurderRepositoryEF huurderRepo = new HuurderRepositoryEF(context);

            BeheerHuizen huisBeheerder = new BeheerHuizen(huizenRepo);
            BeheerContracten contractBeheerder = new BeheerContracten(contractRepo);
            BeheerHuurders huurBeheerder = new BeheerHuurders(huurderRepo);


            Park p1 = new Park("JFLKHDG", "Center Parcs", "Erperheide");
            Park p2 = new Park("ABDE12EHL", "Sunparks", "Vossemeren");
            Park p3 = new Park("FJKHV248", "Center Parcs", "De Haan");

            huisBeheerder.VoegNieuwHuisToe("Vogelstraat", 333, p1);
            huisBeheerder.VoegNieuwHuisToe("Vogelstraat", 335, p1);
            huisBeheerder.VoegNieuwHuisToe("Vogelstraat", 337, p1);
            huisBeheerder.VoegNieuwHuisToe("Bloemkoolstraat", 15, p2);
            huisBeheerder.VoegNieuwHuisToe("Bloemkoolstraat", 17, p2);

            Huis h1 = new Huis(3,"Bloemkoolstraat", 17,true, p2);
            Huis h2 = new Huis(5, "Kippenstraat", 42, true, p3);

            huisBeheerder.UpdateHuis(h1);
            huisBeheerder.UpdateHuis(h2);
            huisBeheerder.ArchiveerHuis(h1);
            huisBeheerder.ArchiveerHuis(h2);

            Huis h3 = huisBeheerder.GeefHuis(1);
            Console.WriteLine($"HUIS\n   Straat:{h3.Straat}, Huisnummer: {h3.Nr}, Actief: {h3.Actief} \n   PARK: Naam: {h3.Park.Naam}, Locatie: {h3.Park.Locatie}");


            huurBeheerder.VoegNieuweHuurderToe("Jan Jaap", new Contactgegevens("janjaap@gmail.com", "091231234", "Gent"));
            huurBeheerder.VoegNieuweHuurderToe("Bob Boos", new Contactgegevens("bobboos@gmail.com", "096875367", "Brussel"));
            huurBeheerder.VoegNieuweHuurderToe(" Bas Baas", new Contactgegevens("basbaas@gmail.com", "049874698", "Mechelen"));
            Huurder hu1 = new Huurder(3,"Karel de Kerel", new Contactgegevens("kareldekerel@gmail.com", "048879878", "Antwerpen"));
            huurBeheerder.UpdateHuurder(hu1);

            Huurder hu2 = huurBeheerder.GeefHuurder(1);
            Console.WriteLine($"\nHUURDER ID (1):\n   Naam: {hu2.Naam}, E-mail: {hu2.Contactgegevens.Email}, Tel: {hu2.Contactgegevens.Tel}, Adres: {hu2.Contactgegevens.Adres}\n");
            List<Huurder> huurders = huurBeheerder.GeefHuurders("a");

            Console.WriteLine($"ALLE HUURDERS (a)");
            int counter = 1;
            foreach (Huurder hu in huurders)
            {
                Console.WriteLine($"   HUURDER {counter}:\n   Naam: {hu.Naam}, E-mail: {hu.Contactgegevens.Email}, Tel: {hu.Contactgegevens.Tel}, Adres: {hu.Contactgegevens.Adres}\n");
                counter++;
            }


            contractBeheerder.MaakContract("1HFKDF3", new Huurperiode(DateTime.Parse("2024-01-22").Date, 14), hu2, h3);
            Huurcontract c1 = contractBeheerder.GeefContract("1HFKDF3");
            contractBeheerder.AnnuleerContract(c1);

            Huis h4 = huisBeheerder.GeefHuis(2);
            Huurder hu3 = huurBeheerder.GeefHuurder(2);
            contractBeheerder.MaakContract("FHKJ427",new Huurperiode(DateTime.Parse("2023-12-27").Date,28), hu3, h4);
            contractBeheerder.MaakContract("ZRUOI87", new Huurperiode(DateTime.Parse("2024-01-01").Date, 31), new Huurder("Lang de Lange", new Contactgegevens("LangdeLange@gmail.com", "046548829", "Brugge")),new Huis("Vogelstraat", 331, p1));

            Huurcontract c2 = new Huurcontract("ZRUOI87",new Huurperiode(DateTime.Now.Date,21),hu3,new Huis("Bloemkoolstraat", 19, p2));
            contractBeheerder.UpdateContract(c2);
            Huis h5 = huisBeheerder.GeefHuis(3);
            contractBeheerder.MaakContract("FHU678Z", new Huurperiode(DateTime.Parse("2024-01-12"), 2), hu3, h5);


            List<Huurcontract> contracten = contractBeheerder.GeefContracten(DateTime.Now.Date, DateTime.Now.AddDays(31).Date);
            Console.WriteLine($"CONTRACTEN TUSSEN '{DateTime.Now.Date.ToString("dd-MM-yyyy")}' en '{DateTime.Now.AddDays(31).Date.ToString("dd-MM-yyyy")}'");
            foreach (Huurcontract c in contracten)
            {
                Console.WriteLine($"CONTRACT\n   HUURDER: Naam: {c.Huurder.Naam}, E-mail: {c.Huurder.Contactgegevens.Email}, Tel: {c.Huurder.Contactgegevens.Tel}, Adres: {c.Huurder.Contactgegevens.Adres}");
                Console.WriteLine($"   Begin datum: {c.Huurperiode.StartDatum}, Eind datum: {c.Huurperiode.EindDatum}");
                Console.WriteLine($"   HUIS: Straat:{c.Huis.Straat}, Huisnummer: {c.Huis.Nr}, Actief: {c.Huis.Actief} \n   PARK: Naam: {c.Huis.Park.Naam}, Locatie: {c.Huis.Park.Locatie}\n");
            }

        }
    }
}
