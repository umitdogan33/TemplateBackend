namespace Core.Utilities.Helpers.Email
{
	public class EmailOption
	{
		public string Host { get; set; }
		public int Port { get; set; }
		public int Timeout { get; set; }
		public string YourEmailAddress { get; set; }
		public string YourPassword { get; set; }
	}
}