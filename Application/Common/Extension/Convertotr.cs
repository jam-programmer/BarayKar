using Application.Common.Enum;
using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Extension
{
    public static class Convertotr
    {
        public static Status MapStatus(this StatusEnum status)
        {
            return status switch
            {
                StatusEnum.Accepted => Status.Accepted,
                StatusEnum.Rejected => Status.Rejected,
                StatusEnum.Waiting => Status.Waiting,
                _ => throw new NotImplementedException()
            };
        }
    }
}
