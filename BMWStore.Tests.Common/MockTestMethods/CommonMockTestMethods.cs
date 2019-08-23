﻿using BMWStore.Entities;
using BMWStore.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace BMWStore.Tests.Common.MockTestMethods
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

        public static void SetupMockedUserManagerIsInRoleAsync(Mock<UserManager<User>> mock,  bool returns)
        {
            mock.Setup(um => um.IsInRoleAsync(It.IsAny<User>(), It.IsAny<string>()))
                .Returns(Task.FromResult(returns));
        }

        public static void SetupMockedDistributedCacheGetAsync(Mock<IDistributedCache> mock, string key, object obj)
        {
            var objAsByteArray = JSonHelper.Serialize(obj);
            if (obj == null)
            {
                objAsByteArray = null;
            }
            mock.Setup(m => m.GetAsync(key, It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(objAsByteArray));
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
    }
}
