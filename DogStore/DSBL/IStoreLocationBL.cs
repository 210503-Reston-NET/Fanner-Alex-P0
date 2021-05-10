//System.Security.Cryptography.X509Certificates;
using DSModels;
using System.Collections.Generic;
namespace DSBL
{
    public interface IStoreLocationBL
    {
        List<StoreLocation> GetAllStoreLocations();
        StoreLocation AddStoreLocation(StoreLocation store);

        List<Item> GetStoreInventory(string address, string location);
        StoreLocation GetStore(string address, string location);
        StoreLocation RemoveStore(string address, string location);
    }
}