using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Xamarin.Forms.Internals;

namespace Xamarin.Forms.Platform.Unity
{
    internal sealed class UnityExpressionSearch : ExpressionVisitor, IExpressionSearch
    {
        private List<object> results;
        private Type targetType;

        public List<T> FindObjects<T>(Expression expression) where T : class
        {
            results = new List<object>();
            targetType = typeof(T);
            Visit(expression);
            return results.Select(o => o as T).ToList();
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            if (node.Expression is ConstantExpression constantExpression && 
                node.Member is FieldInfo fieldInfo)
            {
                var container = constantExpression.Value;
                var value = fieldInfo.GetValue(container);

                if (targetType.IsInstanceOfType(value))
                {
                    results.Add(value);
                }
            }
            return base.VisitMember(node);
        }
    }
}
