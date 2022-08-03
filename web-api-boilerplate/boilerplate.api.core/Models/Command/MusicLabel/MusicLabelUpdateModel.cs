namespace boilerplate.api.core.Models
{
    public record MusicLabelUpdateModel: MusicLabelCreateModel
    {
        public int Id { get; init; }
    }
}
