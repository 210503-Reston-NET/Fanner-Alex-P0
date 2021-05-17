using DSModels;
using System.Collections.Generic;
namespace DSBL
{
    public interface IOrderBL
    {
        DogOrder AddOrder(DogOrder dogOrder);
        List<DogOrder> FindUserOrders(long phoneNumber, int option);
    }
}