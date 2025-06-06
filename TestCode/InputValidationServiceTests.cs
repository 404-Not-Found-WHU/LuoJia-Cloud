using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnlineSupermarketTuto.Services;
using System;

namespace OnlineSupermarketTuto.Services.Tests
{
    [TestClass()]
    public class InputValidationServiceTests
    {
        private InputValidationService _validationService;

        [TestInitialize]  // 添加这个特性，确保每个测试前都会执行
        public void Setup()
        {
            _validationService = new InputValidationService();
        }

        [TestMethod()]
        public void ValidateCredentials_EmptyInput_ReturnsErrorMessage()
        {
            // Arrange & Act
            var result1 = _validationService.ValidateCredentials("", "password");
            var result2 = _validationService.ValidateCredentials("test@test.com", "");
            var result3 = _validationService.ValidateCredentials("", "");

            // Assert
            Assert.IsFalse(result1.IsValid);
            Assert.AreEqual("请输入用户名和密码！", result1.Message);

            Assert.IsFalse(result2.IsValid);
            Assert.AreEqual("请输入用户名和密码！", result2.Message);

            Assert.IsFalse(result3.IsValid);
            Assert.AreEqual("请输入用户名和密码！", result3.Message);
        }

        [TestMethod()]
        public void ValidateCredentials_InvalidEmail_ReturnsErrorMessage()
        {
            // Arrange
            var invalidEmails = new[]
            {
                "plainstring",
                "missing@dot",
                "@domain.com",
                "test@.com",
                //"test@domain..com"
            };

            // Act & Assert
            foreach (var email in invalidEmails)
            {
                var result = _validationService.ValidateCredentials(email, "ValidPass1");

                Assert.IsFalse(result.IsValid);
                Assert.AreEqual("请输入有效的邮箱地址！", result.Message);
            }
        }

        [TestMethod()]
        public void ValidateCredentials_WeakPassword_ReturnsErrorMessage()
        {
            // Arrange
            var weakPasswords = new[]
            {
                "short",       // 太短
                "123456",      // 只有数字
                "letters",     // 只有字母
                //"N0S!ymb0ls",  // 不包含小写字母
                //"n0s!ymb0ls"   // 不包含大写字母
            };

            // Act & Assert
            foreach (var password in weakPasswords)
            {
                var result = _validationService.ValidateCredentials("valid@email.com", password);

                Assert.IsFalse(result.IsValid);
                Assert.AreEqual("密码必须至少6位，包含字母和数字！", result.Message);
            }
        }

        [TestMethod()]
        public void ValidateCredentials_ValidInput_ReturnsSuccess()
        {
            // Arrange
            var validCredentials = new[]
            {
                new { Email = "user@example.com", Password = "Pass123" },
                new { Email = "test.user@domain.com", Password = "1aB!cD@" },
                new { Email = "first.last@sub.domain.com", Password = "pAssw0rd" }
            };

            // Act & Assert
            foreach (var cred in validCredentials)
            {
                var result = _validationService.ValidateCredentials(cred.Email, cred.Password);

                Assert.IsTrue(result.IsValid);
                Assert.AreEqual("验证通过", result.Message);
            }
        }

        [TestMethod()]
        public void IsAdmin_CorrectAdminCredentials_ReturnsTrue()
        {
            // Act
            var result = _validationService.IsAdmin("Admin@admin.com", "password111");

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void IsAdmin_IncorrectCredentials_ReturnsFalse()
        {
            // Arrange
            var testCases = new[]
            {
                new { Email = "admin@admin.com", Password = "password111" },
                new { Email = "Admin@admin.com", Password = "Password111" },
                new { Email = "user@test.com", Password = "password111" },
                new { Email = "Admin@admin.com", Password = "wrongpass" }
            };

            // Act & Assert
            foreach (var testCase in testCases)
            {
                var result = _validationService.IsAdmin(testCase.Email, testCase.Password);
                Assert.IsFalse(result);
            }
        }
    }
}