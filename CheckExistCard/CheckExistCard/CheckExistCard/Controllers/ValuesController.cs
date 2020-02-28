using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CheckExistCard.Models;
using CheckExistCard.Service;

namespace CheckExistCard.Controllers
{
    public class ValuesController : ApiController
    {


        [HttpGet]
        [Route("api/isEnrolled", Name = "isEnrolled")]
        public string isEnrolled(string card)
        {
            
            ValidateCard vl = new ValidateCard();
            bool isCardNum =  vl.isCardNum(card);
            if ( isCardNum )
            {
                CardEntities nd = new CardEntities();
                var existCardInfo = nd.ExistCard(card);
                foreach (var rec in existCardInfo)
                {
                    return "Card is Exist";
                }
                return "Does Not Exist";
            }
            else
            {
                return "Invalid card number";
            }
        }


        [HttpPost]
        [Route("api/enrollCard", Name = "enrollCard")]
        public string enrollCard([FromUri]CardInfos c)
        {
            string ret = string.Empty;
            try
            {
                ValidateCard vl = new ValidateCard();
                if (vl.isCardNum(c.cardNum) && vl.isExpDate(c.expDate))
                {
                    CardInfoStat r = vl.identifyCard(c);
                    CardEntities nd = new CardEntities();

                    var existCardInfo = nd.ExistCard(c.cardNum);
                    foreach (var rec in existCardInfo)
                    {
                        return "Card is Exist";
                    }

                    nd.AddCard(c.cardNum, r.cardType, r.cardStat, c.expDate);
                    return c.cardNum + ":" + r.cardStat + r.cardType;
                }
                else
                {
                    return "Card isn't enrolled";
                }
            }
            catch (Exception ex)
            {
                ret = string.Format("Error: {0}", ex);
            }
            return ret;
        }

    }
}
