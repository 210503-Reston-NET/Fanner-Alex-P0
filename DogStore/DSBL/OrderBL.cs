using DSDL;
using DSModels;
using Entity = DSDL.Entities;
namespace DSBL
{
    public class OrderBL : IOrderBL
    {

        private Repo _repoDS;
        public OrderBL(Entity.FannerDogsDBContext context ){
            _repoDS =  new Repo(context);
        }
        public DogOrder AddOrder(DogBuyer buyer, double tot, StoreLocation sl)
        {
            return _repoDS.AddOrder(buyer, tot, sl);
        }
    }
}