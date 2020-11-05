namespace data_generator.Models
{
    public class TrackData
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Duration { get; set; }
        public int Rank { get; set; }
        public Artist Artist { get; set; }

        public TrackData()
        {
            Artist = new Artist();
        }
    }
}