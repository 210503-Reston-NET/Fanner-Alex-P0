namespace DSModels
{
    /// <summary>
    /// Class representing the order a customer places at a store.
    /// </summary>
    public class DogOrder
    {
        /// <summary>
        /// Customer ordering the dogs, represented by DogBuyer.
        /// </summary>
        /// <value></value>
        public DogBuyer DogBuyer{get; set;}
        /// <summary>
        /// StoreLocation that the customer is ordering from.
        /// </summary>
        /// <value></value>
        public StoreLocation StoreLocation{get; set;}
        /// <summary>
        /// Double representing the total of the order.
        /// </summary>
        /// <value></value>
        public double Total {get;set;}
    }
}