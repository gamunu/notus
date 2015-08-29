using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace Notus.Web.Core.Extensions
{
    public static class HtmlExtensions
    {
        public static MvcHtmlString File(this HtmlHelper html, string name)
        {
            var tb = new TagBuilder("input");
            tb.Attributes.Add("type", "file");
            tb.Attributes.Add("name", name);
            tb.GenerateId(name);
            return MvcHtmlString.Create(tb.ToString(TagRenderMode.SelfClosing));
        }

        public static string LimitLength(this string str, int length)
        {
            if (str.Length > length)
            {
                return str.Substring(0, length) + ".. ";
            }
            return str;
        }


        public static MvcHtmlString FileFor<TModel, TProperty>(this HtmlHelper<TModel> html,
            Expression<Func<TModel, TProperty>> expression)
        {
            var name = GetFullPropertyName(expression);
            return html.File(name);
        }

        #region Helpers

        private static string GetFullPropertyName<T, TProperty>(Expression<Func<T, TProperty>> exp)
        {
            MemberExpression memberExp;

            if (!TryFindMemberExpression(exp.Body, out memberExp))
                return string.Empty;

            var memberNames = new Stack<string>();

            do
            {
                memberNames.Push(memberExp.Member.Name);
            } while (TryFindMemberExpression(memberExp.Expression, out memberExp));

            return string.Join(".", memberNames.ToArray());
        }

        private static bool TryFindMemberExpression(Expression exp, out MemberExpression memberExp)
        {
            memberExp = exp as MemberExpression;

            if (memberExp != null)
                return true;

            if (IsConversion(exp) && exp is UnaryExpression)
            {
                memberExp = ((UnaryExpression) exp).Operand as MemberExpression;

                if (memberExp != null)
                    return true;
            }

            return false;
        }

        private static bool IsConversion(Expression exp)
        {
            return (exp.NodeType == ExpressionType.Convert || exp.NodeType == ExpressionType.ConvertChecked);
        }

        #endregion
    }
}