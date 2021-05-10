using System.Collections.Generic;
using DSDL;
using DSModels;

namespace DSBL
{
    public class StoreLocationBL:IStoreLocationBL
    {
        public List<StoreLocation> GetAllStoreLocations(){
            return DSSCStorage.StoreList;
        }
        public StoreLocation AddStoreLocation(StoreLocation store){
            DSSCStorage.StoreList.Add(store);
            return store;
        }

        public List<Item> GetStoreInventory(string address, string location)
        {
            throw new System.NotImplementedException();
        }
    }
}