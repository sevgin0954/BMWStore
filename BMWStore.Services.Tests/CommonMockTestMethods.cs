using BMWStore.Entities;
using BMWStore.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BMWStore.Services.Tests
{
    public static class CommonMockTestMethods
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

        public static void SetupMockedSignInManager(Mock<SignInManager<User>> mock, bool isSignIn)
        {
            mock.Setup(sm => sm.IsSignedIn(It.IsAny<ClaimsPrincipal>()))
                .Returns(isSignIn);
        }

        public static Mock<SignInManager<User>> GetMockedSignInManager(UserManager<User> userManager)
        {
            var mockedSignInManager = GetSignInManager(userManager);

            return mockedSignInManager;
        }

        public static Mock<SignInManager<User>> GetMockedSignInManager()
        {
            var userManager = GetMockedUserManager().Object;
            var mockedSignInManager = GetSignInManager(userManager);

            return mockedSignInManager;
        }

        private static Mock<SignInManager<User>> GetSignInManager(UserManager<User> userManager)
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

        public static void SetupMockedCludinaryServiceUploadPicturesAsync(
            Mock<ICloudinaryService> mock, 
            params string[] returnPictureUrls)
        {
            mock.Setup(cs => cs.UploadPicturesAsync(It.IsAny<IEnumerable<IFormFile>>()))
                .Returns(Task.FromResult<IEnumerable<string>>(returnPictureUrls));
        }
    }
}
