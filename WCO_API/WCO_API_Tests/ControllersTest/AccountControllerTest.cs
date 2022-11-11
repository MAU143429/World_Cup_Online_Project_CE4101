using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WCO_Api.Controllers;
using WCO_Api.WEBModels;
using Xunit;

namespace WCO_API_Tests.ControllersTest
{
    public class AccountControllerTest
    {
        AccountController accountController;
        AccountWEB account;
        public AccountControllerTest()
        {
            accountController = new AccountController();
            account = new AccountWEB()
            {
                birthdate = "12-12-2000", country = "CRC",
                email = "jj@gmail.com", isAdmin = false, lastname = "Mora", name = "Manu",
                nickname = "manumora", password = "12345678"

            };
        }

        [Fact]
        public async Task AddAccountTest () 
        {
            //Arrange
            //Act
            var result = await this.accountController.createAccount(this.account);
            //Assert
            Assert.IsType<CreatedResult>(result);
        }

        [Fact]
        public async Task getAccountByNicknameTest()
        {
            //Arrange
            string nickname = "manumora";
            //Act
            var result = await this.accountController.getAccountByNickname(nickname);
            //Assert
            Assert.IsType<List<AccountWEB>>(result);
        }

        [Fact]
        public async Task getInformationAccountByEmailTest()
        {
            //Arrange
            string email = "jj@gmail.com";
            //Act
            var result = await this.accountController.getInformationAccountByEmail(email);
            //Assert
            Assert.IsType<List<AccountWEB>>(result);
        }


        [Fact]
        public async Task getRoleAccountByEmailTest()
        {
            //Arrange
            string email = "jj@gmail.com";
            //Act
            var result = await this.accountController.getRoleAccountByEmail(email);
            //Assert
            Assert.False(result);
        }
    }
}
