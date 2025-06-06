using Microsoft.VisualStudio.TestTools.UnitTesting;
using 商品类测试;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using 商品类测试;

namespace ProductValidatorTests
{
    [TestClass]
    public class ProductValidatorTests
    {
        [TestMethod]
        public void ValidateProductInputs_AllValid_ReturnsSuccess()
        {
            // Arrange
            string productName = "测试商品";
            int? manufacturerId = 1;
            int? categoryId = 1;
            string price = "100";
            string quantity = "10";

            // Act
            var result = ProductValidator.ValidateProductInputs(
                productName, manufacturerId, categoryId, price, quantity);

            // Assert
            Assert.IsTrue(result.IsValid);
            Assert.AreEqual("验证通过", result.Message);
        }

        [TestMethod]
        public void ValidateProductInputs_EmptyProductName_ReturnsError()
        {
            // Act
            var result = ProductValidator.ValidateProductInputs(
                "", 1, 1, "100", "10");

            // Assert
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual("商品名称不能为空", result.Message);
        }

        [TestMethod]
        public void ValidateProductInputs_InvalidManufacturer_ReturnsError()
        {
            // Test null manufacturer
            var result1 = ProductValidator.ValidateProductInputs(
                "测试商品", null, 1, "100", "10");

            // Test zero manufacturer
            var result2 = ProductValidator.ValidateProductInputs(
                "测试商品", 0, 1, "100", "10");

            // Assert
            Assert.IsFalse(result1.IsValid);
            Assert.AreEqual("请选择有效的生产商", result1.Message);
            Assert.IsFalse(result2.IsValid);
            Assert.AreEqual("请选择有效的生产商", result2.Message);
        }

        [TestMethod]
        public void ValidateProductInputs_InvalidCategory_ReturnsError()
        {
            // Test null category
            var result1 = ProductValidator.ValidateProductInputs(
                "测试商品", 1, null, "100", "10");

            // Test zero category
            var result2 = ProductValidator.ValidateProductInputs(
                "测试商品", 1, 0, "100", "10");

            // Assert
            Assert.IsFalse(result1.IsValid);
            Assert.AreEqual("请选择有效的商品类目", result1.Message);
            Assert.IsFalse(result2.IsValid);
            Assert.AreEqual("请选择有效的商品类目", result2.Message);
        }

        [TestMethod]
        public void ValidateProductInputs_InvalidPrice_ReturnsError()
        {
            // Test empty price
            var result1 = ProductValidator.ValidateProductInputs(
                "测试商品", 1, 1, "", "10");

            // Test non-numeric price
            var result2 = ProductValidator.ValidateProductInputs(
                "测试商品", 1, 1, "invalid", "10");

            // Assert
            Assert.IsFalse(result1.IsValid);
            Assert.AreEqual("价格不能为空", result1.Message);
            Assert.IsFalse(result2.IsValid);
            Assert.AreEqual("价格必须是有效数字", result2.Message);
        }

        [TestMethod]
        public void ValidateProductInputs_InvalidQuantity_ReturnsError()
        {
            // Test empty quantity
            var result1 = ProductValidator.ValidateProductInputs(
                "测试商品", 1, 1, "100", "");

            // Test non-numeric quantity
            var result2 = ProductValidator.ValidateProductInputs(
                "测试商品", 1, 1, "100", "invalid");

            // Assert
            Assert.IsFalse(result1.IsValid);
            Assert.AreEqual("库存数量不能为空", result1.Message);
            Assert.IsFalse(result2.IsValid);
            Assert.AreEqual("库存数量必须是有效数字", result2.Message);
        }
    }
}