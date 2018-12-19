using System;
using System.Collections.Generic;
using System.Linq;
using FirstExample.Core.Entities.Dto;
using FirstExample.Core.Entities.Domain;
using FirstExample.BusinessLogic.Helpers;
using FirstExample.Core.Interfaces.Repositories;
using FirstExample.Core.Interfaces.BusinessLogic;

namespace FirstExample.BusinessLogic.Concrete
{
    public class UserManager : IUserManager
    {
        //key = property, value = "error message"
        public Dictionary<string, string> Errors { get; private set; }

        private readonly IUnitOfWork _unit;
        private readonly IMailManager _mail;

        public UserManager(IUnitOfWork unit, IMailManager mail)
        {
            Errors = new Dictionary<string, string>();
            _unit = unit;
            _mail = mail;
        }

        public void CreateUser(UserRegistration user)
        {
            if (IsUniqueData(user.Name, user.Email))
            {
                User newUser = _unit.Users.Add(new User
                {
                    Name = user.Name,
                    Email = user.Email,
                    Password = user.Password,
                    RegistrationDate = DateTime.Now,
                    EmailConfirmed = false,
                    Token = TokenGenerator.GenerateToken()
                });
                _unit.Complete();
                SendConfirmation(newUser);
            }
        }

        public bool ActivateUser(string token)
        {

            Token tkn = _unit.Tokens.GetByToken(token);

            if(tkn != null)
            {
                User user = tkn.User;
                int res = DateTime.Compare(DateTime.Now, tkn.ExpirationDate);
                if (res <= 0)
                {
                    tkn.User.EmailConfirmed = true;
                    _unit.Tokens.Remove(tkn);
                    _unit.Users.Update(user);
                    _unit.Complete();
                    return true;
                }
                else
                {
                    Errors.Add("token", "Activation link has been expired.");
                    return false;
                }
            }
            Errors.Add("token", "The link is broken, please, try to login to get new activation link.");
            return false;
        }

        public bool LoginApproved(string name, string password)
        {
            User user = _unit.Users.GetAll()
                                   .FirstOrDefault(x => x.Name == name);

            if (user != null)
            {
                if (user.Password == password)
                {
                    return true;
                }
                else
                {
                    Errors.Add("password", "incorrect pass");
                }
            }
            else
            {
                Errors.Add("name", "there is no name like this");
            }
            return false;

        }

        public void CreateNewToken(string name)
        {
            Token newToken = TokenGenerator.GenerateToken();
            User user = _unit.Users.GetByName(name);

            _unit.Tokens.Remove(user.Token);
            user.Token = newToken;
            _unit.Users.Update(user);
            _unit.Complete();
        }

        public bool IsUserActivated(string name)
        {
            return _unit.Users.GetByName(name).EmailConfirmed;
        }

        public void SendConfirmation(User user)
        {
            string link = $"<a href='http://www.forum-build.somee.com/account/ConfirmEmail/{user.Token.ConfirmationToken}'> activate</a>";
            //string link = $"<a href='http://localhost:51456/account/ConfirmEmail/{user.Token.ConfirmationToken}'> activate</a>";
            _mail.Send(link, user.Email, true);
        }

        #region helpers

        private bool IsUniqueData(string name, string email)
        {
            bool uniqueName = _unit.Users.GetAll()
                                         .Any(x => x.Name == name);

            bool uniqueEmail = _unit.Users.GetAll()
                                          .Any(x => x.Email == email);

            if (uniqueName)
            {
                Errors.Add("name", "This name is already used by other user.");
            }
            if (uniqueEmail)
            {
                Errors.Add("Email", "This Email is already used by other user.");
            }
            return !(uniqueName && uniqueEmail);
        }
        #endregion
    }
}
