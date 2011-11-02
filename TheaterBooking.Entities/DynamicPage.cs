using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace TheaterBooking.Entities
{
    [Serializable]
    public class DynamicPage : BaseEntity<int>
    {
        public virtual int DynamicPageId { get; set; }


        [Required]
        [StringLength(100)]
        public virtual string Name { get; set; }
        
        [Required]
        [StringLength(200)]
        public virtual string Title { get; set; }

        [StringLength(4000)]
        public virtual string Summary { get; set; }

        [Required]
        public virtual string Text { get; set; }
        
        [StringLength(500)]
        public virtual string Tags { get; set; }

        public virtual DateTime CreationDate { get; set; }
        public virtual DateTime LastUpdate { get; set; }

        public override int ItemId
        {
            get { return this.DynamicPageId;  }
        }
    }
}
