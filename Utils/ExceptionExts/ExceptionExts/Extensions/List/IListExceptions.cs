namespace ExceptionExts.Extensions.List
{
    public interface IListExceptions<TValue>
    {
        void NullOrEmpty();
        void OutOfRange(int pIndex);
    }
}
