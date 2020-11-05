using System.Collections.Generic;

namespace data_generator.Models
{
    class Tracks
    {
        public IList<TrackData> Data { get; set; }

        public Tracks()
        {
            Data = new List<TrackData>();
        }
    }
}