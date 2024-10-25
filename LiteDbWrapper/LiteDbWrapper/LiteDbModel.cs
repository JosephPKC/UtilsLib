using LiteDB;

namespace LiteDbWrapper
{
    /// <summary>
    /// Base model. Extend this to create your documents and collections.
    /// </summary>
    public abstract class LiteDbModel
    {
        [BsonId]
        public required int Id { get; set; }
    }
}
