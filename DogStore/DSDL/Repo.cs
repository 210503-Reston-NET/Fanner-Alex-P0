
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
        /// <returns>Return added StoreLocation</returns>
        public Model.StoreLocation AddStoreLocation(Model.StoreLocation store)
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
            //StoreLocation sL = new StoreLocation(address, location);
            try{
                Entity.DogStore dS = (
                                        from DogStore in _context.DogStores where 
                                        DogStore.StoreAddress == address && DogStore.StoreName == location
                                        select DogStore
                                        ).Single();
                List<Entity.Inventory> iList = dS.Inventories.ToList();
                List<Model.Item> itemList = new List<Model.Item>();
                foreach(Entity.Inventory i in iList){
                    itemList.Add(new Model.Item(new Model.Dog(i.Dog.Breed,i.Dog.Gender.ToCharArray()[0], i.Dog.Price),i.Quantity.Value));
                }
                return itemList;
            } catch(Exception){
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
            StoreLocation store = new StoreLocation(address, location);
            return GetAllStoreLocations().FirstOrDefault(stor => stor.Equals(store));
        }
        /// <summary>
        /// Finds a store you're looking for and removes it from the JSON file.
        /// </summary>
        /// <param name="address"> Address of the store you want to remove.</param>
        /// <param name="location"> Name of the store you want to remove.</param>
        /// <returns> Store which was removed from memory.</returns>
        public Model.StoreLocation RemoveStore(string address, string location){
            //List<StoreLocation> storesFromFile = GetAllStoreLocations();
            StoreLocation store = FindStore(address, location);
            /*storesFromFile.Remove(store);
            jsonString = JsonSerializer.Serialize(storesFromFile);
            File.WriteAllText(storePath, jsonString);*/
            _stores.Remove(store);
            return store;
        }

        public Model.Item FindItem(StoreLocation store, Dog dog, int quant)
        {
            Item newItem = new Item(dog, quant);
            try{
            string add = FindStore(store.Address, store.Location).Address;
            string loc = FindStore(store.Address, store.Location).Location;
            return GetStoreInventory(add, loc).First(item => item.Equals(newItem));
            }
            catch(Exception){
                Console.WriteLine("Item not found");
                return new Item(dog, quant);
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
    }
}