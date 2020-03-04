using CheckEnrollCard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CheckEnrollCard.Repository
{
    public class DBHandle
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public string CheckExistCard(string card)
        {
            CARDEntities nd = new CARDEntities();
            var existCardInfo = nd.isExistCard(card);
            foreach (var rec in existCardInfo)
            {
                log.Debug(string.Format("Card {0} is existing", card));
                return string.Format("Card {0} Existing", card);
            }
            log.Debug(string.Format("Card {0} is not exist", card));
            return string.Format("Card {0} Does not exist", card);
        }

        public string RegisterCard(Result resultCard)
        {
            CARDEntities nd = new CARDEntities();

            if(CheckExistCard(resultCard.info.cardNum).Contains("is not exist")){
                nd.RegisterCard(resultCard.info.cardNum, resultCard.stat.cardType, resultCard.stat.cardStat, resultCard.info.expDate);
                log.Info("Enrolling the card successful");
                return string.Format("{0} : {1} {2} Does not exist", resultCard.info.cardNum, resultCard.stat.cardStat, resultCard.stat.cardType);
            }
            else
            {
                log.Info("Can not enroll the card,It is existing");
                return string.Format("{0} : {1} {2} Existing", resultCard.info.cardNum, resultCard.stat.cardStat, resultCard.stat.cardType);
            }
           
        }
    }
}