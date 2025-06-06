using Microsoft.AspNetCore.Identity;

namespace TripWebLast.ServiceOnChanger;

public class ChineseIdentityChanger : IdentityErrorDescriber
{
    public override IdentityError DefaultError() => new IdentityError { Description = "发生未知错误" };
    public override IdentityError PasswordMismatch() => new IdentityError { Description = "密码不正确" };
    public override IdentityError InvalidToken() => new IdentityError { Description = "无效的令牌" };
    public override IdentityError LoginAlreadyAssociated() => new IdentityError { Description = "该登录已关联到其他账号" };

    public override IdentityError PasswordTooShort(int length) => new IdentityError
    {
        Description = $"密码长度不能少于 {length} 个字符"
    };

    public override IdentityError PasswordRequiresNonAlphanumeric() => new IdentityError
    {
        Description = "密码必须包含至少一个特殊字符"
    };

    public override IdentityError PasswordRequiresDigit() => new IdentityError
    {
        Description = "密码必须包含至少一个数字 (0-9)"
    };

    public override IdentityError PasswordRequiresLower() => new IdentityError
    {
        Description = "密码必须包含至少一个小写字母"
    };

    public override IdentityError PasswordRequiresUpper() => new IdentityError
    {
        Description = "密码必须包含至少一个大写字母"
    };

    public override IdentityError DuplicateEmail(string email) => new IdentityError
    {
        Description = $"邮箱 {email} 已被注册"
    };

    public override IdentityError InvalidEmail(string email)
    {
        return string.IsNullOrEmpty(email)
            ? new IdentityError { Description = "邮箱地址不能为空" }
            : new IdentityError { Description = $"邮箱 {email} 格式不正确" };
    }
    
}