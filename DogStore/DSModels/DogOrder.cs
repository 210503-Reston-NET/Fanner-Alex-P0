namespace DSModels
{
    public class DogOrder
    {
        public DogBuyer DogBuyer{get; set;}
        public StoreLocation StoreLocation{get; set;}
        public double Total {get;set;}
    }
}