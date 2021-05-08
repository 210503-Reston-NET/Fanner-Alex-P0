using System;
namespace DSUI
{
    public class GeneralMenu : IMenu
    {
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