using Npgsql;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using data_generator.Models;
using System.Threading;

namespace data_generator
{
    class MusicDataGenerator
    {
        public async Task PopulateDataMusic(NpgsqlConnection conn)
        {



            var httpClient = HttpClientFactory.Create();

            // Looping through Albums ID in API
            for (var i = 129830; i < 300000; i++)
            {
                // API connection
                var url = $"https://api.deezer.com/album/{i}";
                var httpResponseMessage = await httpClient.GetAsync(url);

                if (httpResponseMessage.StatusCode != System.Net.HttpStatusCode.OK) continue;

                var content = httpResponseMessage.Content;
                var album = await content.ReadAsAsync<Album>();


                if (album.Id != 0)
                {
                    // Insert Artist
                    var insertArtist = $@"INSERT INTO ""Artists""
                                       VALUES ({album.Artist.Id}, $${album.Artist.Name}$$);";

                    var artistExistCheck = $@"SELECT EXISTS(SELECT 1 FROM ""Artists"" WHERE id = {album.Artist.Id});";

                    bool artistInDb = false;

                    // Check for key duplicates
                    await using (var cmd = new NpgsqlCommand(artistExistCheck, conn))
                    {
                        var tokenSource = new CancellationTokenSource();

                        using (var reader = cmd.ExecuteReader())
                            while (await reader.ReadAsync())
                            {
                                
                                foreach(var j in reader )
                                artistInDb = reader.GetFieldValue<Boolean>(0);
                            }
                    }

                    if (!artistInDb)
                    {
                        await using (var cmd = new NpgsqlCommand(insertArtist, conn))
                        {
                            await cmd.ExecuteNonQueryAsync();
                        }
                    }


                    // Insert Album
                    var insertAlbum = $@"INSERT INTO ""Albums"" 
                                  VALUES ({album.Id}, {album.Artist.Id}, $${album.Title}$$, {album.Nb_tracks});";


                    await using (var cmd = new NpgsqlCommand(insertAlbum, conn))
                    {
                        await cmd.ExecuteNonQueryAsync();
                    }

                    // Insert genre
                    try
                    {
                        var insertGenre = $@"INSERT INTO ""Genres""
                                  VALUES ({album.Genres.Data[0].Id}, $${album.Genres.Data[0].Name}$$);";

                        var genreExistCheck =
                            $@"SELECT EXISTS(SELECT 1 FROM ""Genres"" WHERE id = {album.Genres.Data[0].Id});";

                        bool genreInDb = false;

                        // Check for key duplicates
                        await using (var cmd = new NpgsqlCommand(genreExistCheck, conn))
                        {
                            var tokenSource = new CancellationTokenSource();

                            using (var reader = cmd.ExecuteReader())
                                while (await reader.ReadAsync())
                                {
                                    genreInDb = reader.GetFieldValue<Boolean>(0);
                                }
                        }

                        // Adding new Genre to db
                        if (!genreInDb)
                        {
                            await using (var cmd = new NpgsqlCommand(insertGenre, conn))
                            {
                                await cmd.ExecuteNonQueryAsync();
                            }
                        }
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        Console.WriteLine("index out of range");
                    }
                }

                //log which album
                Console.WriteLine($"{album.Id}: {album.Title}");

                // Tracks Insert
                try
                {
                    foreach (var track in album.Tracks.Data)
                    {
                        var insertTrack = $@"INSERT INTO ""Tracks""
                                      VALUES ({track.Id}, $${track.Title}$$, {track.Duration}, {track.Rank},
                                              '{album.Release_Date.ToDateString()}', {album.Id}, {album.Genres.Data[0].Id});";

                        await using (var cmd = new NpgsqlCommand(insertTrack, conn))
                        {
                            await cmd.ExecuteNonQueryAsync();
                        }
                    }
                }
                catch (ArgumentOutOfRangeException e)
                {
                    Console.WriteLine("index out of range");
                }
            }
        }
    }
}
