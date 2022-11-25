using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WCO_Api.Repository;
using WCO_Api.WEBModels;

namespace WCO_API_Tests.RepositoryMock
{
    public class AccountRepositoryMock : IAccountRepository
    {
        public Task<int> createNewAccount(AccountWEB account)
        {
            throw new NotImplementedException();
        }

        public Task<List<AccountWEB>> getAccountByNickname(string nick)
        {
            throw new NotImplementedException();
        }

        public Task<List<AccountWEB>> getInformationAccountByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Task<AccountWEB> getLoginAccountWEB(string login)
        {
            throw new NotImplementedException();
        }

        public Task<bool> getRoleAccountByEmail(string email)
        {
            throw new NotImplementedException();
        }
    }
}
