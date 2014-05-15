using System;
using System.Collections.Generic;
using IssueModule.API.Core;
using NSimpleWebAppHttpHandler;

namespace IssueModule.API.Engine
{
	public class IssueListEngine
	{
		public List<Issue> GetIssuesAssignedToUser(int userId)
		{
			return new List<Issue>
			{
				new Issue {Id = 11, Title = "Issue title", Deadline = new DateTime(2014, 12, 31)},
				new Issue {Id = 15, Title = "Another issue", Deadline = new DateTime(2015, 1, 15)}
			};
		}

	}
}
