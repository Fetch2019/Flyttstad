using System.Collections.Generic;

namespace Flyttstad.Models
{
    public class Data
    {
        public List<City> Cities => SetCities();

        private List<City> SetCities()
        {
            return new List<City>
            {
                new City { Id = 1, Name = "Stockholm", SquareMeterPrice = 65, Options = GetOptions() },
                new City { Id = 2, Name = "Uppsala", SquareMeterPrice = 55, Options = GetOptions()}
            };
        }

        private List<Options> GetOptions()
        {
            return new List<Options>
            {
                new Options { Id = 1, Name = "Fönsterputs", Price = 300},
                new Options { Id = 2, Name = "Balkongstädning", Price = 150},
                new Options { Id = 3, Name = "Bortforsling av skräp", Price = 400}
            };
        }
    }

    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SquareMeterPrice { get; set; }
        public List<Options> Options { get; set; }
    }

    public class Options
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
    }

    public class PostData
    {
        public string CityId { get; set; }
        public int SquareMeter { get; set; }
        public List<Options> Options { get; set; }
    }

    public class Offer
    {
        public string City { get; set; }
        public int SquareMeter { get; set; }
        public List<Options> Options { get; set; }
        public int Price { get; set; }
    }
}