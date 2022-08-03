namespace boilerplate.api.core.Models
{
    public record MusicianUpdateModel: MusicianCreateModel
    {
        public int Id { get; init; }
    }
}
