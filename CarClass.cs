using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAppV2
{
    public class Car
    {
        public string Make { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int Year { get; set; }
        public int EngineSize { get; set; }
    }

    public class CarsDatabase : List<Car>
    {
        public CarsDatabase()
        {
            Add(new Car() { Make = "Chevy",     Model = "Corvette",     Year = 1967, EngineSize = 350 });
            Add(new Car() { Make = "Pontiac",   Model = "FireBird",     Year = 1967, EngineSize = 400 });
            Add(new Car() { Make = "Dodge",     Model = "Challenger",   Year = 1970, EngineSize = 427 });
            Add(new Car() { Make = "Ford",      Model = "Pinto",        Year = 1976, EngineSize = 208 });
            Add(new Car() { Make = "AlfaRomeo", Model = "Tudela",       Year = 2000, EngineSize = 200 });
            Add(new Car() { Make = "Porche",    Model = "Corcheros",    Year = 2015, EngineSize = 296 });
            Add(new Car() { Make = "Pontiac",   Model = "Trans Am",     Year = 1976, EngineSize = 455 });
            Add(new Car() { Make = "Chevy",     Model = "Camaro",       Year = 2022, EngineSize = 476 });
            Add(new Car() { Make = "Ford",      Model = "Bronco",       Year = 1985, EngineSize = 350 });
            Add(new Car() { Make = "Chevy",     Model = "Cavalier",     Year = 1996, EngineSize = 287 });
        }

    }
}
