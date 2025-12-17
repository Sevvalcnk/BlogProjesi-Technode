using Microsoft.AspNetCore.Identity;

namespace BlogProjesi.Models
{
    public class TurkceIdentityHatalari : IdentityErrorDescriber
    {
        public override IdentityError DuplicateEmail(string email)
        {
            return new IdentityError { Code = "DuplicateEmail", Description = $"Bu '{email}' e-posta adresi zaten kullanımda." };
        }

        public override IdentityError DuplicateUserName(string userName)
        {
            return new IdentityError { Code = "DuplicateUserName", Description = $"Bu '{userName}' kullanıcı adı zaten alınmış." };
        }

        public override IdentityError PasswordTooShort(int length)
        {
            return new IdentityError { Code = "PasswordTooShort", Description = $"Şifreniz en az {length} karakter olmalıdır." };
        }
    }
}