using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace TheaterBooking.Entities
{
    [Serializable]
    public class Salon : BaseEntity<int>
    {
        public virtual int SalonId { get; set; }

        [Required]
        public virtual string Title { get; set; }
        [Required]
        public virtual string Address { get; set; }
        [Required]
        public virtual string Phone { get; set; }

        public virtual IList<SalonSeatLayout> SeatRows{ get; set; }

        public override int ItemId
        {
            get { return this.SalonId;  }
        }
    }
}
