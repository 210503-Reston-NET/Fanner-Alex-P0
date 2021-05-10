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
            throw new System.NotImplementedException();
        }
    }
}