using DSModels;
using System.Collections.Generic;
namespace DSDL
{
    public interface IRepo
    {
         List<StoreLocation> GetAllStoreLocations();
        StoreLocation AddStoreLocation(StoreLocation store);

        List<Item> GetStoreInventory(string address, string location);
    }
}