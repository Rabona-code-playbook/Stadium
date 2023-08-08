namespace Models;


public record Stadium
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Country { get; init; } = string.Empty;
    public string Team { get; init; } = string.Empty;
    public List<string> Colors { get; init; } = new List<string>();
}

