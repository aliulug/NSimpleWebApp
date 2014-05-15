using IssueModule.API.Core;
using IssueModule.API.Engine;
using IssueModule.WebAPI;
using IssueModule.WebAPI.IO;
using NSimpleWebAppHttpHandler;

namespace IssueModule.HttpHandler
{
	[SWAModuleClass("IssueHH")]
	public class IssueHttpHandler
	{
		[SWAModuleMethod("GetIssue")]	
		public Issue GetIssue(GetIssueInput input)
		{
			IssueEngine engine = new IssueEngine();
			return engine.GetIssue(input.IssueId, SessionHandler.SiteUser);
		}
	}
}
