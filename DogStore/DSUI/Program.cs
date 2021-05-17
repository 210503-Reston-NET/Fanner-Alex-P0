using System.IO;
using DSModels;
using DSBL;
using Microsoft.Extensions.Configuration;
using DSDL.Entities;
using Microsoft.EntityFrameworkCore;
namespace DSUI
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            string connectionString = configuration.GetConnectionString("FannerDogsDB");
            DbContextOptions<FannerDogsDBContext> options = new DbContextOptionsBuilder<FannerDogsDBContext>()
            .UseSqlServer(connectionString).Options;
            var context = new FannerDogsDBContext(options);
            IMenu menu = new GeneralMenu(new StoreLocationBL(context),new BuyerBL(context), new OrderBL(context));
            menu.OnStart();
        }
        
    }
}
