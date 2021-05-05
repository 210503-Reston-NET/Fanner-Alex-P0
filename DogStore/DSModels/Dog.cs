namespace DSModels
{
    public class Dog
    {

        public Dog(string breed, string gender){
            this.Breed = breed;
            this.Gender = gender;
        }
        public string Breed {get; set;}
        
        public string Gender {get; set;}

        public double Price {get; set;}
        
        public override string ToString(){
            return $"Breed: {Breed}, Gender: {Gender}";
        }
    }
}