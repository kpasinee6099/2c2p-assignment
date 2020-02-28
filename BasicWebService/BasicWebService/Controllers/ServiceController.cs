using BasicWebService.Models;
using BasicWebService.Service;
using BasicWebService.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace BasicWebService.Controllers
{ 
    public class ServiceController : ApiController
    {
        [HttpGet]
        [Route("api/GetEnrollCard", Name = "GetEnrollCard")]

        public string GetEnrollCard([FromUri]CardInfos tmpCard)
        {
            ValidateCard card = new ValidateCard();
            string isEnroll = card.isEnrolled(tmpCard);
            return isEnroll;
        }
    }
}
