using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Abstraction.Services
{
    public interface IUserEmailManager
    {
        Task SendConfirmationEmail(int id, string email);
        Task<bool> ConfirmEmail(int id, string token);
    }
}
