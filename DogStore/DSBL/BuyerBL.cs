using System.Collections.Generic;
using DSModels;
using DSDL;
using Entity = DSDL.Entities;
namespace DSBL
{
    public class BuyerBL : IBuyerBL
    {
        private Repo _repoDS;
        public BuyerBL(Entity.FannerDogsDBContext context ){
            _repoDS =  new Repo(context);
        }
        public DogBuyer AddBuyer(DogBuyer user)
        {
            throw new System.NotImplementedException();
        }

        public List<DogBuyer> GetAllBuyers()
        {
            throw new System.NotImplementedException();
        }
    }
}