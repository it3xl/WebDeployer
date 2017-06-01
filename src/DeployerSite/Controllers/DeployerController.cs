using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeployerSite.Controllers
{
    [Route("api/[controller]")]
    public class DeployerController : Controller
    {
        // GET: api/deployer
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new [] { "Service name", "Deployer" };
        }

        // POST api/deployer
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Post()
        {
            // https://stackoverflow.com/a/42277472/390940
            // https://stackoverflow.com/a/39416373/390940



            var stream = Request.Body;

            using (var fileStream = new FileStream(@"C:\00\oppa_2.7z", FileMode.Create, FileAccess.Write))
            {
                stream.CopyTo(fileStream);
            }

            return Ok();


            return Json("['oppa']");
        }
    }
}
