using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using mpNuget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.SMS
{
    public class SMSRepository : ISMSRepository
    {
        private readonly IConfiguration _configuration;

        public SMSRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> SendMessage(string phoneNumber, string text)
        {
            RestClient soapClient = new RestClient(_configuration["smsConfig:userName"], _configuration["smsConfig:password"]);
            var result = soapClient.Send(phoneNumber, _configuration["smsConfig:phoneNumber"], text, true);
            if(result.RetStatus == 1)
                return true;
            return false;
        }

    }
}
