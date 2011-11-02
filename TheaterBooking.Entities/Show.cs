using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace TheaterBooking.Entities
{
    [Serializable]
    public class Show : BaseEntity<int>
    {
        public virtual  int ShowId { get; set; }
        
        [Required]
        public virtual string Title { get; set; }
        public virtual string ImageUrl { get; set; }
        public virtual string Description { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual DateTime CreationDate { get; set; }
        public virtual IList<ShowTime> ShowTimes{ get; set; }

        public override int ItemId
        {
            get { return this.ShowId;  }
        }
    }
}
