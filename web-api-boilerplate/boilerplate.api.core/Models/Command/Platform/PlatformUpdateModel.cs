namespace boilerplate.api.core.Models
{
    public record PlatformUpdateModel: PlatformCreateModel
    {
        public int Id { get; init; }
    }
}
