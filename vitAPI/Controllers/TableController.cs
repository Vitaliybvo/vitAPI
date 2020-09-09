using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Repository.Abstract;

namespace vitAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TableController : ControllerBase
    {
        private readonly ITableRepository _repo;
        public TableController(ITableRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IEnumerable<string> Tables()
        {
            return _repo.GetMyTables();
        }

        [HttpPost("{table}")]
        public IEnumerable<string> Post(string table)
        {
            return _repo.CreateTable(table);
        }

        [HttpDelete("{table}")]
        public IEnumerable<string> Delete(string table)
        {
            return _repo.DeleteTable(table);
        }
    }
}
