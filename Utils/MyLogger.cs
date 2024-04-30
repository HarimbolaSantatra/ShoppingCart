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


	/// <summary>
	/// Debug method print a debug message on the console
	/// <para name="unity"> The name of the class or the method that uses it.</para>
	/// <para name="message">The log message</para>
	/// </summary>
	// TODO: make 'unity' as a property
	public void Debug(string unity, string message)
	{
	    DateTime currentDate = DateTime.Now;
	    Console.WriteLine($"{currentDate} - DEBUG - {unity} : {message}");
	}


	public void Debug(string message)
	{
	    DateTime currentDate = DateTime.Now;
	    Console.WriteLine($"{currentDate} - DEBUG : {message}");
	}

	public void Error(string unity, string message)
	{
	    DateTime currentDate = DateTime.Now;
	    Console.WriteLine($"{currentDate} - ERROR - {unity} : {message}");
	}

    }
}
