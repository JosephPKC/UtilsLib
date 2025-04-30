using System.Runtime.CompilerServices;

using ExceptionExts.Extensions.List;
using ExceptionExts.Extensions.Numeric;
using ExceptionExts.Extensions.String;

namespace ExceptionExts
{
    public static class ExceptionExtensions
    {
        public static INullableNumericExceptions ThrowIf(this int? pObj, [CallerArgumentExpression(nameof(pObj))] string pArgName = "")
        {
            return new IntNullableExceptions(pArgName, pObj);
        }

        public static INumericExceptions ThrowIf(this int pObj, [CallerArgumentExpression(nameof(pObj))] string pArgName = "")
        {
            return new IntExceptions(pArgName, pObj);
        }

        public static IListExceptions ThrowIf<TValue>(this ICollection<TValue>? pObj, [CallerArgumentExpression(nameof(pObj))] string pArgName = "")
        {
            return new ListExceptions<TValue>(pArgName, pObj);
        }

        public static IStringExceptions ThrowIf(this string? pObj, [CallerArgumentExpression(nameof(pObj))] string pArgName = "")
        {
            return new StringExceptions(pArgName, pObj);
        }
    }
}
