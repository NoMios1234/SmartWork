using Microsoft.AspNetCore.Identity;
using System;
using System.Linq.Expressions;

namespace SmartWork.Core.Specifications.UserSpecification
{
    public static class UserSpecificationExtentions
    {
        public static UserSpecification<TEntity> Or <TEntity>(this UserSpecification<TEntity> left, UserSpecification<TEntity> right)
            where TEntity : IdentityUser
        {
            var leftExpr = left.Expression;
            var rightExpr = right.Expression;
            var leftParam = leftExpr.Parameters[0];
            var rightParam = rightExpr.Parameters[0];

            return new UserSpecification<TEntity>(
                Expression.Lambda<Func<TEntity, bool>>(
                    Expression.OrElse(
                        leftExpr.Body,
                        new ParameterReplacer(rightParam, leftParam).Visit(rightExpr.Body)),
                    leftParam));
        }

        public static UserSpecification<TEntity> And<TEntity>(this UserSpecification<TEntity> left, UserSpecification<TEntity> right)
            where TEntity : IdentityUser
        {
            var leftExpr = left.Expression;
            var rightExpr = right.Expression;
            var leftParam = leftExpr.Parameters[0];
            var rightParam = rightExpr.Parameters[0];

            return new UserSpecification<TEntity>(
                Expression.Lambda<Func<TEntity, bool>>(
                    Expression.AndAlso(
                        leftExpr.Body,
                        new ParameterReplacer(rightParam, leftParam).Visit(rightExpr.Body)),
                    leftParam));
        }

        public static UserSpecification<TEntity> Not<TEntity>(this UserSpecification<TEntity> specification)
            where TEntity : IdentityUser
        {
            return new UserSpecification<TEntity>(
                Expression.Lambda<Func<TEntity, bool>>(
                    Expression.Not(specification.Expression.Body),
                    specification.Expression.Parameters));
        }
    }
}