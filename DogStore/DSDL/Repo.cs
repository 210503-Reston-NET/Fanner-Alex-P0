using System.Diagnostics;

using System.Linq;
using System.Collections.Generic;
using Model = DSModels;
using Entity = DSDL.Entities;
using DSModels;
using System.IO;
using System.Text.Json;
using System;
using Microsoft.EntityFrameworkCore;
namespace DSDL
{
    /// <summary>
    /// Repository class to store data in JSON file.
    /// </summary>
    public class Repo : IRepo
    {
        private string invPath;
        private string jsonInv;
        private List<Model.StoreLocation> _stores;
        private List<DogOrder> _orders;
        private List<DogBuyer> _buyers;
        private Entity.FannerDogsDBContext _context;
        public Repo(Entity.FannerDogsDBContext context){
            _context = context;
        }
        /// <summary>
        /// Method to add store location to the file. Adds a store to a file and returns
        /// the added store.
        /// </summary>
        /// <param name="store">StoreLocation to add to memory</param>
        // <returns>Return added StoreLocation</returns>
        public Model.StoreLocation AddStoreLocation(Model.StoreLocation store, Model.DogManager dogManager)
        {
            /*List<StoreLocation> storesFromFile = GetAllStoreLocations();
            storesFromFile.Add(store);
            jsonString = JsonSerializer.Serialize(storesFromFile);
            foreach(StoreLocation s in storesFromFile){
                invPath = "../DSDL/"+s.Location;
                jsonInv = JsonSerializer.Serialize(s.GetInventory());
                File.WriteAllText(invPath, jsonInv);
            }
            File.WriteAllText(storePath, jsonString);*/
            try{
                Entity.DogStore dogStore = new Entity.DogStore();
                dogStore.StoreName = store.Location;
                dogStore.StoreAddress = store.Address;
                _context.DogStores.Add(
                    dogStore
                );
                Entity.ManagesStore managesStore = new Entity.ManagesStore();
                
                _context.SaveChanges();
                Entity.DogStore dS = (
                                        from DogStore in _context.DogStores where 
                                        DogStore.StoreAddress == dogStore.StoreAddress && DogStore.StoreName == dogStore.StoreName
                                        select DogStore
                                        ).Single();
                managesStore.ManagerId = dogManager.PhoneNumber;
                managesStore.StoreId = dS.Id;
                _context.ManagesStores.Add(managesStore);
                _context.SaveChanges();
            }
            catch(Exception ex){
                Console.WriteLine(ex.Message);
            }
            return store;
        }

        /// <summary>
        /// Method that returns all the stores in memory.
        /// </summary>
        /// <returns>List of StoreLocation stored in the JSON</returns>
        public List<Model.StoreLocation> GetAllStoreLocations()
        {
            /*try{
                jsonString = File.ReadAllText(storePath);
            } catch(Exception){
                return new List<StoreLocation>();
            }
            List<StoreLocation> sList = JsonSerializer.Deserialize<List<StoreLocation>>(jsonString);
            foreach(StoreLocation s in sList){
                invPath = "../DSDL/"+s.Location;
                jsonInv = File.ReadAllText(invPath);
                s.SetInventory(JsonSerializer.Deserialize<List<Item>>(jsonInv));
            }*/
            List<Model.StoreLocation> storeList = new List<Model.StoreLocation>();
            List<Entity.DogStore> dogStoreList = (from DogStore in _context.DogStores select DogStore).ToList();
            foreach(Entity.DogStore dS in dogStoreList){
                storeList.Add(new StoreLocation(dS.Id, dS.StoreAddress,dS.StoreName));
            }
            return storeList;
        }
        /// <summary>
        /// Gets a store from memory and returns the Inventory as a List of Items.
        /// </summary>
        /// <param name="address"> Address of store you're looking for</param>
        /// <param name="location"> Name of store you're looking for</param>
        /// <returns>List of items responding to the store's inventory.</returns>
        public List<Model.Item> GetStoreInventory(string address, string location)
        {
            StoreLocation sL = new StoreLocation(address, location);
            try{
                Entity.DogStore dS = (
                                        from DogStore in _context.DogStores where 
                                        DogStore.StoreAddress == address && DogStore.StoreName == location
                                        select DogStore
                                        ).Single();
                List<Entity.Inventory> iList = (
                                        from Inventory in _context.Inventories where
                                        Inventory.StoreId == dS.Id
                                        select Inventory
                                        ).ToList();
                List<Model.Item> itemList = new List<Model.Item>();
                foreach(Entity.Inventory i in iList){
                    Entity.Dog dog = (
                                        from Dog in _context.Dogs where
                                        Dog.ItemId == i.DogId
                                        select Dog
                    ).Single();
                    Console.WriteLine(dog.Breed);
                    Console.WriteLine(dog.Gender.ToCharArray()[0].ToString());
                    Console.WriteLine(dog.Price.ToString());
                    itemList.Add(new Model.Item(new Model.Dog(dog.Breed,dog.Gender.ToCharArray()[0], dog.Price),i.Quantity.Value));
                }
                return itemList;
            } catch(Exception e){
            //    Console.WriteLine(e.Message);
                return new List<Model.Item>();
            }
        }

