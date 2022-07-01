using CST350_CLC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CST350_CLC.Services
{
    public static class SecurityService
    {
        readonly static SecurityDAO securityDAO = new SecurityDAO();

        public static UserModel GetUser(UserModel user)
        {
            return securityDAO.FindUserByNameAndPassword(user);
        }

        public static UserModel GetUserByUsername(string name)
        {
            return securityDAO.FindUserByUsername(name);
        }

        public static bool UserRegistered(UserModel user)
        {
            return securityDAO.CreateUser(user);
        }

        public static bool CreateGameSave(UserModel user, List<CellModel> cells)
        {
            string gameData = "";
            foreach(CellModel c in cells) {

                if (c.isBomb)
                {
                    gameData += -1 + ",";
                    continue;
                }

                gameData += c.CellState.ToString() + ",";
            }

            return securityDAO.CreateGameSave(user, gameData);
        }

        internal static List<GameSaveModel> GetUsersSavedGames(UserModel user)
        {
            return securityDAO.GetGameSaves(user);
        }

        public static List<string> GetGameById(int id)
        {
            return securityDAO.GetGameById(id);
        }

        internal static bool DeleteGameSave(int id)
        {
            return securityDAO.DeleteGameSave(id);
        }
    }
}
