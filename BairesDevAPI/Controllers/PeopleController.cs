using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BairesDevAPI.Controllers
{
    public class PeopleController : Controller
    {
        [HttpGet]
        public IEnumerable<WeatherForecast> TopClients()
        {
            var json = File.ReadAllText("");
        }
    }
}
