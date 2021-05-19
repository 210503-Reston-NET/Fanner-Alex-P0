
using System;
using Xunit;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using DSDL;
using Model = DSModels;
using Entity = DSDL.Entities;
using System.Linq;
namespace DSTests
{
    public class DSTest
    {
        private readonly DbContextOptions<Entity.FannerDogsDBContext> options;
        public DSTest()
        {
            options = new DbContextOptionsBuilder<Entity.FannerDogsDBContext>().UseSqlite("Filename=Test.db").Options;
            Seed();
        }
        [Fact]
        public void AddManagerAddsManager()
        {
            using (var context = new Entity.FannerDogsDBContext(options))
            {
                IRepo _repoDS = new Repo(context);
                Model.DogManager dogManager= new Model.DogManager(1234567890,"Test, TX","Texas Toaster");
                _repoDS.AddManager
                (
                    dogManager
                );
                Model.DogManager dogManagerReturned = _repoDS.FindManager(1234567890);
                Assert.Equal(dogManagerReturned.PhoneNumber, dogManager.PhoneNumber);
            }
        }
        [Fact]
        public void AddBuyerAddsBuyer()
        {
            using (var context = new Entity.FannerDogsDBContext(options))
            {
                IRepo _repoDS = new Repo(context);
                Model.DogBuyer dogBuyer= new Model.DogBuyer("Texas Toaster","Test, TX",1234567890);
                _repoDS.AddBuyer
                (
                    dogBuyer
                );
                Model.DogBuyer dogBuyerReturned = _repoDS.FindBuyer(1234567890);
                Assert.Equal(dogBuyerReturned.PhoneNumber, dogBuyer.PhoneNumber);
            }
        }
        private void Seed(){
            using(var context = new Entity.FannerDogsDBContext(options)){
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                
            }
        }
    }
}
