namespace JsonParser.Parsers.TextJsonStringParser
{
	/// <summary>
	/// Constructs a TextJsonStringParser.
	/// </summary>
	public class TextJsonStringParserFactory : IStringParserFactory
	{
		#region "IStringParserFactory"
		public IStringParser CreateNewParser()
		{
			return new TextJsonStringParser();
		}
		#endregion
	}
}
