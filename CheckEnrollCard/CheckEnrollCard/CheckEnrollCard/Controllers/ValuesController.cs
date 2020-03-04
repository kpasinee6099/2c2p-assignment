using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CheckEnrollCard.Models;
using CheckEnrollCard.Service;
using CheckEnrollCard.Repository;

namespace CheckEnrollCard.Controllers
{
    public class ValuesController : ApiController
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        ValidateCard vl = new ValidateCard();
        [HttpGet]
        [Route("api/IsEnrolled", Name = "IsEnrolled")]
        public string IsEnrolled(string card)
        {
            string resultMessage = string.Empty;
            try
            {
                log.Info("request check existing card");               
                bool isCardNum = vl.IsCardNumber(card);

                if (isCardNum)
                {
                    DBHandle db = new DBHandle();
                    if (db.CheckExistCard(card))
                    {
                        return string.Format("Card {0} Existing", card);
                    }
                    else{
                        return string.Format("Card {0} Does not exist", card);
                    }

                }
                else
                {
                    return "Invalid card number";
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
            log.Debug(string.Format("{0} : ending of check existing card process", card));
            return string.Format("{0} : ending of check existing card", card);
        }

        [HttpPost]
        [Route("api/EnrollCard", Name = "EnrollCard")]
        public string EnrollCard([FromUri]CardInfos card)
        {

            string resultMessage = string.Empty;
            try
            {
                log.Info("Enroll the card");

                bool isNum = vl.IsCardNumber(card.cardNum);
                bool isExp = vl.IsExpDate(card.expDate);

                if (isNum && isExp)
                {

                    CardInfoStat IdtCard = new CardInfoStat();
                    IdtCard.cardType = vl.CheckCardType(card.cardNum);
                    IdtCard.cardStat = vl.CheckCardStat(card.expDate);

                    Result resultCard = new Result()
                    {
                        info = card,
                        stat = IdtCard
                    };
                    DBHandle db = new DBHandle();
                    return resultMessage = db.RegisterCard(resultCard);

                }
                else if (!isNum)
                {
                    log.Info("Can not enrolling the card");
                    return "Invalid card number";
                }
                else if (!isExp)
                {
                    log.Info("Can not enrolling the card");
                    return "Invalid expire date";
                }
                else
                {
                    log.Info("Can not enrolling the card");
                    return string.Format("Card {0} isn't enrolled", card.cardNum);
                }

            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
            log.Debug(string.Format("{0} : ending of enroll process", card.cardNum));
            return string.Format("{0} : ending of enroll process", card.cardNum);
        }
    }
}
