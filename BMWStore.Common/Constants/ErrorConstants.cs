namespace BMWStore.Common.Constants
{
    public class ErrorConstants
    {
        public const string CantBeNullOrEmpty = "String cant be null or empty";
        public const string CantBeNullParameter = "Parameter cant be null";

        public const string EmptyCollection = "The collection was empty";

        public const string EmptyEnum = "Enum was empty";
        public const string IncorrectEnumValue = "Value dont exist in the enum";

        public const string IncorrectId = "Incorrect id";

        public const string IncorrectUser = "You cant do that action on that user";

        public const string IncorrectGenericType = "Incorrect generic type";

        public const string UnitOfWorkNoRowsAffected = "No rows affected in unit of work complete";

        public const string IncorrectPriceRange = "Incorrect price range";

        public const string TypeWasNotEnum = "Type with that name was not enum";

        public const string NotSignIn = "User was not sign in";

        public const string UpcomingStatusRequired = "Status should be upcoming";
        public const string StatusNotFound = "Status not found";
        public const string StatusIsNotUpcoming = "You can check only test drives with upcoming status";

        public const string RoleNotFound = "Role was not found";
    }
}
