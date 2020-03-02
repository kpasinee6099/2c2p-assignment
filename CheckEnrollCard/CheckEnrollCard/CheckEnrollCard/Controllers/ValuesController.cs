using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CheckEnrollCard.Models;
using CheckEnrollCard.Service;

namespace CheckEnrollCard.Controllers
{
    public class ValuesController : ApiController
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [HttpGet]
        [Route("api/isEnrolled", Name = "isEnrolled")]
        public string isEnrolled(string card)
        {
            log.Info("request check existing card");
            ValidateCard vl = new ValidateCard();
            bool isCardNum = vl.isCardNum(card);
            if (isCardNum)
            {
                CARDEntities nd = new CARDEntities();
                var existCardInfo = nd.isExistCard(card);
                foreach (var rec in existCardInfo)
                {
                    log.Debug(string.Format("Card {0} is existing", card));
                    return string.Format("Card {0} is existing", card);
                }
                log.Debug(string.Format("Card {0} is not exist", card));
                return string.Format("Card {0} is not exist", card);
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
                log.Info("Enroll the card");
                ValidateCard vl = new ValidateCard();
                bool isNum = vl.isCardNum(c.cardNum);
                bool isExp = vl.isExpDate(c.expDate);

                if ( isNum && isExp)
                {

                    CARDEntities nd = new CARDEntities();
                    CardInfoStat r = vl.identifyCard(c);
                    if (!string.IsNullOrEmpty(isEnrolled(c.cardNum)))
                    {
                        
                        nd.RegisterCard(c.cardNum, r.cardType, r.cardStat, c.expDate);
                        log.Info("Enrolling the card successful");
                        return string.Format("{0} : {1} {2}", c.cardNum, r.cardStat, r.cardType);
                    }
                    else
                    {
                        log.Info("Can not enrolling the card");
                        return string.Format("{0} : {1} {2} was enrolled", c.cardNum, r.cardStat, r.cardType);
                    }
  
                }
                else if ( isNum )
                {
                    log.Info("Can not enrolling the card");
                    return "Invalid card number";
                }
                else if ( isExp )
                {
                    log.Info("Can not enrolling the card");
                    return "Invalid expire date";
                }
                else
                {
                    log.Info("Can not enrolling the card");
                    return string.Format("Card {0} isn't enrolled", c.cardNum);
                }
            }
            catch (Exception ex)
            {
                log.Error(string.Format("Error: {0}", ex));
            }
            log.Debug(string.Format("{0} : ending of enroll process", c.cardNum));
            Console.Read();
            return string.Format("{0} : ending of enroll process", c.cardNum);
        }
        
    }
}
