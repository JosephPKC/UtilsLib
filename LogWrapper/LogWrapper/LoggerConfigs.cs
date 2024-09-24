namespace LogWrapper
{
	/// <summary>
	/// Data object that contains configurations or settings for the logger.
	/// Primarily used to customize the logger when constructing one.
	/// </summary>
	public class LoggerConfigs
	{
		/// <summary>
		/// Sets color coding of different log levels.
		/// Default, this is the color scheme:
		/// - Debug - Green
		/// - Error - Red with White background
		/// - Fatal - Darker Red with White background
		/// - Info - White
		/// - Warn - Yellow
		/// </summary>
		public bool UseColorCoding { get; set; } = false;
	}
}
