using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TenniesMgt.BL;

namespace TenniesMgt.Controllers
{
   
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("api/Values/GetUser")]
        public HttpResponseMessage GetUser()
        {
            HttpResponseMessage response = null;
            try
            {
                var userInfo = TenniesManagement.GetUser();

                response = Request.CreateResponse(HttpStatusCode.OK, new { Status = true, Message = userInfo });                

                return response;
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { Status = false, Message = ex.Message.ToString() });
                //return Request.CreateResponse(HttpStatusCode.BadRequest, new { Status = false, Message = ex.Message.ToString() });
            }

        }

        [HttpGet]
        [AllowAnonymous]
        [Route("api/Values/GetMyMatchInfo")]
        public HttpResponseMessage GetMyMatchInfo(long userId)
        {
            HttpResponseMessage response = null;
            try
            {
                var userInfo = TenniesManagement.GetMatch(userId);


                response = Request.CreateResponse(HttpStatusCode.OK, new { Status = true, Message = userInfo });

                return response;
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { Status=false, Message= ex.Message.ToString() } );
            }
            
        }
    }
}
