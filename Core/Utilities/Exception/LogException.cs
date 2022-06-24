[System.Serializable]
public class LogException : System.Exception
{
    public LogException():base("Please select a valid log type") { }
    public LogException(string message) : base(message) { }
    public LogException(string message, System.Exception inner) : base(message, inner) { }
    protected LogException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}