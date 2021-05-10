using System.Collections.Generic;
namespace DSModels
{
    /// <summary>
    /// Class representing a store with address, location, and inventory
    /// </summary>
    public class StoreLocation
    {
        /// <summary>
        /// Basic constructor with address and location.
        /// </summary>
        /// <param name="address">string representing store's address</param>
        /// <param name="location">string representing location's address</param>
        public StoreLocation(string address, string location){
            this.Address = address;
            this.Location = location;
        }
        /// <summary>
        /// String representing the address of the store.
        /// </summary>
        /// <value></value>
        public string Address{get; set;}
        /// <summary>
        /// String representing the location of the store.
        /// </summary>
        /// <value></value>
        public string Location{get; set;}
        /// <summary>
        /// List of items representing the store's inventory
        /// </summary>
        /// <value></value>
        public List<Item> Inventory{get;set;}
        /// <summary>
        /// Overriding the ToString() method to return basic information of the store.
        /// </summary>
        /// <returns>string representing the store's information</returns>
        public override string ToString()
        {
            return "Address: " + this.Address + "Location: " + this.Location;
        }
    }
}