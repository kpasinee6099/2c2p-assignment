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

        CardInfoStat result = new CardInfoStat();

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public bool IsCardNumber(string cardNum)
        {
            if (!string.IsNullOrEmpty(cardNum) && !string.IsNullOrWhiteSpace(cardNum))
            {
                if (Regex.IsMatch(cardNum, @"^\d+$") && cardNum.Length > 0 && (cardNum.Length == 15 || cardNum.Length == 16) )
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
            else
            {
                return false;
            }
        }

        public bool IsExpDate(string expDate)
        {
            if (!string.IsNullOrEmpty(expDate) && !string.IsNullOrWhiteSpace(expDate))
            {
                if (expDate.Length == 6)
                {
                    string month = expDate.Substring(0, 2);
                    string year = expDate.Substring(2, 4);
                    if (Regex.IsMatch(expDate, @"^\d+$"))
                    {
                        if ((year.StartsWith("2") && year[1] == '0')
                            && (Convert.ToInt16(year.Substring(2, 2)) >= 20)
                            && (Convert.ToInt16(month) > 0 && Convert.ToInt16(month) <= 12))
                        {
                            log.Info("Format of expire date is allowed");
                            return true;
                        }
                        else
                        {
                            log.Debug(string.Format("Expire date : {0} is not allowed, month {1} / year {2}", expDate, month, year));
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
            else
            {
                return false;
            }
        }

        public string CheckCardType(string cardNum)
        {
            char type = cardNum[0];
            int digit = cardNum.Length;
            log.Info("CheckCardType");
            switch (type)
            {
                case '3':
                    log.Info("In case of card number is start with 3");
                    if (digit == 16)
                    {
                        result.cardType = "JCB";
                    }
                    else
                    {
                        result.cardType = "Amex";
                    }
                    break;
                case '4':
                    log.Info("In case of card number is start with 4");
                    result.cardType = "Visa";
                    break;
                case '5':
                    log.Info("In case of card number is start with 5");
                    result.cardType = "MasterCard";                   
                    break;
                default:
                    log.Info("In case of card number is unknown");
                    result.cardType = "Unknown";
                    break;
            }
            return result.cardType;
        }
        public string CheckCardStat(string expdate)
        {
            string tmpYear = expdate.Substring(2);
            int year = int.Parse(tmpYear);
            string cardType = result.cardType;
            log.Info("CheckCardStat");
            if (cardType.Contains("JCB"))
            {
                log.Info("JCB's status always Valid");
                result.cardStat = "Valid";
            }
            else if(cardType.Contains("Visa"))
            {
                log.Info("Visa");
                if ( year % 4 == 0 )
                {
                    log.Info("Visa's status is Valid because expire date is leap year");
                    result.cardStat = "Valid";
                }
                else
                {
                    log.Info("Visa's status is Invalid because expire date is not leap year");
                    result.cardStat = "Invalid";
                }
                
            }
            else if (cardType.Contains("MasterCard"))
            {
                log.Info("MasterCard");
                for (int i = 2; i <= year; i++)
                {
                    if ( ( year % i == 0 ) && ( i != year ) )
                    {
                        log.Info("MasterCard's status is Invalid because expire date is not prime numeber");
                        result.cardStat = "Invalid";
                        break;
                    }
                    else
                    {
                        log.Info("MasterCard's status is Valid because expire date is not prime numeber");
                        result.cardStat = "Valid";
                    }
                }
            }
            else
            {
                log.Info("Unknown card is Invalid");
                result.cardStat = "Invalid";
            }
            return result.cardStat;
        }

    }      
}