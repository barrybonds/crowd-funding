using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Dynamic.Core;
using Repository.Extensions.Utility;

namespace Repository.Extensions
{
   public static class RepositoryEndeavourExtensions
    {
        public static IQueryable<Endeavour> FilterEndeavours(this IQueryable<Endeavour> endeavour, decimal minGoal, decimal maxGoal)
                    => endeavour.Where(e => (e.GoalAmount >= minGoal && e.GoalAmount <= maxGoal));
        public static IQueryable<Endeavour> Search(this IQueryable<Endeavour> endeavour, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return endeavour;
            var lowerCaseTerm = searchTerm.Trim().ToLower();
            return endeavour.Where(e => e.Name.ToLower().Contains(lowerCaseTerm));
        }

        //TODO: Install System.Linq.Dynamic.Core
        public static IQueryable<Endeavour> Sort(this IQueryable<Endeavour> endeavours, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return endeavours.OrderBy(e => e.Name);
            var orderParams = orderByQueryString.Trim().Split(',');
            var propertyInfos = typeof(Endeavour).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            var orderQueryBuilder = new StringBuilder();

            foreach (var param in orderParams)
            {
                if (string.IsNullOrWhiteSpace(param))
                    continue;
                var propertyFromQueryName = param.Split(" ")[0];
                var objectProperty = propertyInfos.FirstOrDefault(pi => pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));
                if (objectProperty == null)
                    continue;
                var direction = param.EndsWith(" desc") ? "descending" : "ascending";
                orderQueryBuilder.Append($"{objectProperty.Name.ToString()} {direction},");
            }
            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Endeavour>(orderByQueryString);
            if (string.IsNullOrWhiteSpace(orderQuery))
                return endeavours.OrderBy(e => e.Name);
            return endeavours.OrderBy(orderQuery);
            
        }


    }
}
