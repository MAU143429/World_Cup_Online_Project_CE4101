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
        AccountController accountControllerMock;
        AccountWEB accountMock;
        public AccountControllerTest()
        {
            accountControllerMock = new AccountController();
            accountMock = new AccountWEB()
            {
                birthdate = "12-12-2000", country = "CRC",
                email = "jj@gmail.com", isAdmin = false, lastname = "Mora", name = "Manu",
                nickname = "manumora", password = "12345678"

            };
        }

        /// <summary>
        /// Method <c>AddAccountTest</c> método que hace inserción a la  base de datos de todos los datos de un 
        /// usuario de acuerdo con AccountWEB.
        /// </summary>
        [Fact]
        public async Task AddAccountTest() 
        {
            //Arrange
            //Act
            var result = await this.accountControllerMock.createAccount(this.accountMock);
            //Assert
            Assert.IsType<CreatedResult>(result);
        }

        /// <summary>
        /// Method <c>getAccountByNicknameTest</c> método que extrae todos los datos de un 
        /// usuario de acuerdo con el nickname consultado a la base de datos.
        /// </summary>
        [Fact]
        public async Task getAccountByNicknameTest()
        {
            //Arrange
            string nickname = "manumora";
            //Act
            var result = await this.accountControllerMock.getAccountByNickname(nickname);
            //Assert
            Assert.IsType<List<AccountWEB>>(result);
        }

        /// <summary>
        /// Method <c>getInformationAccountByEmailTest</c> método que extrae todos los datos de un 
        /// usuario de acuerdo con el email consultado a la base de datos.
        /// </summary>
        [Fact]
        public async Task getInformationAccountByEmailTest()
        {
            //Arrange
            string email = "jj@gmail.com";
            //Act
            var result = await this.accountControllerMock.getInformationAccountByEmail(email);
            //Assert
            Assert.IsType<List<AccountWEB>>(result);
        }


        /// <summary>
        /// Method <c>getRoleAccountByEmailTest</c> método que extrae un booleano para indicar si el usuario consultado
        /// por email tiene rol de admin.
        /// </summary>
        [Fact]
        public async Task getRoleAccountByEmailTest()
        {
            //Arrange
            string email = "jj@gmail.com";
            //Act
            var result = await this.accountControllerMock.getRoleAccountByEmail(email);
            //Assert
            Assert.False(result);
        }

        /// <summary>
        /// Method <c>loginAccountTest</c> método que consulta las credenciales del usuario a la
        /// base de datos para saber si está registrado en el sistema. 
        /// </summary>
        [Fact]
        public async Task loginAccountTest()
        {
            //Arrange
            LoginAccountWEB login = new LoginAccountWEB() { email = "jj@gmail.com", password = "12345678" };
            //Act
            var result = await this.accountControllerMock.loginAccount(login);
            //Assert
            Assert.True(result);
        }
    }
}
