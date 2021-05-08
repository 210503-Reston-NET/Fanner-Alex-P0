using System.Collections.Generic;
namespace DSModels
{
    public class StoreLocation
    {
        public StoreLocation(string address, string location){
            this.Address = address;
            this.Location = location;
        }
        public string Address{get; set;}
        public string Location{get; set;}

        public List<Item> Inventory{get;set;}
    }
}