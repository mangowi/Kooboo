﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.IndexedDB.Query
{
  public  class ExpressionParser
    {

      public static FilterItem ParseEqual(Expression expression)
      {
          BinaryExpression binary = expression as BinaryExpression;

          MemberExpression member = binary.Left as MemberExpression;
         string name =  member.Member.Name;

         ConstantExpression value = binary.Right as ConstantExpression;
         
         return null; 

      }

      public static List<FilterItem> Prase<TValue>(Expression<Predicate<TValue>> predicate)
      {

          if (predicate.NodeType == ExpressionType.Lambda)
          {

              LambdaExpression lambda = predicate as LambdaExpression;

              if (lambda.Body.NodeType == ExpressionType.Equal)
              {
                var x =   ParseEqual(lambda.Body);                   

              }
          }

          //FilterItem item = new FilterItem();

          //item.Compare = Comparer.EqualTo;

          //item.FieldOrProperty = FieldOrPropertyName;
          //item.FieldType = typeof(Guid);
          //IByteConverter<Guid> x = ObjectContainer.getConverter<Guid>();
          //item.Value = x.ToByte(Value);

          //item.Length = 16;

          //this.items.Add(item);

          return null;
      }

   


    }
}
