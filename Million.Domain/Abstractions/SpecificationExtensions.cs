using System.Linq.Expressions;

namespace Million.Domain.Abstractions
{
    public static class SpecificationExtensions
    {
        public static BaseSpecification<TEntity> And<TEntity>(this BaseSpecification<TEntity> first, BaseSpecification<TEntity> second)
        {
            return new AndSpecification<TEntity>(first, second);
        }
    }

    public class AndSpecification<TEntity> : BaseSpecification<TEntity>
    {
        private readonly BaseSpecification<TEntity> _first;
        private readonly BaseSpecification<TEntity> _second;

        public AndSpecification(BaseSpecification<TEntity> first, BaseSpecification<TEntity> second)
        {
            _first = first;
            _second = second;
        }

        public override Expression<Func<TEntity, bool>> ToExpression()
        {
            var firstExpression = _first.ToExpression();
            var secondExpression = _second.ToExpression();

            var parameter = Expression.Parameter(typeof(TEntity));

            var body = Expression.AndAlso(
                Expression.Invoke(firstExpression, parameter),
                Expression.Invoke(secondExpression, parameter)
            );

            return Expression.Lambda<Func<TEntity, bool>>(body, parameter);
        }
    }
}
