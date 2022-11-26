using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WCO_Api.Controllers;
using WCO_Api.WEBModels;
using Xunit;
using WCO_Api.Repository;
using Moq;

namespace WCO_API_Tests.ControllersTest
{
    public class AccountControllerTest
    {

        AccountWEB accountMock;
        public AccountControllerTest()
        {
            accountMock = new AccountWEB()
            {
                birthdate = "12-12-2000", country = "CRC",
                email = "jj@gmail.com", isAdmin = false, lastname = "Mora", name = "Manu",
                nickname = "manumora", password = "12345678"

            };
        }

        /// <summary>
        /// Method <c>AddAccountTest</c> método que hace inserción haciendo uso del servicio del Repositoryo
        /// </summary>
        [Fact]
        public async Task AddAccountTestSuccess()
        {
            //Arrange

            var mockRepo = new Mock<IAccountRepository>();
            mockRepo.Setup(repo => repo.createNewAccount(this.accountMock)).ReturnsAsync(correctInsert());
            var controller = new AccountController(mockRepo.Object);
            //Act
            var result = await controller.createAccount(this.accountMock);
            //Assert
            var createdResult = Assert.IsType<CreatedResult>(result);

            Assert.Equal("Se creo un usuario exitosamente", createdResult.Value);

        }

        /// <summary>
        /// Method <c>AddAccountTest</c> método que hace inserción haciendo uso del servicio, se provee una inserción
        /// errónea en la salida para probar el caso cuando hay un StatusCode 500 que indica error en la inserción
        /// </summary>

        [Fact]
        public async Task AddAccountTestNotSuccess()
        {
            //Arrange

            var mockRepo = new Mock<IAccountRepository>();
            mockRepo.Setup(repo => repo.createNewAccount(this.accountMock)).ReturnsAsync(incorrectInsert());
            var controller = new AccountController(mockRepo.Object);
            //Act
            var result = await controller.createAccount(this.accountMock);
            //Assert
            var output = Assert.IsType<ObjectResult>(result);

            Assert.Equal(500, output.StatusCode.Value);

        }

        /// <summary>
        /// Method <c>AddAccountWithBadRequest</c> método que hace inserción nula haciendo uso del servicio, se provee una inserción
        /// errónea en la salida para probar el caso cuando hay un BadRequestObjectResult que indica error en la inserción
        /// </summary>
        [Fact]
        public async Task AddAccountWithBadRequest()
        {
            //Arrange

            var mockRepo = new Mock<IAccountRepository>();

            var controller = new AccountController(mockRepo.Object);
            controller.ModelState.AddModelError("Input", "Null entry detected");
            //Act
            var result = await controller.createAccount(null);
            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        /// <summary>
        /// Method <c>AddAccountWithBadRequest</c> método que hace inserción de cuenta haciendo uso del servicio, se provee una inserción
        /// errónea en la salida para probar el caso cuando hay un BadRequestObjectResult que indica que la cuenta ya existe
        /// </summary>
        [Fact]
        public async Task AddExistingAccountError()
        {
            //Arrange

            var mockRepo = new Mock<IAccountRepository>();

            var controller = new AccountController(mockRepo.Object);
            controller.ModelState.AddModelError("error", "Account already exists");
            //Act
            var result = await controller.createAccount(this.accountMock);
            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        /// <summary>
        /// Method <c>getAccountByNicknameTest</c> método que extrae todos los datos de un 
        /// usuario de acuerdo con el nickname consultado al servicio.
        /// </summary>
        [Fact]
        public async Task getAccountByNicknameTest()
        {
            //Arrange
            string nickname = "manumora";
            var mockRepo = new Mock<IAccountRepository>();
            mockRepo.Setup(repo => repo.getAccountByNickname(nickname)).ReturnsAsync(foundUser());
            var controller = new AccountController(mockRepo.Object);
            //Act
            var result = await controller.getAccountByNickname(nickname);
            //Assert
            Assert.Equal(nickname, result[0].nickname);
        }

        /// <summary>
        /// Method <c>getInformationAccountByEmailTest</c> método que extrae todos los datos de un 
        /// usuario de acuerdo con el email consultado al servicio.
        /// </summary>
        [Fact]
        public async Task getInformationAccountByEmailTest()
        {
            //Arrange
            string email = "jj@gmail.com";
            var mockRepo = new Mock<IAccountRepository>();
            mockRepo.Setup(repo => repo.getInformationAccountByEmail(email)).ReturnsAsync(foundUser());
            var controller = new AccountController(mockRepo.Object);
            //Act
            var result = await controller.getInformationAccountByEmail(email);
            //Assert
            Assert.Equal(email, result[0].email);
        }


        /// <summary>
        /// Method <c>getRoleAccountByEmailTest</c> método que extrae un booleano para indicar si el usuario consultado
        /// por email tiene rol de admin.
        /// </summary>
        [Fact]
        public async Task getRoleAccountByEmailTestSuccess()
        {
            //Arrange
            string email = "jj@gmail.com";
            var mockRepo = new Mock<IAccountRepository>();
            mockRepo.Setup(repo => repo.getRoleAccountByEmail(email)).ReturnsAsync(true);
            var controller = new AccountController(mockRepo.Object);
            //Act
            var result = await controller.getRoleAccountByEmail(email);
            //Assert
            Assert.True(result);
        }

        /// <summary>
        /// Method <c>getRoleAccountByEmailTest</c> método que extrae un booleano para indicar si el usuario consultado
        /// por email NO tiene rol de admin.
        /// </summary>
        [Fact]
        public async Task getRoleAccountByEmailTestNotSuccess()
        {
            //Arrange
            string email = "jj@gmail.com";
            var mockRepo = new Mock<IAccountRepository>();
            mockRepo.Setup(repo => repo.getRoleAccountByEmail(email)).ReturnsAsync(false);
            var controller = new AccountController(mockRepo.Object);
            //Act
            var result = await controller.getRoleAccountByEmail(email);
            //Assert
            Assert.False(result);
        }

        /// <summary>
        /// Method <c>loginAccountTest</c> método que consulta las credenciales del usuario a la
        /// base de datos para saber si está registrado en el sistema. 
        /// </summary>
        [Fact]
        public async Task loginAccountSuccess()
        {
            //Arrange
            LoginAccountWEB login = new LoginAccountWEB() { email = "jj@gmail.com", password = "12345678" };
            var mockRepo = new Mock<IAccountRepository>();
            mockRepo.Setup(repo => repo.getLoginAccountWEB(login.email)).ReturnsAsync(foundUser()[0]);
            var controller = new AccountController(mockRepo.Object);
            //Act
            var result = await controller.loginAccount(login);
            //Assert
            Assert.True(result);
        }


        private List<AccountWEB> foundUser()
        {
            var userInArray = new List<AccountWEB>();
            userInArray.Add(this.accountMock);
            return userInArray;
        }

        private int correctInsert()
        {
            return 1;
        }

        private int incorrectInsert()
        {
            return -1;
        }
    }

}
