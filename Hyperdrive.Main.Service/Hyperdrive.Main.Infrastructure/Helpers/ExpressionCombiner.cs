using System;
using System.Linq;
using System.Linq.Expressions;

namespace Hyperdrive.Main.Infrastructure.Helpers;

/// <summary>
/// Represents a <see cref="ExpressionCombiner"/> class.
/// </summary>
public static class ExpressionCombiner
{
    /// <summary>
    /// Combines Expressions With And
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="expressions">Injected <see cref="Expression{Func{T, bool}}[]"/></param>
    /// <returns>Instance of <see cref="Expression{Func{T,bool}}"/></returns>
    public static Expression<Func<T, bool>> CombineWithAnd<T>(params Expression<Func<T, bool>>[] expressions)
    {
        if (expressions is { Length: 0 }) return _ => true;

        var parameter = Expression.Parameter(typeof(T), "x");

        var body = expressions
            .Select(expr => Expression.Invoke(expr, parameter))
            .Aggregate<Expression, Expression>(null, (current, invoked) =>
                current == null ? invoked : Expression.AndAlso(current, invoked));

        return Expression.Lambda<Func<T, bool>>(body, parameter);
    }

    /// <summary>
    /// Combines Expressions With Or
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="expressions">Injected <see cref="Expression{Func{T, bool}}[]"/></param>
    /// <returns>Instance of <see cref="Expression{Func{T,bool}}"/></returns>
    public static Expression<Func<T, bool>> CombineWithOr<T>(params Expression<Func<T, bool>>[] expressions)
    {
        if (expressions is { Length: 0 }) return _ => false;

        var parameter = Expression.Parameter(typeof(T), "x");

        var body = expressions
            .Select(expr => Expression.Invoke(expr, parameter))
            .Aggregate<Expression, Expression>(null, (current, invoked) =>
                current == null ? invoked : Expression.OrElse(current, invoked));

        return Expression.Lambda<Func<T, bool>>(body, parameter);
    }

}
