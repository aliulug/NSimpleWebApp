using System.Web;

namespace IssueModule.WebAPI
{
	public class SessionHandler
	{
		public static IssueModuleUser SiteUser
		{
			get
			{
				if (HttpContext.Current.Session["IssueModule_User"] == null)
					HttpContext.Current.Session.Add("IssueModule_User", new IssueModuleUser());
				return (IssueModuleUser)HttpContext.Current.Session["IssueModule_User"];
			}
			set
			{
				HttpContext.Current.Session.Add("IssueModule_User", value);
			}
		}
	}
}
