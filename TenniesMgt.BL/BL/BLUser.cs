using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using TenniesMgt.Model;
using TenniesMgt.BL.DTO;

namespace TenniesMgt.BL
{
    internal class BLUser
    {
        public static List<PlayersInfo> Get()
        {
            try
            {
                using (TenniesMangementEntities TenniesContext = new TenniesMangementEntities())
                {
                    return
                        TenniesContext.Users
                        .Include("Matches").Include("Matches1").Include("Winners")
                        .Where
                        (x => x.Matches.Any(player1 => player1.PlayerID1 == x.ID) || x.Matches1.Any(player2 => player2.PlayerID2 == x.ID))
                        .Select
                        (x =>
                        new PlayersInfo
                        {
                            ID = x.ID,
                            FullName = x.FirstName + " " + x.LastName,
                            No_Of_Match = x.Matches.Count + x.Matches1.Count,
                            No_Of_Match_Win = x.Winners.Count,
                            No_Of_Match_Lose = (x.Matches.Count + x.Matches1.Count) - x.Winners.Count,
                            TotelPoint = x.Winners.Sum(va => va.Point)
                        })
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        public static List<MyMatchInfo> GetMatch(long userId)
        {
            try
            {
                if (userId == default(int))
                    throw new Exception("Invalid UserId");

                using (TenniesMangementEntities TenniesContext = new TenniesMangementEntities())
                {
                    return
                        TenniesContext.Matches
                        .Include("User").Include("User1").Include("Winners")
                        .Where(x => (x.User.ID == userId || x.User1.ID == userId))
                        .Select(x =>
                        new MyMatchInfo
                        {
                            ID = x.ID,
                            OpponentFullName = (x.User.ID != userId) ? x.User.FirstName + " " + x.User.LastName : x.User1.FirstName + " " + x.User1.LastName,
                            OpponentID = (x.User.ID != userId) ? x.User.ID : x.User1.ID,
                            MatchStatus = x.Winners != null ? x.Winners.Where(win => win.WinnerID == userId).Count() > 0 : false
                        })
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

    }
}
