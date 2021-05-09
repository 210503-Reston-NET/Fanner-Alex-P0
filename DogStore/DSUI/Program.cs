using System;
using DSModels;
using DSBL;
namespace DSUI
{
    class Program
    {
        static void Main(string[] args)
        {
            IMenu menu = new GeneralMenu(new StoreLocationBL());
            menu.OnStart();
        }
        
    }
}
