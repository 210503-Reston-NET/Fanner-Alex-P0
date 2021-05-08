using System.Collections.Generic;
using DSDL;
using DSModels;

namespace DSBL
{
    public class StoreLocationBL:IStoreLocationBL
    {
        List<StoreLocation> GetAllStoreLocations(){
            return DSSCStorage.StoreList;
        }
        StoreLocation AddStoreLocation(StoreLocation store){
            DSSCStorage.StoreList.Add(store);
        }
    }
}