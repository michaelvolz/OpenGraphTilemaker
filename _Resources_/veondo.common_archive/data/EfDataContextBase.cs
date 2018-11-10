using System;
using System.Data.Entity;
using System.Linq;

namespace Veondo.Common.Data
{
	public abstract class EfDataContextBase : DbContext, IUnitOfWork
	{
		public IQueryable<T> GetAll<T>() where T : class
		{
			return Set<T>();
		}

		public bool Remove<T>( T item ) where T : class
		{
			try {
				Set<T>().Remove( item );
			} catch ( Exception ) {
				return false;
			}

			return true;
		}

		public void Commit()
		{
			base.SaveChanges();
		}

		public void Attach<T>( T obj ) where T : class
		{
			Set<T>().Attach( obj );
		}

		public void Add<T>( T obj ) where T : class
		{
			Set<T>().Add( obj );
		}
	}
}