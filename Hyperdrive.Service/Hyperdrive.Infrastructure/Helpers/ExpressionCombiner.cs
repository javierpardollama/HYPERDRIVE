using System;
using System.Linq.Expressions;

namespace Hyperdrive.Infrastructure.Helpers
{
    public static class ExpressionCombiner
    {
        public static Expression<Func<T, bool>> CombineWithAnd<T>(params Expression<Func<T, bool>>[] expressions)
        {
            if (expressions == null || expressions.Length == 0)
                return x => true;

            var parameter = Expression.Parameter(typeof(T), "x");
            Expression body = null;

            foreach (var expr in expressions)
            {
                var invokedExpr = Expression.Invoke(expr, parameter);
                body = body == null ? invokedExpr : Expression.AndAlso(body, invokedExpr);
            }

            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }

        public static Expression<Func<T, bool>> CombineWithOr<T>(params Expression<Func<T, bool>>[] expressions)
        {
            if (expressions == null || expressions.Length == 0)
                return x => false;

            var parameter = Expression.Parameter(typeof(T), "x");
            Expression body = null;

            foreach (var expr in expressions)
            {
                var invokedExpr = Expression.Invoke(expr, parameter);
                body = body == null ? invokedExpr : Expression.OrElse(body, invokedExpr);
            }

            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }
    }
}
