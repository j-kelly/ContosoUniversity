namespace ContosoUniversity.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using NRepository.Core;
    using NRepository.Core.Query;

    public class FindByIdsSpecificationStrategy<TEntity> : QueryStrategy where TEntity : class
    {
        public FindByIdsSpecificationStrategy(Expression<Func<TEntity, object>> propertyExpression, params int[] ids)
            : this(propertyExpression, (IEnumerable<int>)ids)
        {
        }

        public FindByIdsSpecificationStrategy(Expression<Func<TEntity, object>> propertyExpression, IEnumerable<int> ids)
        {
            PropertyExpression = propertyExpression;
            Ids = ids;
        }

        public IEnumerable<int> Ids
        {
            get;
        }

        public Expression<Func<TEntity, object>> PropertyExpression
        {
            get;
        }

        public override IQueryable<T> GetQueryableEntities<T>(object additionalQueryData)
        {
            return DynamicContains<T>(
                QueryableRepository.GetQueryableEntities<T>(additionalQueryData),
                PropertyInfo<TEntity>.GetMemberName(PropertyExpression),
                Ids);
        }

        public IQueryable<T> DynamicContains<T>(
            IQueryable<T> query,
            string property,
            IEnumerable<int> items)
        {
            var pe = Expression.Parameter(typeof(T));
            var me = Expression.Property(pe, property);
            var ce = Expression.Constant(items);
            var call = Expression.Call(typeof(Enumerable), "Contains", new[] { me.Type }, ce, me);
            var lambda = Expression.Lambda<Func<T, bool>>(call, pe);
            return query.Where(lambda);
        }
    }
}