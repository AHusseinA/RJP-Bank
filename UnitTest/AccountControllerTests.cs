//using Account.Service;
//using AccountMicroService.Controllers;
//using DTO;
//using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTest
{
    public class AccountControllerTests
    {
        //[Fact]
        //public async Task GetAccountById_ReturnsOkObjectResult_WhenProductExists()
        //{
        //    // Arrange
        //    var mockService = new Mock<IAccountService>();
        //    var expectedProduct = new AccountDetailsInfo { Id = 1, FirstName = "Hussein", LastName = "Hammoud" };
        //    mockService.Setup(service => service.GetAccountDetailsAsync(1)).ReturnsAsync(expectedProduct);
        //    var controller = new AccountController(mockService.Object);

        //    // Act
        //    var result = await controller.GetAccountByCustomerId(1);

        //    // Assert
        //    var okResult = Assert.IsType<OkObjectResult>(result);
        //    var actualProduct = Assert.IsType<AccountDetailsInfo>(okResult.Value);
        //    Assert.Equal(expectedProduct.Id, actualProduct.Id);
        //    Assert.Equal(expectedProduct.FirstName, actualProduct.FirstName);
        //    Assert.Equal(expectedProduct.LastName, actualProduct.LastName);
        //}

        //[Fact]
        //public async Task CreateProduct_ReturnsCreatedAtActionResult_WhenProductIsValid()
        //{
        //    // Arrange
        //    var mockService = new Mock<IAccountService>();
        //    var newAccountRequest = new CreateAccountInfo { CustomerId = 6, InitialCredit = 1000 };
        //    var expectedAccount = new AccountInfo { Id = 9, Balance = newAccountRequest.InitialCredit, CustomerId = newAccountRequest.CustomerId };
        //    var expectedAccountDetailInfo = new AccountDetailsInfo { Id = 9, Balance = newAccountRequest.InitialCredit,};

        //    mockService.Setup(service => service.CreateAccountAsync(It.IsAny<CreateAccountInfo>()))
        //        .ReturnsAsync((CreateAccountInfo p) =>
        //        {
        //            var account = new AccountInfo { Balance = p.InitialCredit, CustomerId = p.CustomerId };
        //            account.Id = expectedAccount.Id;
        //            return account;
        //        });

        //    mockService.Setup(service => service.GetAccountDetailsAsync(6))
        //        .ReturnsAsync(expectedAccountDetailInfo);

        //    var controller = new AccountController(mockService.Object);

        //    // Act
        //    var result = await controller.GetAccountByCustomerId(6);

        //    // Assert
        //    var okResult = Assert.IsType<OkObjectResult>(result);
        //    var account = Assert.IsType<AccountDetailsInfo>(okResult.Value);
        //    Assert.Equal(expectedAccount.Id, account.Id);
        //    Assert.Equal(expectedAccount.Balance, account.Balance);

        //}
    }
}
