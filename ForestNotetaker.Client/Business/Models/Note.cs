using Supabase.Postgrest.Models;

namespace ForestNotetaker.Business.Models;

public class Note : BaseModel
{
    public Guid Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
