namespace DSModels
{
    /// <summary>
    /// Class for each customer, implements UserInterface
    /// </summary>
    public class DogBuyer:UserInterface
    {
        /// <summary>
        /// String representing the customer's name.
        /// </summary>
        /// <value></value>
        public string Name {get; set;}
        /// <summary>
        /// Overriding ToString method to return string representing customer
        /// </summary>
        /// <returns>Customer's name</returns>
        public override string ToString()
        {
            return this.Name;
        }
    }
}