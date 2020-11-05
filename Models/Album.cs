using System;
using System.ComponentModel.DataAnnotations;

namespace data_generator.Models
{
    class Album
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Nb_tracks { get; set; }
        public DateTime Release_Date { get; set; }
        public Artist Artist { get; set; }
        public Tracks Tracks { get; set; }

        public Genres Genres { get; set; }


        public Album()
        {
            Artist = new Artist();
            Tracks = new Tracks();
            Genres = new Genres();
        }
    }
}