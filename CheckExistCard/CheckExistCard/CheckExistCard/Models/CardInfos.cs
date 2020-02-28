using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CheckExistCard.Models
{
    public class CardInfos
    {
        public string cardNum { get; set; }
        public string expDate { get; set; }

    }
    public class CardInfoStat
    {
        public string cardType { get; set; }
        public string cardStat { get; set; }

    }

}