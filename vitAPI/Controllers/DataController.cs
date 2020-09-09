using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Repository.Abstract;
using System.Collections.Generic;

namespace vitAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DataController : ControllerBase
    {

        private readonly IDataRepository _repo;
        public DataController(IDataRepository repo)
        {
            _repo = repo;
        }

        [Route("{table}/{id?}"), HttpGet]
        public JsonResult Get(string table, int? id)
        {
            if (id.HasValue)
            {
                return new JsonResult(_repo.GetItem(table, id.Value));
            } 
            return new JsonResult(_repo.GetAll(table));
        }

        [Route("{table}"), HttpPost]
        public dynamic Post([FromRoute]string table, [FromBody]dynamic json)
        {
            var model = JsonConvert.DeserializeObject<Dictionary<string, object>>(json.ToString());
            return _repo.Create(table, model);
        }

        [Route("{table}"), HttpPut]
        public dynamic Put([FromRoute]string table, [FromBody]dynamic json)
        {
            var model = JsonConvert.DeserializeObject<Dictionary<string, object>>(json.ToString());
            return _repo.Update(table, model);
        }

        [Route("{table}/{id}"), HttpDelete]
        public dynamic Delete(string table, int id)
        {
            return _repo.Delete(table, id);
        }
    }
}