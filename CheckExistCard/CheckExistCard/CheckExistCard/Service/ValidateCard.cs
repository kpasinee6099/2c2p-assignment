using CheckExistCard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace CheckExistCard.Service
{
    public class ValidateCard
    {
        public bool isCardNum(string cardNum)
        {
            if (Regex.IsMatch(cardNum, @"^\d+$") && (cardNum.Length > 0 && (cardNum.Length == 15 || cardNum.Length == 16)))
            {
                return true;
            }
            else
            {
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
                    if ((year.StartsWith("2") && year[1] == '0') && (Convert.ToInt16(month) > 0 && Convert.ToInt16(month) <= 12))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
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
                    result.cardType = "Visa";
                    if (year % 4 == 0)
                    {
                        result.cardStat = "Valid";
                    }
                    break;

                case '5':
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
                    result.cardType = "Unknown";
                    break;

            }
            return result;
        }
    }      
}