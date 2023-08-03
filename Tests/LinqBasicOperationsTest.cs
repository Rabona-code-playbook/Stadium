using Xunit.Abstractions;

namespace Tests;

public class LinqBasicOperationsTest
{
    private readonly ITestOutputHelper output;

    public LinqBasicOperationsTest(ITestOutputHelper testOutputHelper)
    {
        output = testOutputHelper;
    }

    public record Stadium
    {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string Country { get; init; } = string.Empty;
        public string Team { get; init; } = string.Empty;
        public List<string> Colors { get; init; } = new List<string>();
    }

    public record Country
    {
        public string Name { get; init; } = string.Empty;
        public string Code { get; init; } = string.Empty;
    }

    readonly List<string> stadiumNames = new() { "Wembley", "Camp Nou", "Santiago Bernabeu", "Old Trafford", "Maracana", "Allianz Arena", "Signal Iduna Park", "Stadio Giuseppe Meazza", "Estadio Azteca", "Anfield", "Estadio Centenario", "La Bombonera", "Emirates" };

    readonly List<Stadium> stadiums = new()
    {
        new Stadium{ Id = 1, Name = "Wembley", Country= "ENG", Team="England National Team" },
        new Stadium{ Id = 2, Name = "Camp Nou", Country ="SPA", Team="Barcelona", Colors = new List<string>{ "Blue", "Garnet" } },
        new Stadium{ Id = 3, Name = "Santiago Bernabeu", Country ="SPA", Team="Real Madrid", Colors = new List<string>{ "White" } },
        new Stadium{ Id = 4, Name = "Old Trafford",  Country ="ENG", Team="Man U" },
        new Stadium{ Id = 5, Name = "Maracana",  Country ="BRA", Team="Brazil's National Team" },
        new Stadium{ Id = 6, Name = "Allianz Arena",  Country ="GER", Team="Bayern Munich" },
        new Stadium{ Id = 7, Name = "Signal Iduna Park", Country ="GER", Team="Borusia Dortmund", Colors = new List<string>{ "Yellow", "Black" } },
        new Stadium{ Id = 8, Name = "Stadio Giuseppe Meazza", Country ="ITA", Team="Inter, Milan", Colors = new List<string>{ "Red", "Blue" } },
        new Stadium{ Id = 9, Name = "Estadio Azteca",  Country ="MEX", Team="Mexico's National Team" },
        new Stadium{ Id = 10, Name = "Anfield",  Country ="ENG", Team="Liverpool", Colors = new List<string>{ "Red" } },
        new Stadium{ Id = 11, Name = "Estadio Centenario", Country ="URU", Team="Peñarol, Uruguay's National Team" },
        new Stadium{ Id = 12, Name = "La Bombonera", Country ="ARG", Team="Boca Juniors", Colors = new List<string>{ "Blue", "Yellow" } },
        new Stadium{ Id = 13, Name = "Emirates", Country ="ENG", Team="Arsenal", Colors = new List<string>{ "Red", "White" } },
    };

    readonly List<Country> countries = new() {
        new Country{ Code = "ARG", Name  ="Argentina" },
        new Country{ Code = "BRA", Name = "Brazil" },
        new Country{ Code = "ENG", Name = "England" },
        new Country{ Code = "GER", Name = "Germany" },
        new Country{ Code = "ITA", Name = "Italy" },
        new Country{ Code = "SPA", Name = "SPAIN" }
    };

    [Fact]
    public void FilterString()
    {
        var filteredStadium = stadiumNames.Where(x => x.StartsWith("A")).ToList();
        Assert.True(filteredStadium.Count == 2); // Allianz Arena, Anfield
        Assert.True(filteredStadium[0] == "Allianz Arena");
        Assert.True(filteredStadium[1] == "Anfield");
    }

    [Fact]
    public void FilterObjects()
    {
        var filteredStadium = stadiums.Where(x => x.Country.Equals("BRA")).ToList();
        Assert.True(filteredStadium.Count == 1); // Marcana
        Assert.True(filteredStadium.First().Name == "Maracana");
    }

    [Fact]
    public void JoiningBySingleAttribute()
    {
        var joined = stadiums.Join(countries,
            stadium => stadium.Country,
            country => country.Code,
            (stadium, country) => new { StadiumName = stadium.Name, FullCountryName = country.Name });

        Assert.True(joined.First().StadiumName.Equals("Wembley"));
        Assert.True(joined.First().FullCountryName.Equals("England"));
    }

    [Fact]
    public void ProjectAndRename()
    {
        var ids = stadiums.Select(x => new { NewFieldContainingId = x.Id }).ToList();

        Assert.True(ids[0].NewFieldContainingId == 1);
    }

    [Fact]
    public void SelectMany()
    {
        var projected = stadiums.Select(x => x.Colors).ToList();
        Assert.Equal(13, projected.Count);
        Assert.Equal(new List<string>() { }, projected[0]); // No conors for wembley
        Assert.Equal(new List<string>() { "Blue", "Garnet" }, projected[1]); // Nou camp colors

        var flat = stadiums.SelectMany(x => x.Colors).ToList();

        Assert.Equal(12, flat.Count);
        Assert.Equal("Blue", flat.First());
        Assert.Equal(new List<string>() { "Blue", "Garnet", "White", "Yellow", "Black", "Red", "Blue", "Red", "Blue", "Yellow", "Red", "White" }, flat);
    }

    [Fact]
    public void Sort()
    {
        var sorted = stadiums.OrderBy(x => x.Team).ToList();
        Assert.Equal("Arsenal", sorted.First().Team);

        var sortedDesc = stadiums.OrderByDescending(x => x.Name).ToList();
        Assert.Equal("Wembley", sortedDesc.First().Name);
    }

    [Fact]
    public void GroupByAndCount()
    {
        var aggregated = stadiums.GroupBy(x => x.Country).Select(g => new { country = g.Key, sum = g.Count() }).ToList();

        foreach (var item in aggregated)
        {
            output.WriteLine(item.ToString());
        }


        Assert.Equal(8, aggregated.Count);
        Assert.Equal(new { country = "ENG", sum = 4 }, aggregated.First());
        Assert.Equal(new { country = "SPA", sum = 2 }, aggregated[1]);
        Assert.Equal(new { country = "BRA", sum = 1 }, aggregated[2]);
        Assert.Equal(new { country = "GER", sum = 2 }, aggregated[3]);
        Assert.Equal(new { country = "ITA", sum = 1 }, aggregated[4]);
        Assert.Equal(new { country = "MEX", sum = 1 }, aggregated[5]);
        Assert.Equal(new { country = "URU", sum = 1 }, aggregated[6]);
        Assert.Equal(new { country = "ARG", sum = 1 }, aggregated[7]);
    }
}