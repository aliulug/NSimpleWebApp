using IssueModule.API.Core;
using NSimpleWebAppHttpHandler;

namespace IssueModule.WebAPI.IO
{
	public class GetIssueOutput : SWAHttpRequestResult
	{
		public Issue Issue;
	}
}
