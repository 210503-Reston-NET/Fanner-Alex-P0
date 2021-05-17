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
        private IValidation validation = new Validation();
        private DogBuyer _dogBuyer;
        private DogOrder _dogOrder;
        public CustomerMenu( IStoreLocationBL StoreLoBL, IBuyerBL BuyerBL, IOrderBL OBL){
            this._storeLoBL = StoreLoBL;
            this._buyerBL = BuyerBL;
            this._orBL = OBL;
        }
        public void OnStart()
        {
            long phone = validation.ValidatePhone("Hello, please enter your phone number in the format 1234567890");
            _dogBuyer = _buyerBL.FindUser(phone);
            if(_dogBuyer == null){
                string name = validation.ValidateName("Please enter your name in the format Firstname Lastname");
                string address = validation.ValidateAddress("Please enter your address in the format CityName, ST");
                _dogBuyer = new DogBuyer(name, address, phone);
                _buyerBL.AddBuyer(_dogBuyer);
            }
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
                    case "4":
                        break;
                    case "5":
                        ViewOrders();
                        break;
                    case "7":
                        //StoreLocation storeLocation = _storeLoBL.AddStoreLocation(new StoreLocation("test", "here"));
                        break;
                    case "a":

                        break;
                    default:
                        repeat = false;
                        break;
                }
            }while(repeat);
        }

        private void ViewOrders()
        {
            int orderOption = validation.ValidateOrderSearchOptions("Choose an option from the list!");
            foreach (DogOrder dogOrder in _orBL.FindUserOrders(_dogBuyer.PhoneNumber, orderOption)) Console.WriteLine(dogOrder.ToString());
        }

        private void AddCustomer()
        {
            long phone = validation.ValidatePhone("Hello, please enter your phone number in the format 1234567890");
            _dogBuyer = _buyerBL.FindUser(phone);
            if(_dogBuyer == null){
                string name = validation.ValidateName("Please enter your name in the format Firstname Lastname");
                string address = validation.ValidateAddress("Please enter your address in the format CityName, ST");
                _dogBuyer = new DogBuyer(name, address, phone);
                _buyerBL.AddBuyer(_dogBuyer);
            }else{
                Console.WriteLine("User is already in database.");
            }
        }

        private void OrderDog()
        {
            string input;
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
            string storeLocation = validation.ValidateString("Enter the store's name:");
            string storeAddress = validation.ValidateAddress("Enter the store's address in format CityName, ST");
            _dogOrder = new DogOrder(_dogBuyer,0,_storeLoBL.GetStore(storeAddress,storeLocation));
            do{
                
                char gender = validation.ValidateGender("Enter the gender of dog you'd like to purchase");
                string breed = validation.ValidateString("Enter the breed of the Dog you'd like to purchase");
                int quant = validation.ValidateInt("Enter how many you would like to purchase");
                Item lineItem = _storeLoBL.FindItem(new StoreLocation(_address, _location), new Dog(breed, gender, 1000.0), quant);
                if(lineItem != null) {
                    _dogOrder.AddItemToOrder(lineItem);
                    _dogOrder.Total += ((double)quant * lineItem.Dog.Price);
                    }
                Console.WriteLine("Enter c to complete order or any other character to continue");
                if(Console.ReadLine().Equals("c")) repeat = false;
                //get all the items you want to order
            }while(repeat);
            if(_orBL.AddOrder(_dogOrder)==null) Console.WriteLine("If you're seeing this, something went terribly wrong");
            //send the list of items to the database and remove them from the store's inventory
        }

        private void ViewStoreInv()
        {
            bool repeat = true;
            do{
                _location = validation.ValidateString("Enter the store's name:");
                _address = validation.ValidateAddress("Enter the store's address in format CityName, ST");
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
            } while(repeat);
        }

        private List<StoreLocation> ViewStoreList()
        {
            return _storeLoBL.GetAllStoreLocations();
        }
    }
}
