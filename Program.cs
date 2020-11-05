using Npgsql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace data_generator
{
    class Program
    {
        static async Task Main()
        {
            Connection connection = new Connection();
            await using var con = connection.GetConnection();
            await con.OpenAsync();

            MusicDataGenerator musicDataGenerator = new MusicDataGenerator();
            await musicDataGenerator.PopulateDataMusic(con);

            /*UserPlaylistGenerator userPlaylistGenerator = new UserPlaylistGenerator();
            await userPlaylistGenerator.PopulateUserPlaylists(con);*/

            /*PlaylistGenerator playlistGenerator = new PlaylistGenerator();
            await playlistGenerator.PopulatePlaylists(con);*/

            /*FollowersGenerator followersGenerator = new FollowersGenerator();
            await followersGenerator.PopulateFollowers(con);*/











        }
    }
}
