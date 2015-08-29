using System;
using System.Security.Principal;
using System.Web.Security;

namespace Notus.Web.Core.Models
{
    [Serializable]
    public class NotusUser : IIdentity
    {
        public NotusUser()
        {
        }

        public NotusUser(string name, string displayName, string userId)
        {
            Name = name;
            DisplayName = displayName;
            UserId = userId;
        }

        public NotusUser(string name, string displayName, string userId, string roleName)
        {
            Name = name;
            DisplayName = displayName;
            UserId = userId;
            RoleName = roleName;
        }

        public NotusUser(string name, UserInfo userInfo)
            : this(name, userInfo.DisplayName, userInfo.UserId, userInfo.RoleName)
        {
            if (userInfo == null) throw new ArgumentNullException("userInfo");
            UserId = userInfo.UserId;
        }

        public NotusUser(FormsAuthenticationTicket ticket)
            : this(ticket.Name, UserInfo.FromString(ticket.UserData))
        {
            if (ticket == null) throw new ArgumentNullException("ticket");
        }

        public string DisplayName { get; private set; }
        public string RoleName { get; private set; }
        public string UserId { get; private set; }

        public string Name { get; }

        public string AuthenticationType
        {
            get { return "GoalSetterForms"; }
        }

        public bool IsAuthenticated
        {
            get { return true; }
        }
    }
}