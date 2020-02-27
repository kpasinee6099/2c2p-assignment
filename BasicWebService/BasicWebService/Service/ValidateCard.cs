using BasicWebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace BasicWebService.Service
{
    public class ValidateCard
    {
        public bool isComplete(CardInfo card)
        {
            string resultmessage = string.Empty;
            bool numFlag = false;
            bool expFlag = false;

            if ( card.expDate.Length == 6 )
            {
                string month = card.expDate.Substring(0, 2);
                string year = card.expDate.Substring(2, 4);
                if ( Regex.IsMatch(card.expDate, @"^\d+$") )
                {
                    if ( ( year.StartsWith("2") && year[1] == '0' ) && ( Convert.ToInt16(month) > 0 && Convert.ToInt16(month) <= 12 ) )
                    {
                        expFlag = true;
                    }
                    else
                    {
                        expFlag = false;
                    }
                }
                else
                {
                    expFlag = false;
                }
            }
            else
            {
                expFlag = false;
            }

            if ( Regex.IsMatch(card.cardNum, @"^\d+$") && ( card.cardNum.Length > 0 && ( card.cardNum.Length == 15 || card.cardNum.Length == 16 ) ) )
            {
                numFlag = true;
            }
            else
            {
                numFlag = false;
            }

            if ( numFlag && expFlag )
            {
                resultmessage = identifyCard(card);
                return true;
            }
            else
            {
                return false;
            }                   
        }

        public string identifyCard( CardInfo card )
        {
            CardResult result = new CardResult();
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
            return result.cardStat + " " + result.cardType;
        }
    }
        
}