using Mediateur.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;





namespace Mediateur.Interfaces.Repositories
{
    public interface IAccount
    {
        Task<ApplicationUser?> FindByEmailAsync(string email);
        Task<string> GeneratePasswordResetTokenAsync(ApplicationUser user);
        Task<IdentityResult> ResetPasswordAsync(ApplicationUser user, string token, string newPassword);
    }

}