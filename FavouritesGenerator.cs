using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_generator
{
    class FavouritesGenerator
    {
        public async Task PopulateFavourites(NpgsqlConnection con)
        {
            // skrypty działają jeżeli z tabel favourite_songs/albums/artists wywalimy foreign keye dla track_id/album_id itd,
            // no idea dlaczego tak.
            
            //extracting album id for later use in the main loop
            var AlbumDT = new DataTable();
            var AlbumIdCheck =
                $@"SELECT id FROM ""Albums"";";

            List<DataRow> AlbumId = new List<DataRow>(); 

            await using (var cmd = new NpgsqlCommand(AlbumIdCheck, con))
            {
                using (var reader = cmd.ExecuteReader())
                    // while (await reader.ReadAsync())
                {
                    AlbumDT.Load(reader);
            
                    AlbumId= AlbumDT.AsEnumerable().ToList();
                    // foreach (var j in reader)
                    // {
                    //     AlbumId.Add(j.ToString());
                    // }
                }
            }
            
            
            
            
            //extracting artist id for later use in the main loop
            var ArtistDT = new DataTable();
            var ArtistIdSelect =
                $@"SELECT id FROM ""Artists"";";
            
            List<DataRow> ArtistId = new List<DataRow>(); 

            await using (var cmd = new NpgsqlCommand(ArtistIdSelect, con))
            {
                using (var reader = cmd.ExecuteReader())
                    // while (await reader.ReadAsync())
                {
                    ArtistDT.Load(reader);
            
                    ArtistId= AlbumDT.AsEnumerable().ToList();
                    // foreach (var j in reader)
                    // {
                    //     ArtistId.Add(j.ToString());
                    // }
                }
            }
            
            //extracting song ids for later use
            var TrackDT = new DataTable();
            var TrackIdSelect =
                $@"SELECT id FROM ""Tracks"";";

            List<DataRow> TrackId = new List<DataRow>(); 

            await using (var cmd = new NpgsqlCommand(TrackIdSelect, con))
            {
                using (var reader = cmd.ExecuteReader())
                    // while (await reader.ReadAsync())
                {
                    TrackDT.Load(reader);
            
                    TrackId= AlbumDT.AsEnumerable().ToList();
                    // foreach (var j in reader)
                    // {
                    //     TrackId.Add(j.ToString());
                    // }
                }
            }    

            Random random = new Random();
            
            //Main Loop
            
            for (int i = 1; i < 200001; i++)
            {
                // fav ablums - id/album_id/user_id/
                // album id - u mnie od 116600 do 130515, ale jest sporo pustych id
                
                
                var insertFavouriteAlbums = $"INSERT INTO \"favorite_albums\" VALUES({i},{(AlbumId[random.Next(AlbumId.Count)])[0]},{random.Next(1, 250001)})";
                
                Console.WriteLine(insertFavouriteAlbums);
                await using (var cmd = new NpgsqlCommand(insertFavouriteAlbums, con))
                {

                    await cmd.ExecuteNonQueryAsync();

                }
                
                // fav artists
                var insertFavouriteArtist =
                    $"INSERT INTO \"favorite_artists\"VALUES({i},{(ArtistId[random.Next(ArtistId.Count)])[0]},{random.Next(1, 250001)})";
                Console.WriteLine(insertFavouriteArtist);
                await using (var cmd2 = new NpgsqlCommand(insertFavouriteArtist, con))
                {

                    await cmd2.ExecuteNonQueryAsync();

                }
                
                //fav songs
                var insertFavouriteSongs = 
                    $"INSERT INTO \"favorite_songs\"VALUES({i},{(ArtistId[random.Next(TrackId.Count)])[0]},{random.Next(1, 250001)})";
                Console.WriteLine(insertFavouriteSongs);
                await using (var cmd3 = new NpgsqlCommand(insertFavouriteSongs, con))
                {

                    await cmd3.ExecuteNonQueryAsync();

                }

            }
        }
    }
}
