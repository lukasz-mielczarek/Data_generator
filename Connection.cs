using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace data_generator
{
    public class Connection
    {
        public NpgsqlConnection ConnectionDB;
        public NpgsqlConnection GetConnection()
        {
            var cs = "Host=localhost;Username=postgres;Password=mam5lat;Database=SongDB";

            
            
        
            if (ConnectionDB == null)
            {
                ConnectionDB = new NpgsqlConnection(cs);
                
            }
            return ConnectionDB;



        }
    }
}
