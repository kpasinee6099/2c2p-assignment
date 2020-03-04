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

        public bool CheckExistCard(string card)
        {
            CARDEntities nd = new CARDEntities();
            var existCardInfo = nd.isExistCard(card);
            if( existCardInfo.Count() > 0)
            {
                return true;
            }
            return false;
        }

        public string RegisterCard(Result resultCard)
        {
            CARDEntities nd = new CARDEntities();

            if(!CheckExistCard(resultCard.info.cardNum)){
                nd.RegisterCard(resultCard.info.cardNum, resultCard.stat.cardType, resultCard.stat.cardStat, resultCard.info.expDate);
                log.Info("Enrolling the card successful");
                return string.Format("{0} Does not exist : {1} {2}", resultCard.info.cardNum, resultCard.stat.cardStat, resultCard.stat.cardType);
            }
            else
            {
                log.Info("Can not enroll the card,It is existing");
                return string.Format("{0} Existing : {1} {2}", resultCard.info.cardNum, resultCard.stat.cardStat, resultCard.stat.cardType);
            }
           
        }
    }
}