using Npgsql;
using NpgsqlTypes;
using System.Collections;
using System.Data;

namespace pfp_api.Core
{
    public class database
    {
        public static DataSet GetDataSet(string sql, ArrayList? parameters = null)
        {
            DataSet ds = new DataSet();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(AppSettings.Current.ConnectString))
                {
                    using (var cmd = new NpgsqlCommand(sql, connection))
                    {
                        if (parameters != null)
                        {
                            foreach (Param itm in parameters)
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

        public static int Save(int id, string tableName, ArrayList columns)
        {
            string sql = "";

            if (id == -1)
            {
                string fields = "";
                string parameters = "";

                foreach (Column c in columns)
                {
                    fields += $"{c.Name}, ";
                    parameters += $"@{c.Name}, ";
                }

                fields = fields.TrimEnd(' ').TrimEnd(',');
                parameters = parameters.TrimEnd(' ').TrimEnd(',');

                sql = $"INSERT INTO {tableName} (" + fields + ") VALUES (" + parameters + ") RETURNING id";
            }
            else
            {
                sql = $"UPDATE {tableName} SET ";

                foreach (Column c in columns)
                {
                    sql += $"{c.Name} = @{c.Name}, ";
                }

                sql = sql.TrimEnd(' ').TrimEnd(',');
                sql += " WHERE id = @id RETURNING id";
            }


            using (NpgsqlConnection connection = new NpgsqlConnection(AppSettings.Current.ConnectString))
            {
                connection.Open();

                using (var cmd = new NpgsqlCommand(sql, connection))
                {
                    foreach (Column c in columns)
                    {
                        cmd.Parameters.AddWithValue(c.Name, c.Type, c.Value);
                    }


                    id = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }

            return id;
        }

        public static int Update(string sql, bool returnId = false, ArrayList? parameters = null)
        {
            int id = 0;

            using (NpgsqlConnection connection = new NpgsqlConnection(AppSettings.Current.ConnectString))
            {
                connection.Open();

                using (var cmd = new NpgsqlCommand(sql, connection))
                {
                    if (parameters != null)
                    {
                        foreach (Param p in parameters)
                        {
                            cmd.Parameters.AddWithValue(p.Name, p.Type, p.Value);
                        }
                    }

                    if (returnId)
                    {
                        id = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                    else
                    {
                        cmd.ExecuteScalar();
                    }
                }
            }

            return id;
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

        public class Column
        {
            public string Name { get; set; }
            public object Value { get; set; }
            public NpgsqlDbType Type { get; set; }

            public Column(string name, NpgsqlDbType type, object value)
            {
                Name = name;
                Value = value;
                Type = type;
            }
        }
    }
}