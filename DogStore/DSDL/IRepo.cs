using DSModels;
using System.Collections.Generic;
using Entity = DSDL.Entities;
namespace DSDL
{
    public interface IRepo
    {
         List<StoreLocation> GetAllStoreLocations();
        StoreLocation AddStoreLocation(StoreLocation store, DogManager dogManager);

        List<Item> GetStoreInventory(string address, string location);
        Item FindItem(StoreLocation store, Dog dog, int quant);
        Item UpdateItem(StoreLocation store, Dog dog, int quant);

        Item AddItem(StoreLocation store, Dog dog, int quant);
        DogOrder AddOrder(DogOrder dogOrder);
        DogBuyer FindBuyer(long phoneNumber);
        DogBuyer AddBuyer(DogBuyer buyer);
        DogManager FindManager(long phoneNumber);
        DogManager AddManager(DogManager buyer);
        List<DogOrder> FindUserOrders(long phoneNumber, int option);
    }
}