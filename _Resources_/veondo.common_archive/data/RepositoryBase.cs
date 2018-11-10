using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Veondo.Common.Data
{
	public abstract class RepositoryBase<T> : IRepository<T> where T : class
	{
		internal RepositoryBase() { }

		internal RepositoryBase( VeondoContext context )
		{
			_context												= context;
		}

		private VeondoContext _context;
		public VeondoContext Context
		{
			get { return _context ?? ( _context = new VeondoContext() ); }
		}

		public IQueryable<T> FindAll()
		{
			return Context.GetAll<T>();
		}

		public IQueryable<T> FindAllIncluding( params Expression<Func<T, object>>[] includeProperties )
		{
			var query												= Context.GetAll<T>();

			foreach ( var includeProperty in includeProperties ) {
				query												= query.Include( includeProperty );
			}

			return query;
		}

		public IEnumerable<T> FindBy( Func<T, bool> predicate )
		{
			return Context.GetAll<T>().Where( predicate );
		}

		public T Find( int id )
		{
			return Context.Set<T>().Find( id );
		}

		public int Count
		{
			get { return Context.GetAll<T>().Count(); }
		}

		public bool Contains( T item )
		{
			return Context.GetAll<T>().FirstOrDefault( t => t == item ) != null;
		}

		public abstract void InsertOrUpdate( T item );

		public void Add( T item )
		{
			Context.Add( item );
		}

		public void Remove( T item )
		{
			Context.Remove( item );
		}

		public void Delete( int id )
		{
			var entity												= Context.Set<T>().Find( id );
			Context.Remove( entity );
		}

		public void Save()
		{
			Context.SaveChanges();
		}
	}
}