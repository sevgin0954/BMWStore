using System;

namespace BMWStore.Exceptions.Repositories
{
    public class RepositoryUpdateNoRowsAffectedException : Exception
    {
        public RepositoryUpdateNoRowsAffectedException()
            : base()
        {
        }

        public RepositoryUpdateNoRowsAffectedException(string message) 
            : base(message)
        {
        }

        public RepositoryUpdateNoRowsAffectedException(string message, Exception innerException) 
            : base(message, innerException)
        {
        }
    }
}
