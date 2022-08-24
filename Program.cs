using System;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography;
using System.Xml.Linq;

namespace CarAppV2
{
    class Program
    {
        /*
         * Instantiate the carsDatabase class in a variable.
         * The CarDB class Add 10 cars via class constructor.
        */
        static List<Car> carList = new CarsDatabase();
                
        static void Main(string[] args)
        {
            bool showMenu = true;
            while (showMenu)
            {
                showMenu = MainMenu();
            }
        }

        private static bool MainMenu()
        {
            Console.Clear();
            Console.WriteLine("Choose an option:");
            Console.WriteLine(" 1) Add a Car");
            Console.WriteLine(" 2) Which Make appears the most?");
            Console.WriteLine(" 3) List of every car and its properties");
            Console.WriteLine(" 4) What's the largest engine?");
            Console.WriteLine(" 5) What's the smallest engine?");
            Console.WriteLine(" 6) What's the oldest year?");
            Console.WriteLine(" 7) What's the midle year?");
            Console.WriteLine(" 8) What's the newest year?");
            Console.WriteLine(" 9) What's the total amount of cars?");
            Console.WriteLine("10) Show only the model names, and the year");
            Console.WriteLine("11) Show the year, model, and make");
            Console.WriteLine("12) What would the model name be if printed in reverse?");
            Console.WriteLine("13) Print out all the Chevy cars and all the properties, BUT, change Chevy to Chevrolet");
            Console.WriteLine("14) Print out the years but add a space between each number");
            Console.WriteLine(" 0) Exit");
            Console.Write("\r\nSelect an option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    AddCar();
                    return true;
                case "2":
                    MakeCount();
                    return true;
                case "3":
                    ListCars("All");
                    return true;
                case "4":
                    MaxValue("EngineSize");
                    return true;
                case "5":
                    MinValue("EngineSize");
                    return true;
                case "6":
                    MinValue("Year");
                    return true;
                case "7":
                    AvgValue("Year");
                    return true;
                case "8":
                    MaxValue("Year");
                    return true;
                case "9":
                    CarCount();
                    return true;
                case "10":
                    ListCars("MOY");
                    return true;
                case "11":
                    ListCars("YMOMA");
                    return true;
                case "12":
                    ReverseModel();
                    return true;
                case "13":
                    Console.WriteLine("BEFORE UPDATE");
                    SearchCar("Chevy");
                    UpdateCar("Chevy", "Chevrolet");
                    Console.WriteLine("***------------------------------------------------------------------------***");
                    Console.WriteLine("AFTER UPDATE");
                    SearchCar("Chevrolet");
                    Console.ReadLine();
                    return true;
                case "14":
                    YearSpace();
                    return true;
                case "0":
                    return false;
                default:
                    return true;
            }
        }
      
        public static void AddCar()
        {
            //*--- 1) Add a Car ---*

            string make, model, s_year, s_engineSize;
            int year, engineSize;

            Console.WriteLine("Add a car to the database");

            Console.Write("\r\nCar Company: ");
            make = Console.ReadLine();

            while (!System.Text.RegularExpressions.Regex.IsMatch(make, @"^[a-zA-Z]+$"))            
            {
                Console.Write("The Company accepts only alphabetical characters");
                Console.Write("\r\nCar Company: ");
                make = Console.ReadLine();
            }

            Console.Write("\r\nCar Model: ");
            model = Console.ReadLine();
            
            while (!(model.All(c => Char.IsLetterOrDigit(c))))
            {
                Console.Write("The model does not allow special characters");
                Console.Write("\r\nCar Model: ");
                model = Console.ReadLine();
            }

            Console.Write("\r\nCar Year: ");
            s_year = Console.ReadLine();

            bool isNumber = int.TryParse(s_year, out year);

            while (!isNumber || (isNumber && !(year >= 1900 && year <= 9999)))
            {
                Console.WriteLine("The Year value should be a valid number between [1900-9999]");
                Console.Write("\r\nCar Year: ");
                s_year = Console.ReadLine();

                isNumber = int.TryParse(s_year, out year);
            }

            Console.Write("\r\nCar Engine Size: ");
            s_engineSize = Console.ReadLine();

            while (!(int.TryParse(s_engineSize, out engineSize)))
            {
                Console.WriteLine("The Engine size value should be a valid number");
                Console.Write("\r\nCar Engine Size: ");
                s_engineSize = Console.ReadLine();
            }

            carList.Add(
                new Car() { Make = make, Model = model, Year = year, EngineSize = engineSize });

            var addedCar = carList.Where(car => car.Make == make)
                                  .Where(car => car.Model == model)
                                  .Where(car => car.Year == year)
                                  .Where(car => car.EngineSize == engineSize).ToList();

            foreach( var item in addedCar)
            {
                Console.WriteLine("The Car was successfully added...");
                Console.WriteLine("*-----------------------------------------------------*");                
                Console.WriteLine("Make: {0}",item.Make);
                Console.WriteLine("Model: {0}", item.Model);
                Console.WriteLine("Year: {0}",item.Year);
                Console.WriteLine("Engine Size: {0}",item.EngineSize);

                //Avoid duplicate records, because the car information doesn't have an ID.
                break;
                
            }

            Console.ReadLine();
            
        }
        
