using CheckEnrollCard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace CheckEnrollCard.Service
{
    
    public class ValidateCard
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public bool isCardNum(string cardNum)
        {
            if (Regex.IsMatch(cardNum, @"^\d+$") && (cardNum.Length > 0 && (cardNum.Length == 15 || cardNum.Length == 16)))
            {
                log.Info("Format card of number is allowed");
                return true;
            }
            else
            {
                log.Debug(string.Format("Card number's  : {0}", cardNum));
                log.Debug(string.Format("Card number's length : {0}", cardNum.Length));
                return false;
            }
        }

        public bool isExpDate(string expDate)
        {
            if (expDate.Length == 6)
            {
                string month = expDate.Substring(0, 2);
                string year = expDate.Substring(2, 4);
                if (Regex.IsMatch(expDate, @"^\d+$"))
                {
                    if ( ( year.StartsWith("2") && year[1] == '0' ) 
                        && ( Convert.ToInt16( year.Substring( 2 , 2 ) ) >= 20 )  
                        && (Convert.ToInt16(month) > 0 && Convert.ToInt16(month) <= 12) )
                    {
                        log.Info("Format of expire date is allowed");
                        return true;
                    }
                    else
                    {
                        log.Debug(string.Format("Expire date : {0} is not allowed, month {1} / year {2}", expDate , month , year));
                        return false;
                    }
                }
                else
                {
                    log.Debug(string.Format("Expire date : {0} is not allowed, Digit pls", expDate));
                    return false;
                }
            }
            else
            {
                log.Debug(string.Format("Expire date : {0} is not allowed, 6-digit pls", expDate));
                return false;
            }
        }

        public CardInfoStat identifyCard( CardInfos card )
        {
            CardInfoStat result = new CardInfoStat();
            result.cardStat = "Invalid";
            result.cardType = "Unknown";

            string tmpYear = card.expDate.Substring(2);
            int year = int.Parse(tmpYear);
            
            char type = card.cardNum[0];
            switch (type)
            {
                case '3':
                    log.Info("In case of card number is start with 3");
                    int digit = card.cardNum.Length;
                    if (digit == 16)
                    {
                        result.cardType = "JCB";
                        result.cardStat = "Valid";
                        
                    }
                    else if (digit == 15)
                    {
                        result.cardType = "Amex";
                    }
                    else
                    {
                        result.cardType = "Unknown";
                    }

                    
                    break;

                case '4':
                    log.Info("In case of card number is start with 4");
                    result.cardType = "Visa";
                    if (year % 4 == 0)
                    {
                        result.cardStat = "Valid";
                    }
                    
                    break;

                case '5':
                    log.Info("In case of card number is start with 5");
                    result.cardType = "MasterCard";
                    for (int i = 2; i <= year; i++)
                    {
                        if (year % i == 0)
                        {
                            result.cardStat = "Invalid";
                        }
                        else
                        {
                            result.cardStat = "Valid";
                        }
                    }
                    
                    break;

                default:
                    log.Info("In case of card number is unknown");
                    result.cardType = "Unknown";
                    
                    break;
                    
            }
            return result;
        }
    }      
}