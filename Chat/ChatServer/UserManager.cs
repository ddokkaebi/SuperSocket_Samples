using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CSBaseLib;

namespace ChatServer
{
    class UserManager
    {
        Int64 UserSequenceNumber = 0;

        Dictionary<string, User> UserMap = new Dictionary<string, User>();

        public ERROR_CODE AddUser(string userID, string sessionID)
        {
            if (UserMap.ContainsKey(userID))
            {
                return ERROR_CODE.ADD_USER_DUPLICATION;
            }


            UserSequenceNumber += 1;
            
            var user = new User();
            user.Set(UserSequenceNumber, sessionID);
            
            UserMap.Add(userID, user);

            return ERROR_CODE.NONE;
        }

        public ERROR_CODE RemoveUser(string userID)
        {
            if(UserMap.Remove(userID) == false)
            {
                return ERROR_CODE.REMOVE_USER_SEARCH_FAILURE_USER_ID;
            }

            return ERROR_CODE.NONE;
        }

        public User GetUser(string userID)
        {
            User user = null;
            UserMap.TryGetValue(userID, out user);
            return user;
        }

        public ERROR_CODE 유저_인증_완료(string userID)
        {
            var user = GetUser(userID);

            if (user == null)
            {
                return ERROR_CODE.USER_AUTH_SEARCH_FAILURE_USER_ID;
            }

            if(user.SetAuthorized() == false)
            {
                return ERROR_CODE.USER_AUTH_ALREADY_SET_AUTH;
            }

            return ERROR_CODE.NONE;
        }
    }

    class User
    {
        Int64 SequenceNumber = 0;
        string SessionID;
        bool Authorized;

        public void Set(Int64 sequence, string sessionID)
        {
            Authorized = false;
            SequenceNumber = sequence;
            SessionID = sessionID;
        }

        public bool SetAuthorized()
        {
            if (Authorized)
            {
                return false;
            }

            Authorized = true;

            return true;
        }
    }
}
