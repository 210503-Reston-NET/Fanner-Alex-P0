using System.Linq;
using System.Collections.Generic;
using DSModels;
using System.IO;
using System.Text.Json;
using System;
namespace DSDL
{
    public class Repo : IRepo
    {
        private const string storePath = "../DSDL/Stores";
        private string jsonString;
        public StoreLocation AddStoreLocation(StoreLocation store)
        {
            List<StoreLocation> storesFromFile = GetAllStoreLocations();
            storesFromFile.Add(store);
            jsonString = JsonSerializer.Serialize(storesFromFile);
            File.WriteAllText(storePath, jsonString);
            return store;
        }

        public List<StoreLocation> GetAllStoreLocations()
        {
            try{
                jsonString = File.ReadAllText(storePath);
            } catch(Exception){
                return new List<StoreLocation>();
            }
            return JsonSerializer.Deserialize<List<StoreLocation>>(jsonString);
        }

        public List<Item> GetStoreInventory(string address, string location)
        {
            try{
                return FindStore(address, location).GetInventory();
            } catch(Exception){
                return new List<Item>();
            }
        }

        public StoreLocation FindStore(string address, string location){
            StoreLocation store = new StoreLocation(address, location);
            return GetAllStoreLocations().FirstOrDefault(stor => stor.Equals(store));
        }
        public StoreLocation RemoveStore(string address, string location){
            List<StoreLocation> storesFromFile = GetAllStoreLocations();
            StoreLocation store = FindStore(address, location);
            storesFromFile.Remove(store);
            jsonString = JsonSerializer.Serialize(storesFromFile);
            File.WriteAllText(storePath, jsonString);
            return store;
        }
    }
}