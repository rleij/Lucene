using System.Collections.Generic;
using System.Linq;

namespace Lucene.Data.Model
{
    public static class CarRepository
    {
        public static Car Get(uint id) => 
            GetAll().SingleOrDefault(x => x.Id.Equals(id));
        
        public static List<Car> GetAll() =>
            new List<Car>
            {
                new Car { Id = 1, Brand = "Mercedes", Type = "C63 AMG", Color = "White"},
                new Car { Id = 2, Brand = "Volvo", Type = "V60", Color = "Blue"},
                new Car { Id = 3, Brand = "Volvo", Type = "XC30", Color = "Black"},
                new Car { Id = 4, Brand = "Porsche", Type = "Panamera", Color = "Red"},
                new Car { Id = 5, Brand = "BMW", Type = "M5", Color = "Green"},
                new Car { Id = 6, Brand = "Nissan", Type = "GTR", Color = "Yellow"},
                new Car { Id = 7, Brand = "Mitsubishi", Type = "EVO", Color = "Purple"}
            };
    }
}