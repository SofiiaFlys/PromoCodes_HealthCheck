using NUnit.Framework;
using System;
using System.Collections.Generic;
using TakePromoCodes;
using Moq;

namespace PromoCodes_Test
{
    public class PromoCodesTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void DuplicatesInOneFile_HasOneDuplicate_Test()
        {
            // Arrange
            List<String> expected = new List<String>();
            expected.Add("ABC");
            String clientCode = "SZGR";
            DateTime expiryDate = DateTime.Now;
            DateTime creationDate = DateTime.Now;
            List<String> codes = new List<String>();
            codes.Add("ABC");
            codes.Add("AQWEC");
            codes.Add("ABC");
            PromoCodes promoCodes = new PromoCodes(clientCode, expiryDate, creationDate, codes);


            // Act
            List<String> duplicates = promoCodes.DuplicatesInOneFile();

            // Assert

            Assert.AreEqual(expected, duplicates);
        }

        [Test]
        public void DuplicatesInOneFile_NoDuplicates_Test()
        {
            // Arrange
            List<String> expected = new List<String>();
            String clientCode = "SZGR";
            DateTime expiryDate = DateTime.Now;
            DateTime creationDate = DateTime.Now;
            List<String> codes = new List<String>();
            codes.Add("ABC");
            codes.Add("AQWEC");
            codes.Add("ABCD");
            PromoCodes promoCodes = new PromoCodes(clientCode, expiryDate, creationDate, codes);


            // Act
            List<String> duplicates = promoCodes.DuplicatesInOneFile();

            // Assert

            Assert.AreEqual(expected, duplicates);
        }

        [Test]
        public void DuplicatesInOneFile_HasManyDuplicates_Test()
        {
            // Arrange
            List<String> expected = new List<String>();
            expected.Add("AAA");
            expected.Add("ABC");
            String clientCode = "SZGR";
            DateTime expiryDate = DateTime.Now;
            DateTime creationDate = DateTime.Now;
            List<String> codes = new List<String>();
            codes.Add("ABC");
            codes.Add("AQWEC");
            codes.Add("ABC");
            codes.Add("AAA");
            codes.Add("AAA");
            PromoCodes promoCodes = new PromoCodes(clientCode, expiryDate, creationDate, codes);


            // Act
            List<String> duplicates = promoCodes.DuplicatesInOneFile();

            // Assert
            Assert.Contains(expected[0],duplicates);
            Assert.Contains(expected[1], duplicates);
        }

        [Test]
        public void DuplicatesInOneFile_NoCodes_Test()
        {
            // Arrange
            List<String> expected = new List<String>();

            String clientCode = "SZGR";
            DateTime expiryDate = DateTime.Now;
            DateTime creationDate = DateTime.Now;
            List<String> codes = new List<String>();

            PromoCodes promoCodes = new PromoCodes(clientCode, expiryDate, creationDate, codes);


            // Act
            List<String> duplicates = promoCodes.DuplicatesInOneFile();

            // Assert
            Assert.AreEqual(expected, duplicates);
        }

        [Test]
        public void FindExistingPromoCodes_HasOneDuplicate_Test()
        {
            // Arrange
            List<String> expected = new List<String>();
            expected.Add("ABC");
            String clientCode = "SZGR";
            DateTime expiryDate = DateTime.Now;
            DateTime creationDate = DateTime.Now;
            List<String> codes = new List<String>();
            codes.Add("ABC");
            codes.Add("AQWEC");
            codes.Add("ABCD");
            PromoCodes promoCodes = new PromoCodes(clientCode, expiryDate, creationDate, codes);

            String clientCodeExisting = "SZGR";
            DateTime expiryDateExisting = DateTime.Now;
            DateTime creationDateExisting = DateTime.Now;
            List<String> codesExisting = new List<String>();
            codesExisting.Add("ABC");
            codesExisting.Add("AQWECQWERTY");
            codesExisting.Add("AAAAAAAABCD");
            PromoCodes promoCodesExisting = new PromoCodes(clientCodeExisting, expiryDateExisting, creationDateExisting, codesExisting);


            // Act
            List<String> existingCodes = promoCodes.FindAlreadyExistingPromoCodes(promoCodesExisting);

            // Assert

            Assert.AreEqual(expected, existingCodes);
        }

        [Test]
        public void FindExistingPromoCodes_HasNoDuplicates_Test()
        {
            // Arrange
            List<String> expected = new List<String>();
            String clientCode = "SZGR";
            DateTime expiryDate = DateTime.Now;
            DateTime creationDate = DateTime.Now;
            List<String> codes = new List<String>();
            codes.Add("ABC");
            codes.Add("AQWEC");
            codes.Add("ABCD");
            PromoCodes promoCodes = new PromoCodes(clientCode, expiryDate, creationDate, codes);

            String clientCodeExisting = "SZGR";
            DateTime expiryDateExisting = DateTime.Now;
            DateTime creationDateExisting = DateTime.Now;
            List<String> codesExisting = new List<String>();
            codesExisting.Add("AAA");
            codesExisting.Add("BBN");
            codesExisting.Add("CCD");
            PromoCodes promoCodesExisting = new PromoCodes(clientCodeExisting, expiryDateExisting, creationDateExisting, codesExisting);


            // Act
            List<String> existingCodes = promoCodes.FindAlreadyExistingPromoCodes(promoCodesExisting);

            // Assert

            Assert.AreEqual(expected, existingCodes);
        }

