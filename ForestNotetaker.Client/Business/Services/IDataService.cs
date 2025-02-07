using ForestNotetaker.Business.Models;

namespace ForestNotetaker.Business.Services;

public interface IDataService
{
	Task<Note[]> GetNotes();
}

public class DataService(Supabase.Client supabaseClient) : IDataService
{
    public async Task<Note[]> GetNotes()
	{
		var notes = await supabaseClient.From<Note>().Select("id, content, created_at").Order("created_at", Supabase.Postgrest.Constants.Ordering.Descending).Limit(10).Get();
		return notes.Models.ToArray();
	}
}
