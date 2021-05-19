
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
        /// <summary>
        /// Checks AddManager and FindManager for functionality
        /// </summary>
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
        /// <summary>
        /// Checks AddStoreLocation and FindStore
        /// </summary>
        public void AddStoreLocationAddsStore()
        {
            using (var context = new Entity.FannerDogsDBContext(options))
            {
                IRepo _repoDS = new Repo(context);
                Model.DogManager dogManager= new Model.DogManager(1234567890,"Test, TX","Texas Toaster");
                _repoDS.AddManager
                (
                    dogManager
                );
                _repoDS.AddStoreLocation(
                    new Model.StoreLocation("Test, TX", "Test Dogs"),
                    dogManager
                );
                Model.StoreLocation store = _repoDS.FindStore("Test, TX", "Test Dogs");
                bool storeThere = (store != null);
                bool expected = true;
                Assert.Equal(storeThere, expected);
            }
        }
        [Fact]
        /// <summary>
        /// Checks AddBuyer and FindBuyer
        /// </summary>
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
        [Fact]
        /// <summary>
        /// Checks GetAllDogManagers
        /// </summary>
        public void GetAllManagersGetsManagers()
        {
            using (var context = new Entity.FannerDogsDBContext(options))
            {
                IRepo _repoDS = new Repo(context);
                Model.DogManager dogManager= new Model.DogManager(9638527410,"Wired, Wyoming","Ama Test");
                _repoDS.AddManager
                (
                    dogManager
                );
                dogManager= new Model.DogManager(1234567890,"Test, TX","Texas Toaster");
                _repoDS.AddManager
                (
                    dogManager
                );
                List<Model.DogManager> dogManagers = _repoDS.GetAllDogManagers();
                int expected = 2;
                Assert.Equal(dogManagers.Count, expected);
            }
        }
        [Fact]
        /// <summary>
        /// Checks GetAllBuyers
        /// </summary>
        public void GetAllBuyersGetsBuyers()
        {
            using (var context = new Entity.FannerDogsDBContext(options))
            {
                IRepo _repoDS = new Repo(context);
                Model.DogBuyer dogBuyer = new Model.DogBuyer("Ama Test","Wired, Wyoming",9638527410);
                _repoDS.AddBuyer
                (
                    dogBuyer
                );
                dogBuyer= new Model.DogBuyer("Texas Toaster","Test, TX",1234567890);
                _repoDS.AddBuyer
                (
                    dogBuyer
                );
                List<Model.DogBuyer> dogBuyers = _repoDS.GetAllBuyers();
                int expected = 2;
                Assert.Equal(dogBuyers.Count, expected);
            }
        }
        [Fact]
        /// <summary>
        /// Checks AddItem and FindItem
        /// </summary>
        public void AddItemShouldBeFound(){
            using (var context = new Entity.FannerDogsDBContext(options))
            {
                IRepo _repoDS = new Repo(context);
                Model.DogManager dogManager= new Model.DogManager(1234567890,"Test, TX","Texas Toaster");
                _repoDS.AddManager
                (
                    dogManager
                );
                Model.StoreLocation storeLocation = new Model.StoreLocation("Test, TX", "Test Dogs");
                _repoDS.AddStoreLocation(
                    storeLocation,
                    dogManager
                );
                Model.Dog dog = new Model.Dog("Special Breed",'f',1000);
                _repoDS.AddItem(
                    storeLocation,
                    dog,
                    5
                );
                Model.Item item = _repoDS.FindItem(
                    storeLocation,
                    dog,
                    5
                );
                bool itemThere = (item != null);
                bool expected = true;
                Assert.Equal(itemThere, expected);
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
