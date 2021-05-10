using System.Collections.Generic;
using DSDL;
using DSModels;

namespace DSBL
{
    public class StoreLocationBL:IStoreLocationBL
    {
        private Repo _repoDS = new Repo();
        public List<StoreLocation> GetAllStoreLocations(){
            return _repoDS.GetAllStoreLocations();
        }
        public StoreLocation AddStoreLocation(StoreLocation store){
            return _repoDS.AddStoreLocation(store);
        }

        public List<Item> GetStoreInventory(string address, string location)
        {
            return _repoDS.GetStoreInventory(address, location);
        }
        public StoreLocation GetStore(string address, string location){
            return _repoDS.FindStore(address, location);
        }

        public StoreLocation RemoveStore(string address, string location)
        {
            return _repoDS.RemoveStore(address, location);
        }
    }
}