        public Model.Item AddItem(StoreLocation store, Dog dog, int quant)
        {
            Item newItem = new Item(dog, quant);
            try{
                Entity.Dog searchDog = (
                                        from Dog in _context.Dogs where 
                                        Dog.Breed == dog.Breed && Dog.Gender == dog.Gender.ToString()
                                        select Dog
                                        ).Single();
                Entity.DogStore dS = (
                                        from DogStore in _context.DogStores where 
                                        DogStore.StoreAddress == store.Address && DogStore.StoreName == store.Location
                                        select DogStore
                                        ).Single();
                
                try{

                    Entity.Inventory inv = (
                                        from Inventory in _context.Inventories where
                                        Inventory.StoreId == dS.Id && Inventory.DogId == searchDog.ItemId
                                        select Inventory
                                        ).Single();
                    inv.Quantity += quant;
                    _context.SaveChanges();
                    return newItem;
                }
                catch(Exception e){
                    Console.WriteLine(e.Message);
                    Entity.Inventory inventory = new Entity.Inventory();
                    inventory.DogId = searchDog.ItemId;
                    inventory.Quantity = quant;
                    inventory.StoreId = dS.Id;
                    _context.Inventories.Add(inventory);
                    _context.SaveChanges();
                    return newItem;
                }
            }
            catch (Exception e){
                Console.WriteLine(e.Message);
                Entity.Dog newDog = new Entity.Dog();
                newDog.ItemId = new Random().Next();
                newDog.Breed = dog.Breed;
                newDog.Gender = dog.Gender.ToString();
                newDog.Price = dog.Price;
                _context.Dogs.Add(newDog);
                _context.SaveChanges();
                Entity.Dog searchDog = newDog;
                Entity.DogStore dS = (
                                        from DogStore in _context.DogStores where 
                                        DogStore.StoreAddress == store.Address && DogStore.StoreName == store.Location
                                        select DogStore
                                        ).Single();
                Entity.Inventory inventory = new Entity.Inventory();
                    inventory.DogId = searchDog.ItemId;
                    inventory.Quantity = quant;
                    inventory.StoreId = dS.Id;
                    _context.Inventories.Add(inventory);
                    _context.SaveChanges();
                    return newItem;
            }
            /*try{
            string add = FindStore(store.Address, store.Location).Address;
            string loc = FindStore(store.Address, store.Location).Location;
            GetStoreInventory(add, loc).First(item => item.Equals(newItem)).Quantity += quant;
            GetStoreInventory(add, loc).First(item => item.Equals(newItem)).Dog.Price = dog.Price;
            return newItem;
            }
            catch(Exception){
                string add = FindStore(store.Address, store.Location).Address;
                string loc = FindStore(store.Address, store.Location).Location;
                Console.WriteLine("New item added");
                GetAllStoreLocations().FirstOrDefault(stor => stor.Equals(store)).AddItem(newItem);
                foreach(Item e in GetStoreInventory(add, loc)) Console.WriteLine(e.Dog.ToString());
                return newItem;
            }*/
        }

        /// <summary>
        /// Finds and returns the result of a LINQ query which matches on an 
        /// address and location of a store.
        /// </summary>
        /// <param name="address"> Address of the store you're looking for.</param>
        /// <param name="location"> Location name of the store you're looking for.</param>
        /// <returns></returns>
        public Model.StoreLocation FindStore(string address, string location){
            //StoreLocation store = new StoreLocation(address, location);
            return GetAllStoreLocations().First(stor => stor.Address == address && stor.Location == location);
            //from DogStore in _context.DogStores where 
        }
        /// <summary>
        /// Finds a store you're looking for and removes it from the JSON file.
        /// </summary>
        /// <param name="address"> Address of the store you want to remove.</param>
        /// <param name="location"> Name of the store you want to remove.</param>
        /// <returns> Store which was removed from memory.</returns>
        public Model.StoreLocation RemoveStore(string address, string location){
            List<StoreLocation> storesFromFile = GetAllStoreLocations();
            StoreLocation store = FindStore(address, location);
            /*storesFromFile.Remove(store);
            jsonString = JsonSerializer.Serialize(storesFromFile);
            File.WriteAllText(storePath, jsonString);*/
            _stores.Remove(store);
            return store;
        }

