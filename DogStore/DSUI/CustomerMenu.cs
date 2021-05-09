using System;
using DSModels;
using DSBL;
namespace DSUI
{
    public class CustomerMenu : IMenu
    {
        private IStoreLocationBL _storeLoBL;
        private IBuyerBL _buyerBL;
        public CustomerMenu( IStoreLocationBL StoreLoBL, IBuyerBL BuyerBL){
            this._storeLoBL = StoreLoBL;
            this._buyerBL = BuyerBL;
        }
        public void OnStart()
        {
            bool repeat = true;
            do{
                Console.WriteLine("How can I Help you?");
                Console.WriteLine("[0] See list of stores");
                Console.WriteLine("[1] See a shop's inventory");
                Console.WriteLine("[2] Order a dog");
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
                    case "3":
                        AddCustomer();
                        break;
                    case "7":
                        StoreLocation storeLocation = _storeLoBL.AddStoreLocation(new StoreLocation("test", "here"));
                        break;
                    case "a":

                        break;
                    default:
                        repeat = false;
                        break;
                }
            }while(repeat);
        }

        private void AddCustomer()
        {
            Console.WriteLine("What's your name?");
            string input = Console.ReadLine();
            DogBuyer buyer = new DogBuyer();
            buyer.Name = input;
            Console.WriteLine(input + buyer.Name);
            _buyerBL.AddBuyer(buyer);
            Console.WriteLine("Thank you " + buyer.Name + ". Feel free to look around.");
        }

        private void OrderDog()
        {
            Console.WriteLine("Are you an existing user?");
            Console.WriteLine("[yes] or [no]");
            string input = Console.ReadLine();
            switch(input){
                case "yes":
                    break;
                case "no":
                    AddCustomer();
                    break;
                default:
                    Console.WriteLine("Invalid input");
                    return;
            }
        }

        private void ViewStoreInv()
        {
            throw new NotImplementedException();
        }

        private void ViewStoreList()
        {
            throw new NotImplementedException();
        }
    }
}
