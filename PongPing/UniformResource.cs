using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PongPing.uniformResource;

namespace PongPing
{

    [ApiController]
    public class UniformResource: ControllerBase
    {

        private List<Identifier> Identifiers => new() {
            new("win-9ndprc00ff9.object.social",6000,this.DbContextFactory),
            new("win-9ndprc00ff9.memory.claims",6000,this.DbContextFactory)
        };
        [Route("pongping/uniformresource/identifier/single")]
        [HttpGet]
        public string Get() => Identifiers.Where(x => x.Use < DateTime.UtcNow && x.UsedConnections < x.LimitOfConnection).OrderBy(x => x.Use).First().Path;
        private readonly IDbContextFactory<ServerStorages.OSAndClaimsContext> DbContextFactory;
        public UniformResource(IDbContextFactory<ServerStorages.OSAndClaimsContext> DbContextFactory) {
            this.DbContextFactory = DbContextFactory;
        }
    }
}
/*
 using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Wesbite_BadClaims.Server
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

 */