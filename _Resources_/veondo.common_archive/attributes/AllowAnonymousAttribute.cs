using System;

namespace Veondo.Common.Attributes
{
	[AttributeUsage( AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true )]
	public sealed class AllowAnonymousAttribute : Attribute { }
}