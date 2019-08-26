using BMWStore.Common.Constants;
using BMWStore.Exceptions.Repositories;

namespace BMWStore.Common.Validation
{
    public class RepositoryValidator
    {
        public static void ValidateCompleteChanges(int rowsAffected)
        {
            if (rowsAffected == 0)
            {
                throw new RepositoryUpdateNoRowsAffectedException(ErrorConstants.UnitOfWorkNoRowsAffected);
            }
        }
    }
}
