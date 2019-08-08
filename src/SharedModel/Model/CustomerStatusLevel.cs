using System;
using System.Collections.Generic;
using System.Text;

namespace SharedModel.Model
{
    public class CustomerStatusLevel
    {
        public string Name { get; set; }

        /// <summary>
        /// Level of status. Lowest level is 0
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// How many orders must the customer have placed (within the numberofdaysCriteria) to achive this status
        /// </summary>
        public int NumberOfOrdersCriteria { get; set; }

        /// <summary>
        /// Within how many days must the customer have bought to achive this status
        /// </summary>
        public int NumberOfDaysCriteria { get; set; }


        /// <summary>
        /// The minimum amount the customer must have spent (within the numberofdaysCriteria) to achive this status
        /// </summary>
        public double MinimumAmountCriteria { get; set; }


        /// <summary>
        /// DiscountPercentage percentage of this status
        /// </summary>
        public double Discount { get; set; }
    }
}
