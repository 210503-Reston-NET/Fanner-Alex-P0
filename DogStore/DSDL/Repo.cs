using System.Linq;
using System.Collections.Generic;
using DSModels;
using System.IO;
using System.Text.Json;
using System;
namespace DSDL
{
    /// <summary>
    /// Repository class to store data in JSON file.
    /// </summary>
    public class Repo : IRepo
    {
        private const string storePath = "../DSDL/Stores";
        private string jsonString;
        private string invPath;
        private string jsonInv;
        private List<StoreLocation> _stores = new List<StoreLocation>();
        /// <summary>
        /// Method to add store location to the file. Adds a store to a file and returns
        /// the added store.
        /// </summary>
        /// <param name="store">StoreLocation to add to memory</param>
        /// <returns>Return added StoreLocation</returns>
        public StoreLocation AddStoreLocation(StoreLocation store)
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
            _stores.Add(store);
            return store;
        }

        /// <summary>
        /// Method that returns all the stores in memory.
        /// </summary>
        /// <returns>List of StoreLocation stored in the JSON</returns>
        public List<StoreLocation> GetAllStoreLocations()
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
            return _stores;
        }
        /// <summary>
        /// Gets a store from memory and returns the Inventory as a List of Items.
        /// </summary>
        /// <param name="address"> Address of store you're looking for</param>
        /// <param name="location"> Name of store you're looking for</param>
        /// <returns>List of items responding to the store's inventory.</returns>
        public List<Item> GetStoreInventory(string address, string location)
        {
            try{
                return FindStore(address, location).GetInventory();
            } catch(Exception){
                return new List<Item>();
            }
        }
        /// <summary>
        /// Finds and returns the result of a LINQ query which matches on an 
        /// address and location of a store.
        /// </summary>
        /// <param name="address"> Address of the store you're looking for.</param>
        /// <param name="location"> Location name of the store you're looking for.</param>
        /// <returns></returns>
        public StoreLocation FindStore(string address, string location){
            StoreLocation store = new StoreLocation(address, location);
            return GetAllStoreLocations().FirstOrDefault(stor => stor.Equals(store));
        }
        /// <summary>
        /// Finds a store you're looking for and removes it from the JSON file.
        /// </summary>
        /// <param name="address"> Address of the store you want to remove.</param>
        /// <param name="location"> Name of the store you want to remove.</param>
        /// <returns> Store which was removed from memory.</returns>
        public StoreLocation RemoveStore(string address, string location){
            //List<StoreLocation> storesFromFile = GetAllStoreLocations();
            StoreLocation store = FindStore(address, location);
            /*storesFromFile.Remove(store);
            jsonString = JsonSerializer.Serialize(storesFromFile);
            File.WriteAllText(storePath, jsonString);*/
            _stores.Remove(store);
            return store;
        }
    }
}