//System.Security.Cryptography.X509Certificates;
using DSModels;
using System.Collections.Generic;
namespace DSBL
{
    public interface IStoreLocationBL
    {
        List<StoreLocation> GetAllStoreLocations();
        StoreLocation AddStoreLocation(StoreLocation store);
    }
}