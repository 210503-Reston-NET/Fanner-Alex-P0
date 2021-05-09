namespace DSModels
{
    public class DogBuyer:UserInterface
    {
        public string Name {get; set;}
        public override string ToString()
        {
            return this.Name;
        }
    }
}