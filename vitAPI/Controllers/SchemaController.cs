using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Repository.Abstract;
using Repository.Models;

namespace vitAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SchemaController : ControllerBase
    {
        private readonly ISchemaRepository _repo;
        public SchemaController(ISchemaRepository repo)
        {
            _repo = repo;
        }

        [Route("{table?}"), HttpGet]
        public IEnumerable<SchemaModel> GetTablesSchema(string table = null)
        {
            return _repo.GetTablesSchema(table);
        }

        [HttpPost]
        public IEnumerable<SchemaModel> Post([FromBody] SchemaModel schema)
        {
            return _repo.Create(schema);
        }

        [HttpPut]
        public IEnumerable<SchemaModel> Put([FromBody] SchemaModel schema)
        {
            return _repo.Update(schema);
        }

        [HttpDelete]
        public IEnumerable<SchemaModel> Delete([FromBody] SchemaModel schema)
        {
            return _repo.Delete(schema);
        }
    }
}