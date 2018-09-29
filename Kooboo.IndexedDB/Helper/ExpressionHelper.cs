﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.IndexedDB.Helper
{
    public static class ExpressionHelper
    {

        public static string GetFieldName<TValue>(Expression<Func<TValue, object>> expression)
        {
            string fieldname = string.Empty;

            if (expression.Body is MemberExpression)
                fieldname = ((MemberExpression)expression.Body).Member.Name;
            else if (expression.Body is UnaryExpression)
                fieldname = ((MemberExpression)((UnaryExpression)expression.Body).Operand).Member.Name;
            else
            {
                throw new ArgumentException("Expression must represent field or property access.");
            }

            return fieldname; 
        }

    }
}
