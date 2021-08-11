using Microsoft.AspNetCore.Identity;
using System;
using System.Linq.Expressions;

namespace SmartWork.Core.Specifications.UserSpecification
{
    public class UserSpecification<TEntity>
        where TEntity : IdentityUser
    {
        public Expression<Func<TEntity, bool>> Expression { get; }
        public Func<TEntity, bool> Func => this.Expression.Compile();

        public UserSpecification(Expression<Func<TEntity, bool>> expression)
        {
            this.Expression = expression;
        }
        public bool IsSatisfiedBy(TEntity entity)
        {
            return this.Func(entity);
        }
    }
}