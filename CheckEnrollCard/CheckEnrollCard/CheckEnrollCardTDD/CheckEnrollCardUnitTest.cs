using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CheckEnrollCard.Service;
using CheckEnrollCard.Models;

namespace CheckEnrollCardTDD
{
    [TestClass]
    public class CheckEnrollCardUnitTest : ValidateCard
    {
        private readonly string cardNum15Def = "012345678912345";
        private readonly string cardNum16Def = "0123456789123456";
        private readonly string expDateDef = "012020";
        private readonly string cardStatDef = "Invalid";
        private readonly string cardTypeDef = string.Empty;

        CardInfos testValue = new CardInfos();
        CardInfoStat expecValue = new CardInfoStat();

        /*IsCardNumber*/
        [TestMethod]
        public void Test00_IsCardNumber_Format_Of_CardNumber_IsNull_Then_Fail()
        {
            Assert.IsFalse(IsCardNumber(null));
        }
        [TestMethod]
        public void Test01_IsCardNumber_Format_Of_CardNumber_IsEmpty_Then_Fail()
        {
            Assert.IsFalse(IsCardNumber(string.Empty));
        }
        [TestMethod]
        public void Test02_IsCardNumber_Format_Of_CardNumber_IsContain_WhiteSpace_Then_Fail()
        {
            Assert.IsFalse(IsCardNumber(cardNum16Def.Replace("1"," ")));
        }
        [TestMethod]
        public void Test03_IsCardNumber_Format_Of_CardNumber_IsContain_Alphabet_Then_Fail()
        {
            Assert.IsFalse(IsCardNumber(cardNum16Def.Replace("1234","qwer")));
        }
        [TestMethod]
        public void Test04_IsCardNumber_Length_Of_CardNumber_IsShorter_Then_Fail()
        {
            Assert.IsFalse(IsCardNumber(cardNum16Def.Substring(0,10)));
        }
        [TestMethod]
        public void Test05_IsCardNumber_Length_Of_CardNumber_IsLonger_Then_Fail()
        {
            Assert.IsFalse(IsCardNumber(cardNum16Def+"12345"));
        }
        [TestMethod]
        public void Test06_IsCardNumber_Format_Of_CardNumber_IsNumber_Then_Success()
        {
            Assert.IsTrue(IsCardNumber(cardNum16Def));
        }
        [TestMethod]
        public void Test07_IsCardNumber_Length_Of_CardNumber_isInRange15_Then_Success()
        {
            Assert.IsTrue(IsCardNumber(cardNum15Def));
        }
        [TestMethod]
        public void Test08_IsCardNumber_Length_Of_CardNumber_isInRange16_Then_Success()
        {
            Assert.IsTrue(IsCardNumber(cardNum16Def));
        }
        /*IsExpDate*/
        [TestMethod]
        public void Test09_IsExpDate_Format_Of_ExpireDate_IsNull_Then_Fail()
        {
            Assert.IsFalse(IsExpDate(null));
        }
        [TestMethod]
        public void Test10_IsExpDate_Format_Of_ExpireDate_IsEmpty_Then_Fail()
        {
            Assert.IsFalse(IsExpDate(string.Empty));
        }
        [TestMethod]
        public void Test11_IsExpDate_Format_Of_ExpireDate_IsContain_WhiteSpace_Then_Fail()
        {
            Assert.IsFalse(IsExpDate(expDateDef.Replace("1", " ")));
        }
        [TestMethod]
        public void Test12_IsExpDate_Format_Of_ExpireDate_IsContain_Alphabet_Then_Fail()
        {
            Assert.IsFalse(IsExpDate(expDateDef.Replace("0120","qwer")));
        }
        [TestMethod]
        public void Test13_IsExpDate_Length_Of_ExpireDate_IsShorter_Then_Fail()
        {
            Assert.IsFalse(IsExpDate(expDateDef.Substring(0,2)));
        }
        [TestMethod]
        public void Test14_IsExpDate_Format_Of_ExpireDate_IsLonger_Then_Fail()
        {
            Assert.IsFalse(IsExpDate(expDateDef+"1234"));
        }
        [TestMethod]
        public void Test15_IsExpDate_Month_Of_ExpireDate_IsOutofRange_Then_Fail()
        {
            Assert.IsFalse(IsExpDate(expDateDef.Replace("01","39")));
        }
        [TestMethod]
        public void Test16_IsExpDate_Year_Of_ExpireDate_IsOutofRange_Then_Fail()
        {
            Assert.IsFalse(IsExpDate(expDateDef.Replace("2020", "8765")));
        }
        [TestMethod]
        public void Test17_IsExpDate_Year_Of_ExpireDate_IsOutofDate_Then_Fail()
        {
            Assert.IsFalse(IsExpDate(expDateDef.Replace("2020", "2011")));
        }
        [TestMethod]
        public void Test18_IsExpDate_Format_Of_ExpireDate_IsInRange6_Then_Success()
        {           
            Assert.IsTrue(IsExpDate(expDateDef));
        }
        [TestMethod]
        public void Test19_IsExpDate_Format_Of_ExpireDate_IsNumber_Then_Success()
        {
            Assert.IsTrue(IsExpDate(expDateDef));
        }
        [TestMethod]
        public void Test20_IsExpDate_Month_Of_ExpireDate_IsInRange_Then_Success()
        {
            Assert.IsTrue(IsExpDate(expDateDef));
        }
        [TestMethod]
        public void Test21_IsExpDate_Year_Of_ExpireDate_IsUptoDate_Then_Success()
        {
            Assert.IsTrue(IsExpDate(expDateDef));
        }

