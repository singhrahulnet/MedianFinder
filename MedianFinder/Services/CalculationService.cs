using System;
using System.Collections.Generic;
using System.Linq;

namespace MedianFinder.Services
{
    public interface ICalculationService
    {
        decimal GetMedian(IEnumerable<string> values);
        bool IsValueInMedianRange(decimal median, decimal value, decimal lowerVariancePC, decimal upperVariancePC);
    }

    public class CalculationService : ICalculationService
    {
        public decimal GetMedian(IEnumerable<string> values)
        {
            if (values == null) throw new ArgumentNullException("Collection is null");

            values = values.OrderBy(o => Convert.ToDecimal(o));

            int count = values.Count();
            if (count == 0) throw new InvalidOperationException("Collection is empty");

            //For even count - average the two middle values.
            if (count % 2 == 0)
            {
                decimal mid1 = Convert.ToDecimal(values.ElementAt((count / 2) - 1));
                decimal mid2 = Convert.ToDecimal(values.ElementAt(count / 2));
                return (mid1 + mid2) / 2;
            }

            return Convert.ToDecimal(values.ElementAt(count / 2));
        }

        public bool IsValueInMedianRange(decimal median, decimal value, decimal lowerVariancePC, decimal upperVariancePC)
        {
            decimal MAXLIMIT = median * (1 + (upperVariancePC / 100)); //Get upper bound by upper percentage
            decimal MINLIMIT = median * (1 - (upperVariancePC / 100)); //Get lower bound by lower percentage

            //Determine if the value lies between bounds excluding the median value itself
            return value >= MINLIMIT && value <= MAXLIMIT && value != median;
        }
    }
}
