using System;
using System.Collections;
using System.Collections.Generic;


namespace TheaterBooking.Entities
{
    [Serializable]
    public abstract class BaseEntity<TIdentityType>
    {
        [Newtonsoft.Json.JsonIgnore]
        public abstract TIdentityType ItemId { get; }

        #region Equals And HashCode Overrides

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if ((obj == null) || (obj.GetType() != this.GetType())) return false;
            BaseEntity<TIdentityType> castObj = (BaseEntity<TIdentityType>)obj;
            return (castObj != null) &&
                (this.ItemId.Equals(castObj.ItemId));
        }

        public override int GetHashCode()
        {
            int hash = 57;
            hash = 27 * hash * this.ItemId.GetHashCode();

            return hash;
        }

        #endregion

    }
}

