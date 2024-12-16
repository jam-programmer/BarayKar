using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.SMS
{
    public interface ISMSRepository
    {
        Task<bool> SendMessage(string phoneNumber, string text);
    }
}
