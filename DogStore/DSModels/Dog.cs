namespace DSModels
{
    /// <summary>
    /// Class for dogs, allowing them to be sold.
    /// </summary>
    public class Dog
    {
        /// <summary>
        /// Simple constructor for the Dog Class with basic information.
        /// </summary>
        /// <param name="breed"> Breed of the dog</param>
        /// <param name="gender"> Dog's gender</param>
        public Dog(string breed, string gender){
            this.Breed = breed;
            this.Gender = gender;
        }
        /// <summary>
        /// String representing dog's breed.
        /// </summary>
        /// <value></value>
        public string Breed {get; set;}

        /// <summary>
        /// String representing dog's gender.
        /// </summary>
        /// <value></value>
        public string Gender {get; set;}

        /// <summary>
        /// String representing how much the dog costs.
        /// </summary>
        /// <value></value>
        public double Price {get; set;}

        /// <summary>
        /// Overrides ToString() method to return string representation of dog
        /// </summary>
        /// <returns>string representation of dog</returns>
        public override string ToString(){
            return $"Breed: {Breed}, Gender: {Gender}, Price: {Price.ToString()}";
        }
        public bool Equal(Dog d){
            return (d.Breed.Equals(this.Breed))&&(d.Gender.Equals(this.Gender));
        }
    }
}