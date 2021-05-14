using System;
using DSModels;
using DSBL;
using System.Collections.Generic;
namespace DSUI
{
    public class CustomerMenu : IMenu
    {
        private IStoreLocationBL _storeLoBL;
        private IBuyerBL _buyerBL;
        private string _address;
        private string _location;
        private IOrderBL _orBL;
        private double _runningCount;
        public CustomerMenu( IStoreLocationBL StoreLoBL, IBuyerBL BuyerBL, IOrderBL OBL){
            this._storeLoBL = StoreLoBL;
            this._buyerBL = BuyerBL;
            this._orBL = OBL;
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
                        foreach(StoreLocation s in ViewStoreList()){
                            Console.WriteLine(s.ToString());
                        }
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
            bool repeat = true;
            do{
                Console.WriteLine("What store would you like to buy from?");
                Console.WriteLine("[0] View list of stores");
                Console.WriteLine("[1] I know what store I want to order from");
                input = Console.ReadLine();
                switch(input){
                    case "0":
                        foreach(StoreLocation s in ViewStoreList()){
                            Console.WriteLine(s.ToString());
                        }
                        repeat = false;
                        break;
                    case "1":
                        repeat = false;
                        break;
                    default:
                        Console.WriteLine("Invalid input");
                        break;
                }
            }while(repeat);
            repeat = true;
            do{
                Console.WriteLine("Enter the store you'd like to buy from");
                Console.WriteLine("[0] View list of stores");
                Console.WriteLine("[1] I know what store I want to order from");
                input = Console.ReadLine();
                switch(input){
                    case "0":
                        foreach(StoreLocation s in ViewStoreList()){
                            Console.WriteLine(s.ToString());
                        }
                        repeat = false;
                        break;
                    case "1":
                        repeat = false;
                        break;
                    default:
                        Console.WriteLine("Invalid input");
                        break;
                }
            }while(repeat);
            
            ViewStoreInv();
            repeat = true;
            _runningCount = 0;
            do{
                Console.WriteLine("Enter the address of the store");
                _address = Console.ReadLine();
                Console.WriteLine("Enter the location name of the store");
                _location = Console.ReadLine();
                Console.WriteLine("Enter the gender of dog you'd like to purchase");
                string gender = Console.ReadLine();
                Console.WriteLine("Enter the breed of the Dog you'd like to purchase");
                string breed = Console.ReadLine();
                Console.WriteLine("Enter how many you would like to purchase");
                int quant = int.Parse(Console.ReadLine());
                //get dog by id
                _storeLoBL.FindItem(new StoreLocation(_address, _location), new Dog(breed, gender, 1000.0), quant);
                //
            }while(repeat);
        }

        private void ViewStoreInv()
        {
            bool repeat = true;
            do{
                Console.WriteLine("Enter the address of the store");
                _address = Console.ReadLine();
                Console.WriteLine("Enter the location name of the store");
                _location = Console.ReadLine();
                try{
                    foreach(Item i in _storeLoBL.GetStoreInventory(_address,_location)){
                        Console.WriteLine(i.ToString());
                    }
                    repeat = false;
                }
                catch(Exception e){
                    repeat = true;
                    Console.WriteLine("That didn't work.");
                    Console.WriteLine("Enter q to exit or any other character to continue");
                    if(Console.ReadLine().Equals("q")) repeat = false;
                }
            }while(repeat);
        }

        private List<StoreLocation> ViewStoreList()
        {
            return _storeLoBL.GetAllStoreLocations();
        }
    }
}
