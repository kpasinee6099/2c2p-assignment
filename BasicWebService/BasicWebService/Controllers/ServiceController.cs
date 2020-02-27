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
    /// <summary>
    /// jjj
    /// </summary>
    public class ServiceController : ApiController
    {
        [HttpGet]
        [Route("api/GetCard", Name = "GetCard")]

        public string GetCard([FromUri]CardInfo tmpCard)
        {
            ValidateCard card = new ValidateCard();
            string isEnroll = card.isEnrolled(tmpCard);
            return isEnroll;
        }
    }
}
