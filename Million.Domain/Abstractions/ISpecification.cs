using System.Linq.Expressions;

namespace Million.Domain.Abstractions
{
    public interface ISpecification<TEntity>
    {
        public abstract Expression<Func<TEntity, bool>> ToExpression();
    }
}
