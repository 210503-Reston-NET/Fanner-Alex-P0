using System;
using DSModels;
using DSBL;
using System.Collections.Generic;
namespace DSUI
{
    public class ManagerMenu : IMenu
    {
        private IStoreLocationBL _storeLoBL;
        private IManagerBL _managerBL;
        private string _location;
        private string _address;
        private StoreLocation _store;
        public ManagerMenu( IStoreLocationBL StoreLoBL){
            this._storeLoBL = StoreLoBL;
        }
        public void OnStart()
        {
            bool repeat = true;
            do{
                Console.WriteLine("Welcome manager, please select an option from the list:");
                Console.WriteLine("[0] Add a store");
                Console.WriteLine("[1] Stock some shelves");
                string input = Console.ReadLine();
                switch(input){
                    case "0":
                        Console.WriteLine("Enter the store's name:");
                        _location = Console.ReadLine();
                        Console.WriteLine("Enter the store's address:");
                        _address = Console.ReadLine();
                        StoreLocation store = new StoreLocation(_location, _address);
                        _storeLoBL.AddStoreLocation(store);
                        break;
                    case "1":
                        StockShelves();
                        break;
                    default:
                        repeat = false;
                        break;
                }
            }while(repeat);
        }
        private void StockShelves(){        
            Console.WriteLine("Enter the name of the store you're stocking:");
            _location = Console.ReadLine();
            Console.WriteLine("Enter the store's address:");
            _address = Console.ReadLine();
            try{
                _store = _storeLoBL.GetStore(_address,_location);
                _storeLoBL.RemoveStore(_address,_location);
                Console.WriteLine("Enter breed of the dog");
                string breed = Console.ReadLine();
                Console.WriteLine("Enter gender of the dog");
                string gender = Console.ReadLine();
                Dog dog = new Dog(breed, gender);
                Console.WriteLine("How many?");
                int quant = int.Parse(Console.ReadLine());
                Item ite = new Item(dog, quant);
                _store.AddItem(ite);
                _storeLoBL.AddStoreLocation(_store);
                Console.WriteLine("Thanks!");
            }catch(Exception){
                Console.WriteLine("Can't find the store");
            }
        
        }
    }
}