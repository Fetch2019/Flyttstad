using Flyttstad.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Flyttstad
{
    public class DataService
    {
        public Offer GetOffer(PostData data)
        {
            var options = new List<Options>();
            var city = new Data().Cities.Where(x => x.Id == Convert.ToInt32(data.CityId)).Single();

            if (data.Options != null)
            {
                options.AddRange(city.Options.FindAll(x => data.Options.Any(y => y.Id == x.Id)));
            }

            return new Offer
            {
                City = city.Name,
                SquareMeter = data.SquareMeter,
                Options = options,
                Price = Calculate(data.SquareMeter, city, data.Options)
            };
        }

        public List<Options> GetOptions(int id)
        {
            var values = new Data().Cities.Where(x => x.Id == id).SelectMany(x => x.Options);

            if (id == 1)
                return values.Where(x => x.Id != 3).ToList(); 

            return values.ToList();
        }

        private int Calculate(int squareMeter, City city, List<Options> options)
        {
            var optionPrice = 0;
            var cityPrice = city.SquareMeterPrice;

            foreach (var option in city.Options)
            {
                if (options != null && options.Any(x => x.Id == option.Id))
                {
                    optionPrice += city.Options.Single(x => x.Id == option.Id).Price;
                }
            }

            return squareMeter * cityPrice + optionPrice;
        }
    }
}