        [Test]
        public void ReadCodesFromFile_Test()
        {
            // Arrange
            String path = @"D:\SOFT_SERVE_OLD_PC\Promo codes\promo_codes.txt";
            String clientCode = "SZGR";
            DateTime expiryDate = DateTime.Now;
            DateTime creationDate = DateTime.Now;
            List<String> codes = new List<String>();
            PromoCodes requiredPromoCodes = new PromoCodes(clientCode, expiryDate, creationDate, codes);

            List<String> expected = new List<String>();
            expected.Add("AAA");
            expected.Add("BBB");
            expected.Add("CCC");

            // Act
            requiredPromoCodes.ReadCodesFromFile(path);

            // Assert
            Assert.AreEqual(expected, requiredPromoCodes.Codes);
        }

        [Test]
        public void ReadCodesFromFile_EmptyLines_Test()
        {
            // Arrange
            String path = @"D:\SOFT_SERVE_OLD_PC\Promo codes\promo_codes_empty_lines.txt";
            String clientCode = "SZGR";
            DateTime expiryDate = DateTime.Now;
            DateTime creationDate = DateTime.Now;
            List<String> codes = new List<String>();
            PromoCodes requiredPromoCodes = new PromoCodes(clientCode, expiryDate, creationDate, codes);

            List<String> expected = new List<String>();
            expected.Add("AAA");
            expected.Add("BBB");
            expected.Add("CCC");

            // Act
            requiredPromoCodes.ReadCodesFromFile(path);

            // Assert
            Assert.AreEqual(expected, requiredPromoCodes.Codes);
        }

        [Test]
        public void ReadCodesFromFile_WhiteSpacesLines_Test()
        {
            // Arrange
            String path = @"D:\SOFT_SERVE_OLD_PC\Promo codes\Promo_codes_white_spaces_line.txt";
            String clientCode = "SZGR";
            DateTime expiryDate = DateTime.Now;
            DateTime creationDate = DateTime.Now;
            List<String> codes = new List<String>();
            PromoCodes requiredPromoCodes = new PromoCodes(clientCode, expiryDate, creationDate, codes);

            List<String> expected = new List<String>();
            expected.Add("AAA");
            expected.Add("BBB");
            expected.Add("CCC");

            // Act
            requiredPromoCodes.ReadCodesFromFile(path);

            // Assert
            Assert.AreEqual(expected, requiredPromoCodes.Codes);
        }

        [Test]

        public void ReadCodesFromFile_FileNotExisted_Test()
        {
            // Arrange
            String path = @"D:\SOFT_SERVE_OLD_PC\Promo codes\Promo_codes_not_existed.txt";
            String clientCode = "SZGR";
            DateTime expiryDate = DateTime.Now;
            DateTime creationDate = DateTime.Now;
            List<String> codes = new List<String>();
            PromoCodes requiredPromoCodes = new PromoCodes(clientCode, expiryDate, creationDate, codes);
            List<String> expected = new List<String>();

            // Act
            requiredPromoCodes.ReadCodesFromFile(path);

            // Assert
            Assert.AreEqual(expected, requiredPromoCodes.Codes);
        }

        [Test]
        public void ReadCodesFromFile_FileNotExisted_CatchError_Test()
        {
            // Arrange
            String path = @"D:\SOFT_SERVE_OLD_PC\Promo codes\Promo_codes_not_existed.txt";
            String clientCode = "SZGR";
            DateTime expiryDate = DateTime.Now;
            DateTime creationDate = DateTime.Now;
            List<String> codes = new List<String>();
            PromoCodes requiredPromoCodes = new PromoCodes(clientCode, expiryDate, creationDate, codes);
            List<String> expected = new List<String>();

            // Assert
            Assert.Catch(() => requiredPromoCodes.ReadCodesFromFile_WithoutTryCatch(path));
        }
    }

    public class PromoCodesManipulationTests
    {
        //[Test]
        //public void ChangePromoCodesFormat_SZGR_Test()
        //{
        //    // Arrange
        //    String path = @"D:\SOFT_SERVE_OLD_PC\Promo codes\Promo_codes_szgr.txt";
        //    String clientCode = "SZGR";
        //    DateTime expiryDate = DateTime.Now;
        //    DateTime creationDate = DateTime.Now;
        //    List<String> codes = new List<String>();
        //    codes.Add("632/S,\"AAA\"");
        //    codes.Add("601R/O-LH,\"BBB\"");
        //    codes.Add("650/O-LH,\"CCC\"");
        //    var mock = new Mock<PromoCodes>();
        //    mock
        //        .Setup(p => p.ReadCodesFromFile(path))
        //        .Returns(new PromoCodes(clientCode, expiryDate, creationDate, codes));

          


        //    PromoCodes requiredPromoCodes = new PromoCodes(clientCode, expiryDate, creationDate, codes);
        //    PromoCodesManipulation promoCodesManipulation = new PromoCodesManipulation(requiredPromoCodes);

        //    List<String> expected = new List<String>();
        //    expected.Add("AAA");
        //    expected.Add("BBB");
        //    expected.Add("CCC");

        //    // Act
        //    promoCodesManipulation.ReadCodesFromFile(path);

        //    // Assert
        //    Assert.AreEqual(expected, promoCodesManipulation.RequiredPromoCodes.Codes);
        //}

    }
}