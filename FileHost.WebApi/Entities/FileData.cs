using MongoDB.Bson.Serialization.Attributes;

namespace FileHost.WebApi.Entities
{
    public class FileData
    {
        [BsonId]
        public string? Id { get; set; }
        public string? FileName { get; set; }
        public long? Size { get; set; }
        public string? Format { get; set; }
        public string? Source { get; set; }
        public byte[]? Data { get; set; }
        public DateTime? StorageTime { get; set; }
    }
}
