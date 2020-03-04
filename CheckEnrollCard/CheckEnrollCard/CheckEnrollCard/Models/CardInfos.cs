using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
namespace CheckEnrollCard.Models
{
    public class Result
    {
        public CardInfos info {get; set;}

        public CardInfoStat stat { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
    public class CardInfos
    {
        public string cardNum { get; set; }
        public string expDate { get; set; }

        public CardInfos()
        {
            this.cardNum = string.Empty;
            this.expDate = string.Empty;
        }

    }
    public class CardInfoStat
    {
        public string cardType { get; set; }
        public string cardStat { get; set; }

        public CardInfoStat()
        {
            this.cardStat = "Invalid";
            this.cardType = "Unknown";
            
        }

    }

}