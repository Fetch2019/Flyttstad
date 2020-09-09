using Flyttstad.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;

namespace Flyttstad.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly DataService _dataService;

        public HomeController(DataService dataService)
        {
            _dataService = dataService;
        }

        [HttpGet]
        public Data Get() => new Data();

        [HttpPost]
        public Offer Save([FromBody] PostData data) => _dataService.GetOffer(data);

        [HttpGet]
        [Route("getOptions/{id}")]
        public List<Options> GetOptions(int id) => _dataService.GetOptions(id);
    }
}