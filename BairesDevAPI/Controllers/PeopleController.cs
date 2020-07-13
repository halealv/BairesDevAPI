using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BairesDevAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<People> Get()
        {
            var json = GetFileContent();

            try
            {
                var personArray = JArray.Parse(json);

                if (personArray != null)
                {
                    var peoples = personArray.ToObject<People[]>();
                    return peoples;
                }

                return null;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        [HttpGet("topclients/{top}")]
        public IActionResult TopClients(int top)
        {
            var json = GetFileContent();

            try
            {
                var personArray = JArray.Parse(json);

                if (personArray != null)
                {
                    var peoples = personArray.ToObject<List<People>>();

                    var topClients = (from p in peoples
                                      orderby p.NumberOfConnections + p.NumberOfRecommendations descending
                                      select new { p.PersonId }).Take(top).ToList();

                    return new JsonResult(topClients);
                }

                return null;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpGet("clientposition/{personId}")]
        public IActionResult ClientPosition(int personId)
        {
            var json = GetFileContent();

            try
            {
                var personArray = JArray.Parse(json);

                if (personArray != null)
                {
                    var peoples = personArray.ToObject<List<People>>();

                    var query = peoples.OrderByDescending(p => p.NumberOfConnections + p.NumberOfRecommendations).Select((p, i) => new
                    {
                        Position = i,
                        p.PersonId
                    });

                    var clientPosition = query.Where(p => p.PersonId == personId).Select(p => p.Position).FirstOrDefault();

                    return new JsonResult(clientPosition);
                }

                return null;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private string GetFilePath()
        {
            string path;

            path = @"C:\repo\people.json";

            return path;
        }

        private string GetFileContent()
        {
            var jsonFile = this.GetFilePath();
            var json = System.IO.File.ReadAllText(jsonFile);

            return json;
        }
    }
}
