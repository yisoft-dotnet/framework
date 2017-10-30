//      )                             *     
//   ( /(        *   )       (      (  `    
//   )\()) (   ` )  /( (     )\     )\))(   
//  ((_)\  )\   ( )(_)))\ ((((_)(  ((_)()\  
// __ ((_)((_) (_(_())((_) )\ _ )\ (_()((_) 
// \ \ / / (_) |_   _|| __|(_)_\(_)|  \/  | 
//  \ V /  | | _ | |  | _|  / _ \  | |\/| | 
//   |_|   |_|(_)|_|  |___|/_/ \_\ |_|  |_| 
// 
// This file is subject to the terms and conditions defined in
// file 'License.txt', which is part of this source code package.
// 
// Copyright © Yi.TEAM. All rights reserved.
// -------------------------------------------------------------------------------

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

            if (body is MemberExpression) propertyInfo = (body as MemberExpression).Member as PropertyInfo;
            else if (body is UnaryExpression) propertyInfo = ((MemberExpression) ((UnaryExpression) body).Operand).Member as PropertyInfo;

            if (propertyInfo == null) throw new ArgumentException("The lambda expression 'property' should point to a valid Property");

            return propertyInfo.Name;
        }

        public static string GetPropertyName<T>(Expression<Func<T, object>> property)
        {
            PropertyInfo propertyInfo = null;
            var body = property.Body;

            if (body is MemberExpression) propertyInfo = (body as MemberExpression).Member as PropertyInfo;
            else if (body is UnaryExpression) propertyInfo = ((MemberExpression) ((UnaryExpression) body).Operand).Member as PropertyInfo;

            if (propertyInfo == null) throw new ArgumentException("The lambda expression 'property' should point to a valid Property");

            return propertyInfo.Name;
        }
    }
}
