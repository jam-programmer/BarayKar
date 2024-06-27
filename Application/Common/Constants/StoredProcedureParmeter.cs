using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Const
{
    public class StoredProcedureParmeter
    {
        public required string ParmeterName { set; get; }
        public required DbType ParmeterType { set; get; }
        public required object ParmeterValue { set; get; }
    }
}
