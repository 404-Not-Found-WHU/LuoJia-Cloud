using System.ComponentModel.DataAnnotations;

namespace 商品类测试
{
    public static class ProductValidator
    {
        public static ValidationResult ValidateProductInputs(
            string productName,
            int? manufacturerId,
            int? categoryId,
            string price,
            string quantity)
        {
            if (string.IsNullOrEmpty(productName))
                return new ValidationResult(false, "商品名称不能为空");

            if (!manufacturerId.HasValue || manufacturerId.Value <= 0)
                return new ValidationResult(false, "请选择有效的生产商");

            if (!categoryId.HasValue || categoryId.Value <= 0)
                return new ValidationResult(false, "请选择有效的商品类目");

            if (string.IsNullOrEmpty(price))
                return new ValidationResult(false, "价格不能为空");

            if (!int.TryParse(price, out _))
                return new ValidationResult(false, "价格必须是有效数字");

            if (string.IsNullOrEmpty(quantity))
                return new ValidationResult(false, "库存数量不能为空");

            if (!int.TryParse(quantity, out _))
                return new ValidationResult(false, "库存数量必须是有效数字");

            return new ValidationResult(true, "验证通过");
        }
    }
    public static class ProductQueryBuilder
    {
        public static string BuildInsertQuery(
            string productName,
            int manufacturerId,
            int categoryId,
            int quantity,
            int price)
        {
            return $"INSERT INTO ProductTb1 VALUES('{productName}',{manufacturerId},{categoryId},{quantity},{price})";
        }

        public static string BuildUpdateQuery(
            int productId,
            string productName,
            int manufacturerId,
            int categoryId,
            int quantity,
            int price)
        {
            return $"UPDATE ProductTb1 SET PName='{productName}',PManufact={manufacturerId},PCategory={categoryId},PQty={quantity},PPrice={price} WHERE PId={productId}";
        }

        public static string BuildDeleteQuery(int productId)
        {
            return $"DELETE FROM ProductTb1 WHERE PId={productId}";
        }
    }


    public class ValidationResult
    {
        public bool IsValid { get; }
        public string Message { get; }

        public ValidationResult(bool isValid, string message)
        {
            IsValid = isValid;
            Message = message;
        }
    }
}