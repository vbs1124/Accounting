using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vserv.Accounting.Data.Entity.Models
{
    public class FinancialPeriod : IEquatable<FinancialPeriod>
    {
        public int Year { get; set; }
        public int Month { get; set; }

        public bool Equals(FinancialPeriod other)
        {
            //Check whether the compared object is null.
            if (Object.ReferenceEquals(other, null)) return false;

            //Check whether the compared object references the same data.
            if (Object.ReferenceEquals(this, other)) return true;

            //Check whether the products' properties are equal.
            return Month.Equals(other.Month) && Year.Equals(other.Year);
        }

        // If Equals() returns true for a pair of objects 
        // then GetHashCode() must return the same value for these objects.

        public override int GetHashCode()
        {
            //Get hash code for the Name field if it is not null.
            int hashProductName = Year == null ? 0 : Year.GetHashCode();

            //Get hash code for the Code field.
            int hashProductCode = Month.GetHashCode();

            //Calculate the hash code for the product.
            return hashProductName ^ hashProductCode;
        }
    }
}
