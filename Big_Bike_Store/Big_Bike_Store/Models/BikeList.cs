using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Big_Bike_Store.Models
{
    public class BikeList : AdventureWorksLT2017gr1Context
    {

        public string ProductModel { get; set; }

        public string Description { get; set; }

        public int ProductModelId { get; set; }
    }
}
