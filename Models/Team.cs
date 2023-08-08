namespace Models;


public record Team
{
    public int id { get; set; }
    public string name { get; set; } = string.Empty;
    public string code { get; set; } = string.Empty;
    public string country { get; set; } = string.Empty;
    public int founded { get; set; }
    public bool national { get; set; }
    public string logo { get; set; } = string.Empty;
}

