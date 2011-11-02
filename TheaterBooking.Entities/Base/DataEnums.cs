using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheaterBooking.Entities
{
    public class DataEnums
    {
        public enum CategoryType
        {
            BikeSeries = 1,
            Components = 2
        }

        public enum ResourceType
        {
            Image = 1,
            SWF = 2,
            PDF = 3,
            Unknown = 4
        }
    }
}
