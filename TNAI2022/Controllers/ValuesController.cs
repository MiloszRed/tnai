using System.Collections.Generic;
using System.Web.Http;

using TNAI2022.Models;

namespace TNAI2022.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values
        public IHttpActionResult Get(string name)
        {
            if (name == "abc")
                return BadRequest("Niepoprawne imie");

            return Ok($"Twoje imię to {name}");
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public IHttpActionResult Post([FromBody] ProductDto product)
        {
            if (product == null)
                return NotFound();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok($"Dostałem produkt o nazwie {product.Name} i cenie {product.Price}");
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
