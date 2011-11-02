using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheaterBooking.Entities
{
    [Serializable]
    public class Reservation : BaseEntity<int>
    {
        public virtual int ReservationId { get; set; }

        public virtual ShowTime ShowTime { get; set; }
        public virtual int SeatRow { get; set; }
        public virtual int SeatColumn { get; set; }
        public virtual DateTime ReserveDateTime { get; set; }
        public virtual string ReserveInfo { get; set; }
        public virtual Guid ReservedByUserId { get; set; }

        public override int ItemId
        {
            get { return this.ReservationId; }
        }
    }
}
