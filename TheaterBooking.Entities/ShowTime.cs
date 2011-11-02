using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheaterBooking.Entities
{
    [Serializable]
    public class ShowTime : BaseEntity<int>
    {
        public virtual int ShowTimeId { get; set; }
        public virtual Salon Salon { get; set; }
        public virtual Show Show { get; set; }
        public virtual DateTime ShowDateTime { get; set; }
        public virtual string ShowDateSh { get; set; }

        public override int ItemId
        {
            get { return this.ShowTimeId; }
        }
    }
}