        public Model.Item FindItem(StoreLocation store, Dog dog, int quant)
        {
            //Item newItem = new Item(dog, quant);
            try{
                string add = FindStore(store.Address, store.Location).Address;
                string loc = FindStore(store.Address, store.Location).Location;
                Entity.Dog searchDog = (
                                        from Dog in _context.Dogs where 
                                        Dog.Breed == dog.Breed && Dog.Gender == dog.Gender.ToString()
                                        select Dog
                                        ).Single();
                Entity.DogStore dS = (
                                        from DogStore in _context.DogStores where 
                                        DogStore.StoreAddress == store.Address && DogStore.StoreName == store.Location
                                        select DogStore
                                        ).Single();
                Entity.Inventory inv = (
                                        from Inventory in _context.Inventories where
                                        Inventory.StoreId == dS.Id && Inventory.DogId == searchDog.ItemId
                                        select Inventory
                                        ).Single();
                if(inv.Quantity<quant) {
                    Console.WriteLine("Store doesn't have that many of that dog!");
                    throw new Exception();
                }
                else {
                    return new Model.Item(new Dog(searchDog.Breed,searchDog.Gender.ToCharArray()[0],searchDog.Price,searchDog.ItemId),quant);
                }
            }
            catch(Exception){
                Console.WriteLine("Item not found");
                return null;
            }
        }

        public Model.Item UpdateItem(StoreLocation store, Dog dog, int quant)
        {
            try{
                Item itemToBeInc = FindItem(store, dog, quant);
                itemToBeInc.Quantity += quant;
                return itemToBeInc;
            }catch(Exception){
                Console.WriteLine("Item not found");
                return new Item(dog, quant);
            }
        }

        public Model.DogOrder AddOrder(DogBuyer buyer, double total, StoreLocation sl)
        {
            DogOrder order = new DogOrder(buyer, total, sl);
            try{
                _orders.Add(order);
            }catch(Exception){
                _orders = new List<DogOrder>();
                _orders.Add(order);
            }
            return order;
        }

        public Model.DogBuyer FindBuyer(long phoneNumber)
        {
            try{
                Entity.DogBuyer dogBuyer = (
                                            from DogBuyer in _context.DogBuyers where 
                                            DogBuyer.PhoneNumber == phoneNumber
                                            select DogBuyer
                                            ).Single();
                return new Model.DogBuyer(dogBuyer.UserName, dogBuyer.UserAddress,dogBuyer.PhoneNumber);
            }catch(Exception e){
                return null;
            }
        }

        public DogBuyer AddBuyer(DogBuyer buyer)
        {
            Entity.DogBuyer dogBuyer = new Entity.DogBuyer();
                    dogBuyer.UserName = buyer.Name;
                    dogBuyer.PhoneNumber = buyer.PhoneNumber;
                    dogBuyer.UserAddress = buyer.Address;
                    _context.DogBuyers.Add(dogBuyer);
                    _context.SaveChanges();
                    return buyer;
        }

        public DogManager FindManager(long phoneNumber)
        {
            try{
                Entity.DogManager dogManager = (
                                            from DogManager in _context.DogManagers where 
                                            DogManager.PhoneNumber == phoneNumber
                                            select DogManager
                                            ).Single();
                return new Model.DogManager(dogManager.PhoneNumber,dogManager.UserAddress,dogManager.UserName);
            }catch(Exception e){
                return null;
            }
        }

        public DogManager AddManager(DogManager manager)
        {
            Entity.DogManager dogManager = new Entity.DogManager();
                    dogManager.UserName = manager.Name;
                    dogManager.PhoneNumber = manager.PhoneNumber;
                    dogManager.UserAddress = manager.Address;
                    _context.DogManagers.Add(dogManager);
                    _context.SaveChanges();
                    return manager;
        }

