using System.Linq.Expressions;
using System.Web.Mvc;

namespace Zek.Extensions.Web
{
    public static class ExpressionExtensions
    {
        public static bool IsBindable(this LambdaExpression expression)
        {
            var nodeType = expression.Body.NodeType;
            if ((nodeType != ExpressionType.MemberAccess) && (nodeType != ExpressionType.Parameter))
            {
                return false;
            }
            return true;
        }

        public static string MemberWithoutInstance(this LambdaExpression expression)
        {
            return ExpressionHelper.GetExpressionText(expression);
        }

        public static MemberExpression ToMemberExpression(this LambdaExpression expression)
        {
            var body = expression.Body as MemberExpression;
            if (body == null)
            {
                var expression3 = expression.Body as UnaryExpression;
                if (expression3 != null)
                {
                    body = expression3.Operand as MemberExpression;
                }
            }
            return body;
        }
    }
}
