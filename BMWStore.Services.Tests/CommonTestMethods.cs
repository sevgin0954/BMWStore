using BMWStore.Data.Repositories;
using BMWStore.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;
using System.Security.Claims;

namespace BMWStore.Services.Tests
{
    public static class CommonTestMethods
    {
        public static Mock<UserManager<User>> GetMockedUserManager()
        {
            var userStore = new Mock<IUserStore<User>>().Object;
            var mockedUserManager = new Mock<UserManager<User>>(userStore, null, null, null, null, null, null, null, null);

            return mockedUserManager;
        }

        public static void SetupMockedUserManagerGetUserId(Mock<UserManager<User>> mock, string userId)
        {
            mock.Setup(um => um.GetUserId(It.IsAny<ClaimsPrincipal>()))
                .Returns(userId);
        }

        public static Mock<SignInManager<User>> GetMockedSignInManager(UserManager<User> userManager)
        {
            var contextAccessor = new Mock<IHttpContextAccessor>().Object;
            var claimsFactory = new Mock<IUserClaimsPrincipalFactory<User>>().Object;
            var mockedSignInManager = new Mock<SignInManager<User>>(userManager, contextAccessor, claimsFactory, null, null, null);

            return mockedSignInManager;
        }

        public static void SutupMockedRequestCookieCollection(
            Mock<IRequestCookieCollection> mock,
            string key,
            string value)
        {
            mock.SetupGet(rc => rc[key])
                .Returns(value.ToString());
        }
    }
}
