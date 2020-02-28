using BasicWebService.Models;
using BasicWebService.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace BasicWebService.Service
{
    public class ValidateCard
    {
        public string isEnrolled(CardInfos card)
        {
            string resultmessage = string.Empty;
            bool numFlag = false;
            bool expFlag = false;

            if (card.expDate.Length == 6)
            {
                string month = card.expDate.Substring(0, 2);
                string year = card.expDate.Substring(2, 4);
                if (Regex.IsMatch(card.expDate, @"^\d+$"))
                {
                    if ((year.StartsWith("2") && year[1] == '0') && (Convert.ToInt16(month) > 0 && Convert.ToInt16(month) <= 12))
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

            if (Regex.IsMatch(card.cardNum, @"^\d+$") && (card.cardNum.Length > 0 && (card.cardNum.Length == 15 || card.cardNum.Length == 16)))
            {
                numFlag = true;
            }
            else
            {
                numFlag = false;
            }

            if (numFlag && expFlag)
            {
                DBHandle db = new DBHandle();
                bool isExist = db.isExistCard(card);
                if (!isExist)
                {
                    return "Does not exist";
                }
                else
                {
                    resultmessage = identifyCard(card);
                    return resultmessage;
                }
            }
            else if(!expFlag)
            {
                return "Invalid Expire Date";
            }
            else if (!numFlag)
            {
                return "Invalid Card Numbers";
            }
            else
            {
                return "Invalid Card";
            }
        }
        public string identifyCard( CardInfos card )
        {
            CardInfo result = new CardInfo();
            result.CardNum = card.cardNum;
            result.ExpDate = card.expDate;
            result.CardStat = "Invalid";
            result.CardType = "Unknown";

            string tmpYear = card.expDate.Substring(2);
            int year = int.Parse(tmpYear);
            
            char type = card.cardNum[0];
            switch (type)
            {
                case '3':
                    int digit = card.cardNum.Length;
                    if (digit == 16)
                    {
                        result.CardType = "JCB";
                        result.CardStat = "Valid";
                    }
                    else if (digit == 15)
                    {
                        result.CardType = "Amex";
                    }
                    else
                    {
                        result.CardType = "Unknown";
                    }
                    break;

                case '4':
                    result.CardType = "Visa";
                    if (year % 4 == 0)
                    {
                        result.CardStat = "Valid";
                    }
                    break;

                case '5':
                    result.CardType = "MasterCard";
                    for (int i = 2; i <= year; i++)
                    {
                        if (year % i == 0)
                        {
                            result.CardStat = "Invalid";
                        }
                        else
                        {
                            result.CardStat = "Valid";
                        }
                    }
                    break;

                default:
                    result.CardType = "Unknown";
                    break;

            }
            return result.CardStat + " " + result.CardType;
        }
    }      
}