using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.RequestFeatures
{
    public abstract class RequestParameters
    {
        const int maxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 10;

        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }
        public string OrderBy { get; set; }
        public string Fields { get; set; }

    }

    public class EndeavourParameters : RequestParameters
    {
        public EndeavourParameters()
        {
            OrderBy = "name";
        }
        public decimal MinGoal { get; set; }
        public decimal MaxGoal { get; set; } = decimal.MaxValue;

        public bool ValidGoalRange => MaxGoal > MinGoal;
        public string SearchTerm { get; set; }

    }

   

}
