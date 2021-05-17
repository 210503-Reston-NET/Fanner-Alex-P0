using DSModels;
using System.Collections.Generic;
using Entity = DSDL.Entities;
namespace DSDL
{
    public interface IRepo
    {
         List<StoreLocation> GetAllStoreLocations();
        StoreLocation AddStoreLocation(StoreLocation store);

        List<Item> GetStoreInventory(string address, string location);
        Item FindItem(StoreLocation store, Dog dog, int quant);
        Item UpdateItem(StoreLocation store, Dog dog, int quant);

        Item AddItem(StoreLocation store, Dog dog, int quant);
        DogOrder AddOrder(DogBuyer buyer, double total, StoreLocation sl);
        DogBuyer FindBuyer(long phoneNumber);
        DogBuyer AddBuyer(DogBuyer buyer);
    }
}