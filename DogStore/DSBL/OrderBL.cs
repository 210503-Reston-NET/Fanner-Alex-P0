using DSDL;
using DSModels;

namespace DSBL
{
    public class OrderBL : IOrderBL
    {
        private Repo _repoDS = new Repo();
        public DogOrder AddOrder(DogBuyer buyer, double tot, StoreLocation sl)
        {
            return _repoDS.AddOrder(buyer, tot, sl);
        }
    }
}