using System.Collections.Generic;

namespace data_generator.Models
{
    public class Genres
    {
        public IList<GenreData> Data { get; set; }

        public Genres()
        {
            Data = new List<GenreData>();
        }
    }
}