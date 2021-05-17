using DSModels;
using System.Collections.Generic;
namespace DSBL
{
    public interface IManagerBL
    {
        List<Item> GetAllItems();
        Item AddItem(Item item);
        List<DogManager> GetAllManagers();
        DogManager AddManager(DogManager user);
        DogManager FindManager(long phone);
    }
}