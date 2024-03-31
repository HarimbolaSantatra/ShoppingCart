namespace ShoppingCart.Utils
{

    using System.Globalization;

    public class MyLogger
    {

	private String LogLevel = "debug";

	public MyLogger() {}

	public MyLogger(String logLevel = "debug")
	{
	    this.LogLevel = logLevel;
	}

	public void SetLogLevel(String logLevel)
	{
	    TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
	    this.LogLevel = textInfo.ToLower(logLevel);
	}

	public String getLogLevel() => this.LogLevel;

	public void Debug(string message)
	{
	    DateTime currentDate = DateTime.Now;
	    Console.WriteLine($"{currentDate} - DEBUG : {message}");
	}

    }
}
