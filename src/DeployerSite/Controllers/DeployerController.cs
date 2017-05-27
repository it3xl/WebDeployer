using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
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
            return new string[] { "Service name", "Deployer" };
        }

        // POST api/deployer
        [HttpPost]
        public async Task<JsonResult> Post(string fileName)
        {
            // https://stackoverflow.com/a/40970671


            try
            {

                Request.HttpContext.Co
                var filedata = await Request.ReadFormAsync();
                    
                //    .ReadAsMultipartAsync(new MultipartMemoryStreamProvider());
                foreach (var file in filedata.)
                {
                    var fileStream = await file.ReadAsStreamAsync();
                }

            }
            catch (Exception ex)
            {

            }

            
            return Json("['oppa']");
        }
    }
}
