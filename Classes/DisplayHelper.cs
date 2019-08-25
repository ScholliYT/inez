using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace INEZ.Classes
{
    public static class DisplayHelper
    {
        public static string GetDisplayName<TModel>(Expression<Func<TModel, object>> expression)
        {
            var type = typeof(TModel);

            string propertyName = null;
            string[] properties = null;
            IEnumerable<string> propertyList;
            //unless it's a root property the expression NodeType will always be Convert
            switch (expression.Body.NodeType)
            {
                case ExpressionType.Convert:
                case ExpressionType.ConvertChecked:
                    propertyList =
                        (expression.Body is UnaryExpression ue ? ue.Operand : null)?.ToString().Split(".".ToCharArray())
                        .Skip(1).ToList(); //don't use the root property
                    break;
                default:
                    propertyList = expression.Body.ToString().Split(".".ToCharArray()).Skip(1).ToList();
                    break;
            }

            //the propert name is what we're after
            propertyName = (propertyList ?? throw new InvalidOperationException()).Last();
            //list of properties - the last property name
            properties = propertyList.Take(propertyList.Count() - 1).ToArray(); //grab all the parent properties

            foreach (var property in properties)
            {
                var propertyInfo = type.GetProperty(property);
                if (propertyInfo != null) type = propertyInfo.PropertyType;
            }

            MemberInfo prop = type.GetProperty(propertyName);
            if (prop.GetCustomAttribute(typeof(DisplayNameAttribute)) is DisplayNameAttribute dd) return dd.DisplayName;
            return null;
        }
    }
}