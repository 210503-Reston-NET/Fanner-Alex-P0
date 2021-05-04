namespace DSModels
{
    public class Item
    {
        public Item(Dog dog, int quant){
            this.Dog = dog;
            this.Quantity = quant;
        }
        public Dog Dog {get; set; }

        public int Quantity {get; set; }
    }
}