using Mediateur.Models;
using System.Collections.Generic;

namespace Mediateur.Interfaces.Services
{
    public interface IAccountService
    {
        Task ForgotPasswordAsync(string email);
        Task<bool> ResetPasswordAsync(string email, string token, string newPassword);

    }

}
