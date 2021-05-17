using System;
using DSModels;
using DSBL;
using System.Collections.Generic;
namespace DSUI
{
    public class ManagerMenu : IMenu
    {
        private IStoreLocationBL _storeLoBL;
        
        private string _location;
        private string _address;
        private StoreLocation _store;
        private IOrderBL _orBL;
        private IManagerBL _mBL;
        private IValidation validation = new Validation();
        private DogManager _dogManager;
        public ManagerMenu( IStoreLocationBL StoreLoBL,IOrderBL OBL, IManagerBL MBL){
            this._storeLoBL = StoreLoBL;
            this._orBL = OBL;
            this._mBL = MBL;
        }
        public void OnStart()
        {
            long phone = validation.ValidatePhone("Hello, please enter your phone number in the format 1234567890");
            _dogManager = _mBL.FindManager(phone);
            if(_dogManager == null){
                string name = validation.ValidateName("Please enter your name in the format Firstname Lastname");
                string address = validation.ValidateAddress("Please enter your address in the format CityName, ST");
                _dogManager = new DogManager(phone, address, name);
                _mBL.AddManager(_dogManager);
            }
            bool repeat = true;
            do{
                Console.WriteLine("Welcome manager, please select an option from the list:");
                Console.WriteLine("[0] Add a store");
                Console.WriteLine("[1] Stock some shelves");
                Console.WriteLine("[2] View a store's inventory");
                Console.WriteLine("[3] View a store's order history");
                string input = Console.ReadLine();
                switch(input){
                    case "0":
                        _location = validation.ValidateString("Enter the store's name:");
                        _address = validation.ValidateAddress("Enter the store's address in format CityName, ST");
                        StoreLocation store = new StoreLocation(_address,_location);
                        _storeLoBL.AddStoreLocation(store,_dogManager);
                        break;
                    case "1":
                        StockShelves();
                        break;
                    case "2":
                        ViewStoreInv();
                        break;
                    case "3":
                        ViewOrders();
                        break;
                    default:
                        repeat = false;
                        break;
                }
            }while(repeat);
        }
        private void StockShelves(){    
            _location = validation.ValidateString("Enter the store's name:");
            _address = validation.ValidateAddress("Enter the store's address in format CityName, ST");
            try{
                _store = _storeLoBL.GetStore(_address,_location);
                if(_store == null){
                    Console.WriteLine("Store Not Found, please add the store");
                    return;
                }
                //_storeLoBL.RemoveStore(_address,_location);
                string breed = validation.ValidateString("Enter breed of the dog");
                char gender = validation.ValidateGender("Enter gender of the dog either m or f");
                double price = validation.ValidateDouble("Enter price of the dog in the form dollars.cents");
                Dog dog = new Dog(breed, gender, price);
                int quant = validation.ValidateInt("How many? Just enter a number");
                
                Console.WriteLine(_store.ToString());
                _storeLoBL.AddItem(_store, dog, quant);
                //_storeLoBL.AddStoreLocation(_store);
                Console.WriteLine("Thanks!");
            }catch(Exception e){
                Console.WriteLine("Error while stocking");
            }
        
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
        private void ViewOrders()
        {
            _location = validation.ValidateString("Enter the store's name:");
            _address = validation.ValidateAddress("Enter the store's address in format CityName, ST");
            int orderOption = validation.ValidateOrderSearchOptions("Choose an option from the list!");
            foreach (DogOrder dogOrder in _orBL.FindStoreOrders(_address,_location,orderOption)) Console.WriteLine(dogOrder.ToString());
        }
    }
}