using System;

[System.Serializable]
public class AuthorizationDeniedException : System.Exception
{
	public AuthorizationDeniedException() : base("Authorization Denied") { }

	public AuthorizationDeniedException(string message) : base(message)
	{
	}

	public AuthorizationDeniedException(string message, System.Exception inner) : base(message, inner) { }

	protected AuthorizationDeniedException(
	  System.Runtime.Serialization.SerializationInfo info,
	  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}