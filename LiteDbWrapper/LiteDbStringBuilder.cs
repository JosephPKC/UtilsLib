using System.Text;

namespace LiteDbWrapper
{
    internal static class LiteDbStringBuilder
    {
        public static string BuildQueryString(string pColName, string pWhere, string pOrder, int pLimit)
        {
            StringBuilder builder = new();
            builder.Append($"FROM \"{pColName}\"");

            if (!string.IsNullOrWhiteSpace(pWhere))
            {
                builder.Append($" WHERE \"{pWhere}\"");
            }

            if (!string.IsNullOrWhiteSpace(pOrder))
            {
                builder.Append($" ORDER BY \"{pOrder}\"");
            }

            if (pLimit > 0)
            {
                builder.Append($" LIMIT {pLimit}");
            }

            return builder.ToString();
        }

        public static string BuildListString<TItem>(IEnumerable<TItem> pList, string delimiter = ",")
        {
            return string.Join(delimiter, pList.Select(static x => x == null ? "" : x.ToString()));
        }
    }
}
