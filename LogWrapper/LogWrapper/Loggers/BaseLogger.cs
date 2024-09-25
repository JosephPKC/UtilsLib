namespace LogWrapper.Loggers
{
	internal abstract class BaseLogger(Type pDeclaringType)
	{
		protected static readonly string[] _levels = ["DEBUG", "ERROR", "FATAL", "INFO", "WARN"];
		protected readonly Type _declaringType = pDeclaringType;
	}
}
