using BMWStore.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace BMWStore.Tests.Common.MockMethods
{
	public static class CommonGetMockMethods
	{
		public static Mock<SignInManager<User>> GetSetupedSignInManager(bool isSignIn)
		{
			var userManager = GetUserManager().Object;
			var mockedSignInManager = GetSignInManager(userManager);
			CommonSetupMockMethods.SetupMockedSignInManagerIsSignIn(mockedSignInManager, isSignIn);

			return mockedSignInManager;
		}

		public static Mock<UserManager<User>> GetSetupedUserManager(bool isInRole)
		{
			var mockedUserManager = GetUserManager();
			CommonSetupMockMethods.SetupMockedUserManagerIsInRoleAsync(mockedUserManager, isInRole);

			return mockedUserManager;
		}

		public static UserManager<User> GetSetupedUserManager(string userId)
		{
			var mockedUserManger = GetUserManager();
			CommonSetupMockMethods.SetupMockedUserManagerGetUserId(mockedUserManger, userId);

			return mockedUserManger.Object;
		}

		public static Mock<SignInManager<User>> GetSignInManager(UserManager<User> userManager)
		{
			var contextAccessor = new Mock<IHttpContextAccessor>().Object;
			var claimsFactory = new Mock<IUserClaimsPrincipalFactory<User>>().Object;
			var mockedSignInManager = new Mock<SignInManager<User>>(userManager, contextAccessor, claimsFactory, null, null, null);

			return mockedSignInManager;
		}

		public static Mock<UserManager<User>> GetUserManager()
		{
			var userStore = new Mock<IUserStore<User>>().Object;
			var mockedUserManager = new Mock<UserManager<User>>(userStore, null, null, null, null, null, null, null, null);

			return mockedUserManager;
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