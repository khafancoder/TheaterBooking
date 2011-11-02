using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheaterBooking.Entities
{
    [Serializable]
    public class SalonSeatLayout : BaseEntity<int>
    {
        public virtual int SalonSeatId { get; set; }
        public virtual Salon Salon{ get; set; }
        public virtual int RowIndex { get; set; }
        public virtual int SeatsCount { get; set; }
        
        public override int ItemId
        {
            get { return this.SalonSeatId ;  }
        }
    }
}
