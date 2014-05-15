using System;
using IssueModule.API.Core;
using IssueModule.WebAPI;
using NSimpleWebAppHttpHandler;

namespace IssueModule.API.Engine
{
	[SWAModuleClass("IssueEngine")]
	public class IssueEngine
	{
		public Issue GetIssue(int issueId, IssueModuleUser requestUser)
		{
			return new Issue {Id = 11, Title = "Issue title", Deadline = new DateTime(2014, 12, 31)};
		}
	}
}
