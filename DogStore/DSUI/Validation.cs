using System.ComponentModel;
using System.Text.RegularExpressions;
using System;
namespace DSUI
{
    public class Validation : IValidation
    {
        public string ValidateAddress(string message)
        {
            //@"^[\w\s]+,\s\w{2}$"
            string enteredString = "";
            bool repeat = true;
            do{
                Console.WriteLine(message);
                try{
                    enteredString = Console.ReadLine();
                    if(Regex.IsMatch(enteredString, @"^[\w\s]+,\s\w{2}$")){
                        repeat = false;
                    }
                    else
                    {
                        Console.WriteLine("Incorrect Format, use Cityname,ST");
                    }
                }catch(Exception e){
                    Console.WriteLine("Not a valid input, please try again");
                }
            }while(repeat);
            return enteredString;
        }

        public double ValidateDouble(string message)
        {
            double enteredDouble = 0;
            bool repeat = true;
            do{
                Console.WriteLine(message);
                try{
                    enteredDouble = Double.Parse(Console.ReadLine());
                    if (enteredDouble > 0)
                    {
                        repeat = false;
                    }
                    else
                    {
                        Console.WriteLine("Must be positive");
                    }
                }catch(Exception e){
                    Console.WriteLine("Not a valid input, please try again");
                }

            }while(repeat);
            return enteredDouble;
        }

        public int ValidateInt(string message)
        {
            throw new System.NotImplementedException();
        }

        public string ValidateString(string message)
        {
            string entererdString = "";
            bool repeat = true;
            do{
                Console.WriteLine(message);
                try{
                    entererdString = Console.ReadLine();
                    if (String.IsNullOrWhiteSpace(entererdString)){
                        Console.WriteLine("Please put in a valid string");
                    }else{
                        repeat = false;
                    }
                } catch(Exception e){
                    Console.WriteLine("Something went wrong, try again");
                }
            }while(repeat);
            return entererdString;
        }
        public string ValidateName(string message)
        {
            //@"^[\w\s]+,\s\w{2}$"
            string enteredString = "";
            bool repeat = true;
            do{
                Console.WriteLine(message);
                try{
                    enteredString = Console.ReadLine();
                    if(Regex.IsMatch(enteredString, @"^[a-zA-Z]{2,}\s[a-zA-Z]{1,}$")){
                        repeat = false;
                    }
                    else
                    {
                        Console.WriteLine("Incorrect Format, use Firstname Lastname");
                    }
                }catch(Exception e){
                    Console.WriteLine("Not a valid input, please try again");
                }
            }while(repeat);
            return enteredString;
        }

        public long ValidatePhone(string message)
        {
            long phoneNumber = 0;
            string enteredString;
            bool repeat = true;
            do{
                Console.WriteLine(message);
                try{
                    enteredString = Console.ReadLine();
                    if(Regex.IsMatch(enteredString, @"^[0-9]{10}$")){
                        repeat = false;
                        phoneNumber = Int64.Parse(enteredString);
                    }
                    else
                    {
                        Console.WriteLine("Incorrect Format, use 1234567890");
                    }
                }catch(Exception e){
                    Console.WriteLine("Not a valid input, please try again");
                }
            }while(repeat);
            return phoneNumber;
        }
    }
}