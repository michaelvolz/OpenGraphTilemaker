
using System.Web.Configuration;
namespace Veondo.Common
{
	public static class TeaserUtils
	{
		public static bool ShouldShowOnlyTeaser()
		{
			bool value;
			if ( bool.TryParse( WebConfigurationManager.AppSettings["ShowTeaser"], out value ) ) {
				return value;
			}
			return false;
		}
	}
}