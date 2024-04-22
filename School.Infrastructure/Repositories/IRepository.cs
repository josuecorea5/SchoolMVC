using System.Linq.Expressions;

namespace School.Infrastructure.Repositories
{
	public interface IRepository<T> where T : class
	{
		IEnumerable<T> GetAll(string? includeProperties = null);
		T Get(Expression<Func<T, bool>> predicate, string? includeProperties = null);
		void Add(T item);
		void Remove(T item);
	}
}