        public static void MakeCount()
        {
            //*--- 2) Which Make appears the most? ---*
            var makeCount = carList.GroupBy(car => car.Make)
                                .OrderByDescending(makeGroup => makeGroup.Count())
                                .Select(
                                    makeGroup =>
                                        new
                                        {
                                            Make = makeGroup.Key,
                                            Count = makeGroup.Count()
                                        }
                                ).ToList();

            Console.WriteLine("Make \t\t Total of makes");
            foreach (var item in makeCount)
            {
                Console.WriteLine(item.Make + " \t\t " + item.Count);
                break;
            }

            Console.ReadLine();
        }

        public static void ListCars(string combination)
        {
            //*---  3) List of every car and its properties ---*
            //*---  9) Show only the model names, and the year ---*
            //*--- 10) Show the year, model, and make ---*            

            if (combination == "All")
                Console.WriteLine("Make \t\t\t Model \t\t\t Year \t\t Engine Size");
            else if (combination == "MOY")
                Console.WriteLine("Model \t\t\t Year");
            else if (combination == "YMOMA")
                Console.WriteLine("Year \t\t\t Model \t\t\t Make");

            foreach (var item in carList)
            {               
                if (combination == "All")
                    Console.WriteLine("{0} \t\t {1} \t\t {2} \t\t {3}", item.Make.PadRight(10), item.Model.PadRight(10), item.Year, item.EngineSize);
                else if (combination == "MOY")
                    Console.WriteLine("{0} \t\t {1}", item.Model.PadRight(10), item.Year);
                else if (combination == "YMOMA")
                    Console.WriteLine("{0} \t\t {1} \t\t {2}", item.Year.ToString().PadRight(10), item.Model.PadRight(10), item.Make);
            }

            Console.ReadLine();
        }

        public static void MaxValue(string columnName)
        {
            //*--- 4) What's the largest engine? ---*
            //*--- 8) What's the newest year? ---*

            int max = 0;

            if (columnName == "EngineSize")
                max = carList.Select(car => car.EngineSize).Max();
            else if (columnName == "Year")
                max = carList.Select(car => car.Year).Max();

            Console.WriteLine("The largest value for column {0} is: {1}", columnName, max);

            Console.ReadLine();
        }
        
        public static void MinValue(string columnName)
        {
            //*--- //5) What's the smallest engine? ---*

            int min = 0;

            if (columnName == "EngineSize")
                min = carList.Select(car => car.EngineSize).Min();
            else if (columnName == "Year")
                min = carList.Select(car => car.Year).Min();

            Console.WriteLine("The smallest value for column {0} is: {1}",columnName,min);

            Console.ReadLine();
        }
        
        public static void AvgValue(string columnName)
        {
            //*--- 7) What's the middle year? ---*
            
            double avg = carList.Select(car => car.Year).Average();

            Console.WriteLine("The middle {0} is: {1:F0}", columnName, avg);
            Console.ReadLine();
        }
        
        public static void CarCount()
        {
            //*--- 9) What's the total amount of cars? ---*
            
            int count = carList.Count();
            Console.WriteLine("Total amount of cars: " + count);
            Console.ReadLine();
        }
        
        public static void ReverseModel()
        {
            //*--- 12) What would the model name be if printed in reverse? ---*
            
            var reverseModel = carList.Select(car => car.Model).ToList();

            Console.Clear();

            Console.WriteLine("*--- BEFORE REVERSE ---*");
            foreach (var item in reverseModel)
                Console.WriteLine(item);

            Console.WriteLine("*-----------------------------------------------------*");

            Console.WriteLine("*--- CAR MODEL IN REVERSE ---*");
            foreach (var item in reverseModel)
            {
                string itemString = item.ToString();
                char[] cArray = itemString.ToCharArray();
                Array.Reverse(cArray);

                Console.WriteLine(cArray);
               
            }

            Console.ReadLine();
        }
        
        public static void SearchCar(string search_value)
        {
            //*--- 13) Print out all the Chevy cars and all the properties, BUT, change Chevy to Chevrolet ---*
           
            var items = carList.Where(car => (car.Make == search_value)).ToList();                            
            
            Console.WriteLine("Make \t\t\t Model \t\t\t Year \t\t Engine Size");
            foreach (var item in items)
                Console.WriteLine("{0} \t\t {1} \t\t {2} \t\t {3}",item.Make.PadRight(10), item.Model.PadRight(10), item.Year, item.EngineSize);
        }

        public static void UpdateCar(string currentValue, string newValue)
        {
            //*--- 13) Print out all the Chevy cars and all the properties, BUT, change Chevy to Chevrolet ---*

            var items = carList.Where(car => (car.Make == currentValue)).ToList();

            foreach (var item in items)
                item.Make = newValue;
        }
        
        public static void YearSpace()
        {
            //*--- 14) Print out the years but add a space between each number ---*
            var yearSpace = carList.Select(car => car.Year).ToList();

            foreach (var item in yearSpace)
            {
                string itemString = item.ToString();
                char[] cArray = itemString.ToCharArray();
                Console.WriteLine(String.Join(" ", cArray));                
            }

            Console.ReadLine();
        }
    }
 }