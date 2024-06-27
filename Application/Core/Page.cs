using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core
{
    public static class Page
    {
        public static int PageCount(this int count, int take)
        {
            int pageCount = count / take;
            if (count % take != 0)
            {
                pageCount++;
            }

            return pageCount;
        }
    }
}
