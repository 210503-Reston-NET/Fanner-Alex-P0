using DSModels;
using System.Collections.Generic;
namespace DSBL
{
    public interface IBuyerBL
    {
        List<DogBuyer> GetAllBuyers();
        DogBuyer AddBuyer(DogBuyer user);
        DogBuyer FindUser(long phone);
    }
}