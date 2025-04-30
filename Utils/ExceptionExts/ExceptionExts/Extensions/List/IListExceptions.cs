namespace ExceptionExts.Extensions.List
{
    public interface IListExceptions
    {
        void NullOrEmpty();
        void OutOfRange(int pIndex);
    }
}
