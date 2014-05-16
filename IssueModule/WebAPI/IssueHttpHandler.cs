using System.Web;
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
		public GetIssueOutput GetIssue(GetIssueInput input)
		{
			IssueEngine engine = new IssueEngine();
			Issue issue = engine.GetIssue(input.IssueId, SessionHandler.SiteUser);
			return issue == null ? new GetIssueOutput{Success = false} : new GetIssueOutput {Success = true, Issue = issue};
		}


	}
}
