using System.Text.RegularExpressions;

namespace OnlineSupermarketTuto.Services
{
    public class InputValidationService
    {
        private const string EmailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        private const string PasswordPattern = @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d@$!%*?&]{6,}$";

        public ValidationResult ValidateCredentials(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                return new ValidationResult(false, "请输入用户名和密码！");
            }

            else if (!Regex.IsMatch(email, EmailPattern))
            {
                return new ValidationResult(false, "请输入有效的邮箱地址！");
            }

            else if (!Regex.IsMatch(password, PasswordPattern))
            {
                return new ValidationResult(false, "密码必须至少6位，包含字母和数字！");
            }

            return new ValidationResult(true, "验证通过");
        }

        public bool IsAdmin(string email, string password)
        {
            return email == "Admin@admin.com" && password == "password111";
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
