using System.Collections.Generic;
namespace DSModels
{
    public class StoreLocation
    {
        public string Address{get; set;}
        public string Location{get; set;}

        public List<Item> Inventory{get;set;}
    }
}