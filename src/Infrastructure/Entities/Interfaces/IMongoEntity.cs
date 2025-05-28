namespace Infrastructure.Entities.Interfaces;

public interface IMongoEntity
{
    public string Id { get; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
