using Npgsql;
using NpgsqlTypes;
using System.Collections;
using System.Data;

namespace pfp_api.Core
{
    public class database
    {
        public static DataSet GetDataSet(string sql, ArrayList? @params = null)
        {
            DataSet ds = new DataSet();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(AppSettings.Current.ConnectString)) {
                   
                    using (var cmd = new NpgsqlCommand(sql, connection))
                    {
                        if (@params != null)
                        {
                            foreach (Param itm in @params)
                            {
                                cmd.Parameters.AddWithValue(itm.Name, itm.Type, itm.Value);
                            }
                        }

                        NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(cmd);
                        ds.Reset();
                        dataAdapter.Fill(ds);
                        connection.Close();

                        return ds;

                    }
                } 
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine(ex.Message);
                return ds;
            }
        }

        public class Param
        {
            public string Name { get; set; }
            public NpgsqlDbType Type { get; set; }
            public object Value { get; set; }

            public Param(string name, NpgsqlDbType type, object value)
            {
                Name = name;
                Type = type;
                Value = value;
            }
        }
    }
}