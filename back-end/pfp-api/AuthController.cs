using Microsoft.AspNetCore.Mvc;
using NpgsqlTypes;
using pfp_api.Core;
using System.Collections;
using System.Data;

namespace pfp_api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration Configuration;
        private database db;

        public AuthController(IConfiguration configuration)
        {
            Configuration = configuration;
            db = new database(Configuration);
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            ArrayList p = new ArrayList
            {
                new database.Param("test", NpgsqlDbType.Integer, 1)
            };

            DataSet ds = db.GetDataSet("SELECT 1 = @test", p);

            return new string[] { "value1", "value2" };
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}