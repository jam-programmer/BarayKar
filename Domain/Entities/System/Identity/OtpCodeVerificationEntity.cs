using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.System.Identity
{
    public class OtpCodeVerificationEntity : BaseEntity
    {
        public string Code { get; set; }
        public DateTime ExpireTime { get; set; }
        public Guid UserId { get; set; }
        public bool IsUsed { get; set; }
    }
}
