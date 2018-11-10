using System;
using System.Linq;

namespace Veondo.Common.Data
{
	public interface IUnitOfWork : IDisposable
	{
		void Commit();
		void Attach<T>( T obj ) where T : class;
		void Add<T>( T obj ) where T : class;
		IQueryable<T> GetAll<T>() where T : class;
		bool Remove<T>( T item ) where T : class;
	}
}