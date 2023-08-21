namespace Pronia.Core.Entities;

public class Service:BaseEntity
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string IMageUrl { get; set; } = null!;
}
