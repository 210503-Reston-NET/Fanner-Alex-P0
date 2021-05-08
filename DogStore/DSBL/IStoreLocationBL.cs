using System.Security.Cryptography.X509Certificates;
namespace DSBL
{
    public interface IStoreLocationBL
    {
        List<StoreLocation> GetAllStoreLocations();
        StoreLocation AddStoreLocation(StoreLocation store);
    }
}