using BasicWebService.Models;
using BasicWebService.Service;
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
        [Route("api/getCard", Name = "getCard")]
        public bool getCard([FromUri]CardInfo tmpCard)
        {
            ValidateCard card = new ValidateCard();
            bool isCom = card.isComplete(tmpCard);           
            return isCom;
        }


    }
}
