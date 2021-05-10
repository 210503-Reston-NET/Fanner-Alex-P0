using System.Collections.Generic;
namespace DSModels
{
    /// <summary>
    /// Class representing a store manager with their name and
    /// the stores that they manage.
    /// </summary>
    public class DogManager : UserInterface
    {
        /// <summary>
        /// String with manager's name.
        /// </summary>
        /// <value></value>
        public string Name { get; set; }
        /// <summary>
        /// List of the stores that the manager manages.
        /// </summary>
        /// <value></value>
        public List<StoreLocation> ManagedStores{get; set;}

    }
}