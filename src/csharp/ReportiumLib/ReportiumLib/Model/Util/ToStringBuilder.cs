using System;
using System.Linq.Expressions;
using System.Text;

namespace Reportium.Model.Util
{
    public sealed class ToStringBuilder<T>
    {
        private readonly T obj;
        private readonly Type objType;
        private readonly StringBuilder innerSb;

        public ToStringBuilder(T obj)
        {
            this.obj = obj;
            objType = obj.GetType();
            innerSb = new StringBuilder();
        }

        public ToStringBuilder<T> Append<TProperty>(Expression<Func<T, TProperty>> expression)
        {
            // exit conditions
            if (!TryGetPropertyName(expression, out string propertyName))
            {
                throw new ArgumentException("Expression must be a simple property expression.");
            }

            // build
            var func = expression.Compile();
            if (innerSb.Length < 1)
            {
                innerSb.Append(propertyName).Append(": ").Append(func(obj).ToString());
            }
            else
            {
                innerSb.Append(", ").Append(propertyName).Append(": ").Append(func(obj).ToString());
            }

            // get
            return this;
        }

        private static bool TryGetPropertyName<TProperty>(Expression<Func<T, TProperty>> expression, out string propertyName)
        {
            // exit conditions
            if (expression.Body is not MemberExpression propertyExpression)
            {
                propertyName = string.Empty;
                return false;
            }

            // get
            propertyName = propertyExpression.Member.Name;
            return true;
        }

        public override string ToString()
        {
            return objType.Name + "{" + innerSb.ToString() + "}";
        }
    }
}