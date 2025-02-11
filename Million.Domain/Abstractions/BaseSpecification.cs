using System.Linq.Expressions;

namespace Million.Domain.Abstractions
{
    public abstract class BaseSpecification<TEntity>
    {
        public BaseSpecification()
        {
        }

        public abstract Expression<Func<TEntity, bool>> ToExpression();

        public bool IsSatisfiedBy(TEntity entity)
        {
            Func<TEntity, bool> predicate = ToExpression().Compile();
            return predicate(entity);
        }
    }
}
