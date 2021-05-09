using DSModels;
using System.Collections.Generic;
namespace DSBL
{
    public interface IManagerBL
    {
        List<Item> GetAllItems();
        DogBuyer AddItem(Item item);
    }
}