namespace ExceptionExts.Extensions.Numeric
{
    public interface INumericExceptions<TNum> where TNum : struct
    {
        void Negative();
    }
}
