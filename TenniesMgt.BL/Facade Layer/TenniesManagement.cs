using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenniesMgt.BL.DTO;
using TenniesMgt.Model;

namespace TenniesMgt.BL
{
    public class TenniesManagement
    {
        public static List<PlayersInfo> GetUser()
        {
            return BLUser.Get();
        }

        public static List<MyMatchInfo> GetMatch(long userId)
        {
            return BLUser.GetMatch(userId);
        }
    }
}
