using System;
using DSBL;
using DSModels;
namespace DSUI
{
    public class GeneralMenu : IMenu
    {
        private IStoreLocationBL _storeLoBL;
        public GeneralMenu( IStoreLocationBL StoreLoBL){
            this._storeLoBL = StoreLoBL;
            
        }
        public void OnStart()
        {
            bool repeat = true;
            do{
                Console.WriteLine("Welcome to the Dog Store!");
                Console.WriteLine("How can I Help you?");
                Console.WriteLine("[0] See list of stores");
                Console.WriteLine("[1] See a shop's inventory");
                Console.WriteLine("[2] Order a random dog");
                Console.WriteLine("[3] Add a customer");
                Console.WriteLine("[4] Find a customer");
                Console.WriteLine("[5] See a customer's orders");
                Console.WriteLine("[6] See a location's orders");
                Console.WriteLine("[7] Add a Store")
                
                string input = Console.ReadLine();
                switch(input){
                    case "0":
                        ViewStoreList();
                        break;
                    case "1":
                        ViewStoreInv();
                        break;
                    case "2":
                        OrderDog();
                        break;
                    case "7":
                        StoreLocation storeLocation = _storeLoBL.AddStoreLocation(new StoreLocation());
                    default:
                        repeat = false;
                        break;
                }
            } while(repeat);
            
        }
        private void ViewStoreList(){}
        private void ViewStoreInv(){}
        private void OrderDog(){}
    }
}