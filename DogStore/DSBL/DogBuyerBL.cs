using System.Collections.Generic;
using DSModels;
using DSDL;
namespace DSBL
{
    public class DogBuyerBL : IBuyerBL
    {
        public DogBuyer AddBuyer(DogBuyer user)
        {
            DSSCStorage.BuyerList.Add((DogBuyer)user);
            return user;
        }

        public List<DogBuyer> GetAllBuyers()
        {
            return DSSCStorage.BuyerList;
        }
    }
}