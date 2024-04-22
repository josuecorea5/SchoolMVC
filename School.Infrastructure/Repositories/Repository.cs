using Microsoft.EntityFrameworkCore;
using School.Infrastructure.Data;
using System.Linq.Expressions;

namespace School.Infrastructure.Repositories
{
	public class Repository<T> : IRepository<T> where T : class
	{
		private readonly ApplicationDbContext _context;
		internal DbSet<T> dbSet;

		public Repository(ApplicationDbContext context)
		{
			_context = context;
			dbSet = _context.Set<T>();
		}

		public void Add(T item)
		{
			dbSet.Add(item);
		}

		public T Get(Expression<Func<T, bool>> predicate, string? includeProperties = null)
		{
			IQueryable<T> query = dbSet;

			if(!string.IsNullOrWhiteSpace(includeProperties))
			{
				foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(includeProperty);
				}
			}

			query = query.Where(predicate);

			return query.FirstOrDefault();
		}

		public IEnumerable<T> GetAll(string? includeProperties = null)
		{
			IQueryable<T> query = dbSet;

			if(!string.IsNullOrEmpty(includeProperties))
			{
				foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(includeProperty);
				}
			}

			return query.ToList();
		}

		public void Remove(T item)
		{
			dbSet.Remove(item);
		}
	}
}
