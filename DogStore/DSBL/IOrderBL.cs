using DSModels;

namespace DSBL
{
    public interface IOrderBL
    {
        DogOrder AddOrder(DogBuyer buyer, double tot, StoreLocation sl);
    }
}