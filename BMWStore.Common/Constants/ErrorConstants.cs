﻿namespace BMWStore.Common.Constants
{
    public class ErrorConstants
    {
        public const string EmptyCollection = "The collection was empty";

        public const string EmptyEnumException = "Enum was empty";
        public const string IncorrectEnumValue = "Value dont exist in the enum";

        public const string IncorrectId = "Incorrect id";

        public const string IncorrectUser = "You cant do that action on that user";

        public const string UnitOfWorkNoRowsAffected = "No rows affected in unit of work complete";

        public const string IncorrectPriceRange = "Incorrect price range";

        public const string UpcomingStatusRequired = "Status should be upcoming";
        public const string StatusNotFound = "Status not found";
        public const string StatusIsNotUpcoming = "You can check only test drives with upcoming status";

        public const string RoleNotFound = "Role was not found";
    }
}
