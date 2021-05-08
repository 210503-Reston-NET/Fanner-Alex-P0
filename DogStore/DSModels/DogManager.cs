using System.Collections.Generic;
namespace DSModels
{
    public class DogManager : UserInterface
    {
        public string Name { get; set; }
        public List<StoreLocation> ManagedStores{get; set;}

    }
}