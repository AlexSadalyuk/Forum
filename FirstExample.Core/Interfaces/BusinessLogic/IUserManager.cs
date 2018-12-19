using System.Collections.Generic;
using FirstExample.Core.Entities.Dto;
using FirstExample.Core.Entities.Domain;

namespace FirstExample.Core.Interfaces.BusinessLogic
{
    public interface IUserManager
    {
        Dictionary<string, string> Errors { get; }

        void CreateUser(UserRegistration user);
        void SendConfirmation(User user);
        void CreateNewToken(string name);

        bool LoginApproved(string name, string password);
        bool ActivateUser(string token);
        bool IsUserActivated(string name);

    }
}
