using Shared.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Core.Interfaces
{
    public interface IEmailService
    {
        Task SendConfirmationEmail(UserEmailOptions userEmailOptions);
        Task SendResetPasswordConfirmation(UserEmailOptions userEmailOptions);
        Task SendAlertQuantityToAdmin(UserEmailOptions userEmailOptions);
    }
}
