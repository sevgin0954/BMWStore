using BMWStore.Common.Constants;
using System;

namespace BMWStore.Common.Validation
{
    // TODO: Rename to entityvalidator
    public class UnitOfWorkValidator
    {
        public static void ValidateUnitOfWorkCompleteChanges(int rowsAffected)
        {
            if (rowsAffected == 0)
            {
                throw new Exception(ErrorConstants.UnitOfWorkNoRowsAffected);
            }
        }
    }
}
