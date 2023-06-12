using FluentValidation;
using SocialbookAPI.Application.Features.Commands.AppUser.CreateUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialbookAPI.Application.Validators.Users
{
    public class CreateUserValidator : AbstractValidator<CreateUserCommandRequest>
    {
        public CreateUserValidator()
        {
            RuleFor(p => p.NameSurname)
               .NotEmpty()
               .NotNull()
                  .WithMessage("Lütfen Ad Soyad kısmını Boş Bırakmayınız..")
               .MaximumLength(50)
               .MinimumLength(3)
                  .WithMessage("Lütfen 3 ile 50 karakter arasında giriniz..");

            RuleFor(p => p.Username)
               .NotEmpty()
               .NotNull()
                  .WithMessage("Lütfen Kullanıcı Adını Boş Bırakmayınız..")
               .MaximumLength(50)
               .MinimumLength(3)
                  .WithMessage("Lütfen kullanıcı adını 3 ile 50 karakter arasında giriniz..");

            RuleFor(p => p.Email)
               .NotEmpty()
               .NotNull()
                  .WithMessage("Lütfen Email Boş Bırakmayınız..")
               .EmailAddress()
                  .WithMessage("Lütfen bir email giriniz")
               .MaximumLength(70)
               .MinimumLength(6)
                  .WithMessage("Lütfen Email 6 ile 150 karakter arasında giriniz..");

            RuleFor(p => p.Password)
               .NotEmpty()
               .NotNull()
                  .WithMessage("Lütfen Şifreyi Boş Bırakmayınız..")
               .MaximumLength(50)
               .MinimumLength(3)
                  .WithMessage("Lütfen Şifreyi 3 ile 50 karakter arasında giriniz..");

            RuleFor(p => p.PasswordConfirm)
              .NotEmpty()
              .NotNull()
                 .WithMessage("Lütfen Şifreyi Boş Bırakmayınız..")
              .MaximumLength(50)
              .MinimumLength(3)
                 .WithMessage("Lütfen Şifreyi 3 ile 50 karakter arasında giriniz..");


        }
    }
}
