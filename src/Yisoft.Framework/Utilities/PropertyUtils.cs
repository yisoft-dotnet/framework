// ===============================================================================
// Website: https://yi.team/
// Copyright © Yi.TEAM. All rights reserved.
// ===============================================================================

using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Yisoft.Framework.Utilities
{
    public static class PropertyUtils
    {
        public static string GetPropertyName<T, TProperty>(Expression<Func<T, TProperty>> property)
        {
            PropertyInfo propertyInfo = null;
            var body = property.Body;

            switch (body)
            {
                case MemberExpression memberExpression:
                    propertyInfo = memberExpression.Member as PropertyInfo;

                    break;
                case UnaryExpression unaryExpression:
                    propertyInfo = ((MemberExpression) unaryExpression.Operand).Member as PropertyInfo;

                    break;
            }

            if (propertyInfo == null) throw new ArgumentException("The lambda expression 'property' should point to a valid Property");

            return propertyInfo.Name;
        }

        public static string GetPropertyName<T>(Expression<Func<T, object>> property)
        {
            PropertyInfo propertyInfo = null;
            var body = property.Body;

            switch (body)
            {
                case MemberExpression memberExpression:
                    propertyInfo = memberExpression.Member as PropertyInfo;

                    break;
                case UnaryExpression unaryExpression:
                    propertyInfo = ((MemberExpression) unaryExpression.Operand).Member as PropertyInfo;

                    break;
            }

            if (propertyInfo == null) throw new ArgumentException("The lambda expression 'property' should point to a valid Property");

            return propertyInfo.Name;
        }
    }
}