        public DogOrder AddOrder(DogOrder dogOrder)
        {
            try{
                Entity.DogOrder dogOrd = new Entity.DogOrder();
                dogOrd.BuyerId = dogOrder.DogBuyer.PhoneNumber;
                dogOrd.StoreId = dogOrder.StoreLocation.id;
                dogOrd.DateOrder = dogOrder.OrderDate;
                dogOrd.Total = dogOrder.Total;
                _context.DogOrders.Add(dogOrd);
                _context.SaveChanges();
                Entity.OrderItem orderItem;
                dogOrd  = (
                            from DogOrder in _context.DogOrders where
                            DogOrder.BuyerId == dogOrder.DogBuyer.PhoneNumber &&
                            DogOrder.StoreId == dogOrder.StoreLocation.id &&
                            DogOrder.DateOrder == dogOrder.OrderDate &&
                            DogOrder.Total == dogOrder.Total
                            select DogOrder
                ).Single();
                foreach(Model.Item item in dogOrder.GetItems()){
                    Entity.Inventory inv = (
                                            from Inventory in _context.Inventories where
                                            Inventory.StoreId == dogOrder.StoreLocation.id && Inventory.DogId == item.Dog.id
                                            select Inventory
                                            ).Single();
                    inv.Quantity -= item.Quantity;
                    _context.SaveChanges();
                    orderItem = new Entity.OrderItem();
                    orderItem.DogId = item.Dog.id;
                    orderItem.OrderId = dogOrd.Id;
                    orderItem.Quantity = item.Quantity;
                    _context.OrderItems.Add(orderItem);
                    _context.SaveChanges();
                }
                return dogOrder;
            }
            catch(Exception e){
                Console.WriteLine("Something went wrong :(");
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public List<Model.DogOrder> FindUserOrders(long phoneNumber, int option)
        {
            Model.DogBuyer dogBuyer = FindBuyer(phoneNumber);
            List<Entity.DogOrder> dogOrders = new List<Entity.DogOrder>();
            switch(option){
            case 1:
                dogOrders = (
                                            from DogOrder in _context.DogOrders where
                                            DogOrder.BuyerId == phoneNumber
                                            orderby DogOrder.DateOrder ascending
                                            select DogOrder
                                            ).ToList();
                break;
            case 2:
                dogOrders = (
                                            from DogOrder in _context.DogOrders where
                                            DogOrder.BuyerId == phoneNumber
                                            orderby DogOrder.DateOrder descending
                                            select DogOrder
                                            ).ToList();
                break;
            case 3:
                dogOrders = (
                                            from DogOrder in _context.DogOrders where
                                            DogOrder.BuyerId == phoneNumber
                                            orderby DogOrder.Total ascending
                                            select DogOrder
                                            ).ToList();
                break;
            case 4:
                dogOrders = (
                                            from DogOrder in _context.DogOrders where
                                            DogOrder.BuyerId == phoneNumber
                                            orderby DogOrder.Total descending
                                            select DogOrder
                                            ).ToList();
                break;
            default:
                return null;
            }
            Entity.DogStore dogStore;
            List<Entity.OrderItem> orderItems;
            List<Model.DogOrder> returnOrders = new List<Model.DogOrder>();
            Model.StoreLocation storeLocation;
            Model.DogOrder returnOrder;
            Entity.Dog dog;
            foreach(Entity.DogOrder dogOrder in dogOrders){
                /*Entity.Dog dog = (
                                        from Dog in _context.Dogs where
                                        Dog.ItemId == i.DogId
                                        select Dog
                    ).Single();
                    Console.WriteLine(dog.Breed);
                    Console.WriteLine(dog.Gender.ToCharArray()[0].ToString());
                    Console.WriteLine(dog.Price.ToString());
                    itemList.Add(new Model.Item(new Model.Dog(dog.Breed,dog.Gender.ToCharArray()[0], dog.Price),i.Quantity.Value));*/
                dogStore = (
                            from DogStore in _context.DogStores where
                            DogStore.Id == dogOrder.StoreId
                            select DogStore
                            ).Single();
                orderItems = (
                            from OrderItem in _context.OrderItems where
                            OrderItem.OrderId == dogOrder.Id
                            select OrderItem
                            ).ToList();
                returnOrder = new DogOrder(
                    dogBuyer,
                    dogOrder.Total,
                    new Model.StoreLocation(
                        dogStore.Id,
                        dogStore.StoreAddress,
                        dogStore.StoreName
                    )
                );
                returnOrder.OrderDate = dogOrder.DateOrder;
                foreach(Entity.OrderItem orderItem in orderItems){
                    dog = (
                            from Dog in _context.Dogs where
                            Dog.ItemId == orderItem.DogId
                            select Dog
                    ).Single();
                    returnOrder.AddItemToOrder(new Model.Item(
                        new Model.Dog(
                            dog.Breed,
                            dog.Gender.ToCharArray()[0],
                            dog.Price
                        ),
                        orderItem.Quantity.Value
                    ));
                }
                returnOrders.Add(returnOrder);
            }
            return returnOrders;
        }
    }
}