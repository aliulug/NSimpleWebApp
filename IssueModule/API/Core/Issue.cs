using System;

namespace IssueModule.API.Core
{
	public class Issue
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public DateTime Deadline { get; set; }
	}
}
