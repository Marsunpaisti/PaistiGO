using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaistiGO
{
    public static class RefreshCheck
    {
        private static int _refreshId = 1;
        public static int refreshId
        {
            get
            {
                return _refreshId;
            }
            set
            {
                if (value == int.MaxValue) _refreshId = 0;
            }
        }

        public static bool NeedRefresh(this int value)
        {
            if (value != refreshId)
            {
                value = refreshId;
                return true;
            }
            return false;
        }
    }
}
