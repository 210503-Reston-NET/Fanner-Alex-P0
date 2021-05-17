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
            return _repoDS.AddBuyer(user);
        }

        public DogBuyer FindUser(long phone)
        {
            return _repoDS.FindBuyer(phone);
        }

        public List<DogBuyer> GetAllBuyers()
        {
            throw new System.NotImplementedException();
        }
    }
}