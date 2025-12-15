using System;

namespace Hyperdrive.Test.Infrastructure.Extensions;

/// <summary>
/// Represents a <see cref="FluentExtension"/> class.
/// </summary>
public static class FluentExtension
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj">Injected <see cref="T"/></param>
    /// <param name="action">Injected <see cref="Action{T}"/></param>
    /// <returns>Instance of <see cref="T"/></returns>
    public static T Also<T>(this T obj, Action<T> action)
    {
        action(obj);
        return obj;
    }
}
