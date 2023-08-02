namespace Tests
{
    public class LinqBasicOperationsTest
    {
        public record Stadium {
            public int Id { get; init; }
            public string Name { get; init; } = string.Empty;
            public string Country { get; init; } = string.Empty;
            public string Team { get; init; } = string.Empty;
        }

        public record Country {
            public string Name { get; init; } = string.Empty;
            public string Code { get; init; } = string.Empty;
        }

        readonly List<string> stadiumNames = new() { "Wembley", "Camp Nou", "Santiago Bernabeu", "Old Trafford", "Maracana", "Allianz Arena", "Signal Iduna Park", "Stadio Giuseppe Meazza","Estadio Azteca", "Anfield", "Estadio Centenario", "La Bombonera", "Emirates" };

        readonly List<Stadium> stadiums = new()
        {
            new Stadium{ Id = 1, Name = "Wembley", Country= "ENG", Team="England National Team" },
            new Stadium{ Id = 2, Name = "Camp Nou", Country ="SPA", Team="Barcelona" },
            new Stadium{ Id = 3, Name = "Santiago Bernabeu", Country ="SPA", Team="Real Madrid" },
            new Stadium{ Id = 4, Name = "Old Trafford",  Country ="ENG", Team="Man U" },
            new Stadium{ Id = 5, Name = "Maracana",  Country ="BRA", Team="Brazil's National Team" },
            new Stadium{ Id = 6, Name = "Allianz Arena",  Country ="GER", Team="Bayern Munich" },
            new Stadium{ Id = 7, Name = "Signal Iduna Park", Country ="GER", Team="Borusia Dortmund" },
            new Stadium{ Id = 8, Name = "Stadio Giuseppe Meazza", Country ="ITA", Team="Inter, Milan" },
            new Stadium{ Id = 9, Name = "Estadio Azteca",  Country ="MEX", Team="Mexico's National Team" },
            new Stadium{ Id = 10, Name = "Anfield",  Country ="ENG", Team="Liverpool" },
            new Stadium{ Id = 11, Name = "Estadio Centenario", Country ="URU", Team="Peñarol, Uruguay's National Team" },
            new Stadium{ Id = 12, Name = "La Bombonera", Country ="ARG", Team="Boca Juniors" },
            new Stadium{ Id = 13, Name = "Emirates", Country ="ENG", Team="Arsenal" },
        };

        readonly List<Country> countries = new () {
            new Country{ Code = "ARG", Name  ="Argentina" },
            new Country{ Code = "BRA", Name = "Brazil" },
            new Country{ Code = "ENG", Name = "England" },
            new Country{ Code = "GER", Name = "Germany" },
            new Country{ Code = "ITA", Name = "Italy" },
            new Country{ Code = "SPA", Name = "SPAIN" }
        };

        [Fact]
        public void ShouldFilter()
        {
            var filteredStadium = stadiumNames.Where(x => x.StartsWith("A")).ToList();
            Assert.True(filteredStadium.Count == 2); // Allianz Arena, Anfield
            Assert.True(filteredStadium[0] == "Allianz Arena");
            Assert.True(filteredStadium[1] == "Anfield");
        }

        [Fact]
        public void ShouldFilterObjects()
        {
            var filteredStadium = stadiums.Where(x => x.Country.Equals("BRA")).ToList();
            Assert.True(filteredStadium.Count == 1); // Marcana
            Assert.True(filteredStadium.First().Name == "Maracana");
        }

        [Fact]
        public void ShouldJoinByCountry() {
            var joined = stadiums.Join(countries, 
                stadium => stadium.Country, 
                country => country.Code, 
                (stadium, country) => new { StadiumName = stadium.Name, FullCountryName = country.Name });

            Assert.True(joined.First().StadiumName.Equals("Wembley"));
            Assert.True(joined.First().FullCountryName.Equals("England"));
        }

        [Fact]
        public void ShouldProject() {
            Assert.True(false);
        }

        /*
         * Join Operators
Projection Operations
Sorting Operators
Grouping Operators
Conversions
Concatenation
Aggregation
Quantifier Operations
Partition Operations
Generation Operations
Set Operations
Equality
Element Operators
         * 
         */
    }
}