        /*identifyCard*//*JCB Amex Visa MasterCard*/
        [TestMethod]
        public void Test22_checkCardType_CardNumber_StartWith3_Length16_ShouldBe_JCB_Success()
        {
            testValue.cardNum = cardNum16Def.Replace("0", "3");

            expecValue.cardType = "JCB";

            string resultValue = CheckCardType(testValue.cardNum);

            Assert.AreEqual(expecValue.cardType, resultValue);

        }

        [TestMethod]
        public void Test23_checkCardType_CardNumber_StartWith3_Length15_ShouldBe_Amex_Success()
        {
            testValue.cardNum = cardNum15Def.Replace("0", "3");

            expecValue.cardType = "Amex";

            string resultValue = CheckCardType(testValue.cardNum);

            Assert.AreEqual(expecValue.cardType, resultValue);

        }

        [TestMethod]
        public void Test24_checkCardType_CardNumber_StartWith4_ShouldBe_Visa_Success()
        {
            testValue.cardNum = cardNum16Def.Replace("0", "4");

            expecValue.cardType = "Visa";

            string resultValue = CheckCardType(testValue.cardNum);

            Assert.AreEqual(expecValue.cardType, resultValue);

        }

        [TestMethod]
        public void Test25_checkCardType_CardNumber_StartWith5_ShouldBe_MasterCard_Success()
        {
            testValue.cardNum = cardNum16Def.Replace("0", "5");
          
            expecValue.cardType = "MasterCard";

            string resultValue = CheckCardType(testValue.cardNum);

            Assert.AreEqual(expecValue.cardType, resultValue);

        }
        [TestMethod]
        public void Test26_checkCardStat_CardType_JCB_ShouldBe_Valid_All_Success()
        {
            testValue.cardNum = cardNum16Def.Replace("0", "3");
            testValue.expDate = expDateDef;

            expecValue.cardType = "JCB";
            expecValue.cardStat = "Valid";

            string cardType = CheckCardType(testValue.cardNum);
            string resultValue = CheckCardStat(testValue.expDate);

            Assert.AreEqual(expecValue.cardStat, resultValue);
        }
        [TestMethod]
        public void Test27_checkCardStat_CardType_JCB_CanNotBe_Invalid_Success()
        {
            testValue.cardNum = cardNum16Def.Replace("0", "3");
            testValue.expDate = expDateDef;

            expecValue.cardType = "JCB";
            expecValue.cardStat = "Invalid";

            string cardType = CheckCardType(testValue.cardNum);
            string resultValue = CheckCardStat(testValue.expDate);

            Assert.AreNotEqual(expecValue.cardStat, resultValue);
        }
        [TestMethod]
        public void Test28_checkCardStat_CardType_Visa_ShouldBe_Valid_When_ExpDate_IsLeapYear_Success()
        {
            testValue.cardNum = cardNum16Def.Replace("0", "4");
            testValue.expDate = expDateDef;

            expecValue.cardType = "Visa";
            expecValue.cardStat = "Valid";

            string cardType = CheckCardType(testValue.cardNum);
            string resultValue = CheckCardStat(testValue.expDate);

            Assert.AreEqual(expecValue.cardStat, resultValue);
        }
        [TestMethod]
        public void Test29_checkCardStat_CardType_Visa_ShouldBe_Invalid_When_ExpDate_IsNotLeapYear_Success()
        {
            testValue.cardNum = cardNum16Def.Replace("0", "4");
            testValue.expDate = expDateDef.Replace("020","023");

            expecValue.cardType = "Visa";
            expecValue.cardStat = "Invalid";

            string cardType = CheckCardType(testValue.cardNum);
            string resultValue = CheckCardStat(testValue.expDate);

            Assert.AreEqual(expecValue.cardStat, resultValue);
        }
        [TestMethod]
        public void Test30_checkCardStat_CardType_MasterCard_ShouldBe_Valid_When_ExpDate_IsPrimeNumber_Success()
        {
            testValue.cardNum = cardNum16Def.Replace("0", "5");
            testValue.expDate = expDateDef.Replace("2020", "2027");

            expecValue.cardType = "MasterCard";
            expecValue.cardStat = "Valid";

            string cardType = CheckCardType(testValue.cardNum);
            string resultValue = CheckCardStat(testValue.expDate);

            Assert.AreEqual(expecValue.cardStat, resultValue);
        }
        [TestMethod]
        public void Test31_checkCardStat_CardType_MasterCard_ShouldBe_Invalid_When_ExpDate_IsNotPrimeNumber_Success()
        {
            testValue.cardNum = cardNum16Def.Replace("0", "5");
            testValue.expDate = expDateDef;

            expecValue.cardType = "MasterCard";
            expecValue.cardStat = "Invalid";

            string cardType = CheckCardType(testValue.cardNum);
            string resultValue = CheckCardStat(testValue.expDate);

            Assert.AreEqual(expecValue.cardStat, resultValue);
        }
        [TestMethod]
        public void Test32_checkCardType_CardNumber_NotStartWith345_ShouldBe_Unknown_Success()
        {
            testValue.cardNum = cardNum16Def.Replace("0", "9");

            expecValue.cardType = "Unknown";

            string resultValue = CheckCardType(testValue.cardNum);

            Assert.AreEqual(expecValue.cardType, resultValue);
        }
        [TestMethod]
        public void Test33_checkCardStat_CardType_Unknown_ShouldBe_Invalid_Success()
        {
            testValue.cardNum = cardNum16Def.Replace("0", "9");
            testValue.expDate = expDateDef;

            expecValue.cardType = "Unknown";
            expecValue.cardStat = "Invalid";

            string cardType = CheckCardType(testValue.cardNum);
            string resultValue = CheckCardStat(testValue.expDate);

            Assert.AreEqual(expecValue.cardStat, resultValue);
        }

    }
}
