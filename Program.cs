using System;
using System.Linq;
using System.Xml.Linq;

namespace CarAppV2
{
    class Program
    {
        /*
         * Instantiate the carsDatabase class in a variable.
         * The CarDB class Add 5 cars via class constructor.
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
            Console.WriteLine(" 6) What's the average year?");
            Console.WriteLine(" 7) What's the newest year?");
            Console.WriteLine(" 8) What's the total amount of cars?");
            Console.WriteLine(" 9) Show only the model names, and the year");
            Console.WriteLine("10) Show the year, model, and make");
            Console.WriteLine("11) What would the model name be if printed in reverse?");
            Console.WriteLine("12) Print out all the Chevy cars and all the properties, BUT, change Chevy to Chevrolet");
            Console.WriteLine("13) Print out the years but add a space between each number");
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
                    AvgValue("Year");
                    return true;
                case "7":
                    MaxValue("Year");
                    return true;
                case "8":
                    CarCount();
                    return true;
                case "9":
                    ListCars("MOY");
                    return true;
                case "10":
                    ListCars("YMOMA");
                    return true;
                case "11":
                    ReverseModel();
                    return true;
                case "12":
                    SearchCar("Chevy", "Chevrolet");
                    return true;
                case "13":
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

            Console.WriteLine("Method to create a car");

            Console.Write("\r\nCar Company: ");
            string make = Console.ReadLine();

            Console.Write("\r\nCar Model: ");
            string model = Console.ReadLine();

            Console.Write("\r\nCar Year: ");
            int year = int.Parse(Console.ReadLine());

            Console.Write("\r\nCar Engine Size: ");
            int engineSize = int.Parse(Console.ReadLine());

            carList.Add(
                new Car() { Make = make, Model = model, Year = year, EngineSize = engineSize });

            ListCars("All");
        }

        
        public static void MakeCount()
        {
            //*--- 2) Which Make appears the most? ---*
            var makeCount = carList.GroupBy(g => g.Make)
                                .OrderByDescending(makeGroup => makeGroup.Count())
                                .Select(
                                    makeGroup =>
                                        new
                                        {
                                            Make = makeGroup.Key,
                                            Count = makeGroup.Count()
                                        }
                                );

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

            var resCars = carList.Select(c => c);

            if (combination == "All")
                Console.WriteLine("Make \t\t Model \t\t Year \t\t Engine Size");
            else if (combination == "MOY")
                Console.WriteLine("Model \t\t Year");
            else if (combination == "YMOMA")
                Console.WriteLine("Year \t\t Model \t\t Make");

            foreach (var item in resCars)
            {
                if (combination == "All")
                    Console.WriteLine("{0} \t\t {1} \t\t {2} \t\t {3}", item.Make, item.Model, item.Year, item.EngineSize);
                else if (combination == "MOY")
                    Console.WriteLine("{0} \t\t {1}", item.Model, item.Year);
                else if (combination == "YMOMA")
                    Console.WriteLine("{0} \t\t {1} \t\t {2}", item.Year, item.Model, item.Make);
            }

            Console.ReadLine();
        }

        public static void MaxValue(string columnName)
        {
            //*--- 4) What's the largest engine? ---*
            //*--- 7) What's the newest year? ---*

            int max =0;

            if (columnName == "EngineSize")
                max = carList.Select(m => m.EngineSize).Max();
            else if (columnName == "Year")
                max = carList.Select(m => m.Year).Max();

            Console.WriteLine("The largest value for column {0} is: {1}", columnName, max);

            Console.ReadLine();
        }
        
        public static void MinValue(string columnName)
        {
            //*--- //5) What's the smallest engine? ---*

            int min = 0;

            if (columnName == "EngineSize")
                min = carList.Select(m => m.EngineSize).Min();
            else if (columnName == "Year")
                min = carList.Select(m => m.Year).Min();

            Console.WriteLine("The smallest value for column {0} is: {1}",columnName,min);

            Console.ReadLine();
        }
        
        public static void AvgValue(string columnName)
        {
            //*--- 6) What's the average year? ---*
            
            double avg = carList.Select(a => a.Year).Average();

            Console.WriteLine("Average value for column {0} is: {1:F0}", columnName, avg);
            Console.ReadLine();
        }
        
        public static void CarCount()
        {
            //*--- 8) What's the total amount of cars? ---*
            
            int count = carList.Count();
            Console.WriteLine("Total amount of cars: " + count);
            Console.ReadLine();
        }
        
        public static void ReverseModel()
        {
            //*--- 11) What would the model name be if printed in reverse? ---*
            
            var reverseModel = carList.Select(r => r.Model);

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
        
        public static void SearchCar(string makeKeyName, string newValue)
        {
            //*--- 12) Print out all the Chevy cars and all the properties, BUT, change Chevy to Chevrolet ---*
           
            var items = carList.Where(s => (s.Make == makeKeyName));                            

            Console.WriteLine("BEFORE UPDATE");
            Console.WriteLine("Make \t\t Model \t\t Year \t\t Engine Size");
            foreach (var item in items)
                Console.WriteLine("{0} \t\t {1} \t\t {2} \t\t {3}",item.Make, item.Model, item.Year, item.EngineSize);

            Console.WriteLine("*-----------------------------------------------------*");

            Console.WriteLine("AFTER UPDATE");
            foreach (var item in items)
                item.Make = newValue;

            ListCars("All");
        }
        
        public static void YearSpace()
        {
            //*--- 13) Print out the years but add a space between each number ---*
            var yearSpace = carList.Select( y => y.Year);

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