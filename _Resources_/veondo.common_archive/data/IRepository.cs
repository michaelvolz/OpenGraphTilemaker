using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Veondo.Common.Data
{
	public interface IRepository<T>
	{
		VeondoContext Context { get; }
		IQueryable<T> FindAll();
		IQueryable<T> FindAllIncluding( params Expression<Func<T, object>>[] includeProperties );
		IEnumerable<T> FindBy( Func<T, bool> predicate );
		T Find( int id );
		int Count { get; }
		bool Contains( T item );
		void InsertOrUpdate( T item );
		void Add( T item );
		void Remove( T item );
		void Delete( int id );
		void Save();
	}
}