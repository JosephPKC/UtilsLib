namespace JsonParser.Parsers.NullParser
{
	public class NullStringParserFactory : IStringParserFactory
	{
		#region "IStringParserFactory"
		public IStringParser CreateNewParser()
		{
			return new NullParser();
		}
		#endregion
	}
}
