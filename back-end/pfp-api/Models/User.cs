using pfp_api.Core;
using System.Collections;
using System.Data;

namespace pfp_api.Models
{
    public class User
    {
        public User(
            int id,
            string firstName,
            string lastName,
            string email,
            string password,
            DateTime? createDate = null,
            DateTime? updateDate = null,
            Guid? apiKey = null,
            DateTime? apiKeyExpiration = null)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;

            if (createDate != null)
                CreateDate = createDate;

            if (updateDate != null)
                UpdateDate = updateDate;

            if (apiKey != null && apiKey != Guid.Empty)
                APIKey = apiKey;

            if (apiKeyExpiration != null)
                APIKeyExpiration = apiKeyExpiration;
        }

        public int Id { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Guid? APIKey { get; set; }
        public DateTime? APIKeyExpiration { get; set; }

        public void Save()
        {
            ArrayList c = new ArrayList
            {
                new database.Column("first_name", NpgsqlTypes.NpgsqlDbType.Varchar, this.FirstName),
                new database.Column("last_name", NpgsqlTypes.NpgsqlDbType.Varchar, this.LastName),
                new database.Column("email", NpgsqlTypes.NpgsqlDbType.Varchar, this.Email),
            };

            if (this.Id == -1)
            {
                // New user, encrypt and store their password, give them an API key to access the site
                this.Password = BCrypt.Net.BCrypt.HashPassword(this.Password);
                this.APIKey = Guid.NewGuid();
                this.APIKeyExpiration = DateTime.UtcNow.AddHours(1);

                c.Add(new database.Column("password", NpgsqlTypes.NpgsqlDbType.Varchar, this.Password));
                c.Add(new database.Column("api_key", NpgsqlTypes.NpgsqlDbType.Uuid, this.APIKey));
                c.Add(new database.Column("api_key_expiration", NpgsqlTypes.NpgsqlDbType.TimestampTz, this.APIKeyExpiration));
            }
            else
            {
                c.Add(new database.Column("update_date", NpgsqlTypes.NpgsqlDbType.Timestamp, DateTime.UtcNow));
            }

            this.Id = database.Save(this.Id, "tbl_user", c);
        }

        public static User? Get(int id)
        {
            ArrayList p = new ArrayList
            {
                new database.Param("id", NpgsqlTypes.NpgsqlDbType.Integer, id),
            };

            using (DataSet ds = database.GetDataSet("SELECT * FROM tbl_user WHERE id = @id LIMIT 1;", p))
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow r = ds.Tables[0].Rows[0];

                    return new User(
                        Convert.ToInt32(r["id"]),
                        r["first_name"] + "",
                        r["last_name"] + "",
                        r["email"] + "",
                        r["password"] + "",
                        Convert.ToDateTime(r["create_date"]),
                        Convert.ToDateTime(r["update_date"]),
                        r["api_key"] + "" == "" ? Guid.Empty : Guid.Parse(r["api_key"] + ""),
                        r["api_key_expiration"] + "" == "" ? null : Convert.ToDateTime(r["api_key_expiration"]));
                }

                return null;
            }
        }
    }
}
