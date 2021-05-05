using System;

namespace DSUI
{
    class Program
    {
        static void Main(string[] args)
        {
            bool repeat = true;
            do{
                Console.WriteLine("Welcome to the Dog Store!");
                Console.WriteLine("How can I Help you?");
                Console.WriteLine("[0] see list of stores");
                Console.WriteLine("[1] see a shop's inventory");
                Console.WriteLine("[2] order a random dog");
                string input = Console.ReadLine();
                switch(input){
                    case "0":
                        ViewStoreList();
                        break;
                    case "1":
                        ViewStoreInv();
                        break;
                    case "2":
                        OrderDog();
                        break;
                    default:
                        repeat = false;
                        break;
                }
            } while(repeat);
        }
        private void ViewStoreList(){}
        private void ViewStoreInv(){}
        private void OrderDog(){}
    }
}
