using ParkDataLayer.Model;

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
        }
    }
}
