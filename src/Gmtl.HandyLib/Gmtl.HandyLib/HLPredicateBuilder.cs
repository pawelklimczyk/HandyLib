// -------------------------------------------------------------------------------------------------------------------
// <copyright company="Gemotial" file="HLPredicateBuilder.cs" project="Gmtl.HandyLib" date="2021-03-12 06:21">
// 
// </copyright>
// -------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Linq.Expressions;

namespace Gmtl.HandyLib
{
    /// <summary>
    /// Expression predicate builder
    /// </summary>
    /// <remarks>
    /// <code>
    /// Expression&lt;Func&lt;OfferMailingSubscription, bool&gt;&gt; filter = s =&gt; EF.Functions.ILike(s.Email, subs.Email);
    /// 
    /// if (!string.IsNullOrWhiteSpace(subs.Location))
    ///     filter = HLPredicateBuilder.And(filter, s =&gt; EF.Functions.ILike(s.Location, subs.Location));
    ///
    /// if (!string.IsNullOrWhiteSpace(subs.Position))
    ///     filter = HLPredicateBuilder.And(filter, s =&gt; EF.Functions.ILike(s.Position, subs.Position));
    /// </code>
    /// </remarks>
    public static class HLPredicateBuilder
    {
        public static Expression<Func<T, bool>> True<T>() { return f => true; }
        public static Expression<Func<T, bool>> False<T>() { return f => false; }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1,
                                                            Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                  (Expression.OrElse(expr1.Body, invokedExpr), expr1.Parameters);
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1,
                                                             Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                  (Expression.AndAlso(expr1.Body, invokedExpr), expr1.Parameters);
        }
    }
}