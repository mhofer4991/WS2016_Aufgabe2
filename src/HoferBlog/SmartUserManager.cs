using BlogStorage.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using BlogStorage;

namespace HoferBlog
{
    public class SmartUserManager<TUser> : UserManager<TUser> where TUser : class
    {
        private BlogRepository repository;

        public SmartUserManager(IUserStore<TUser> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<TUser> passwordHasher, IEnumerable<IUserValidator<TUser>> userValidators, IEnumerable<IPasswordValidator<TUser>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<TUser>> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            this.repository = new BlogRepository();
        }

        public async Task<IdentityResult> ChangeEmailAsync(TUser user, string newEmail, string token, string password)
        {
            if (await this.CheckPasswordAsync(user, password))
            {
                return await this.ChangeEmailAsync(user, newEmail, token);
            }

            return IdentityResult.Failed(new IdentityError() { Code = "PasswordMismatch", Description = "Incorrect password." });
        }
    }
}