using System.Data.Entity.Validation;
using System.Text;

namespace ChecklistManager.Repository
{
    internal static class RepositoryHelper
    {
        internal static StringBuilder GetValidationErrorMessageForEntity(DbEntityValidationException databaseEx)
        {
            StringBuilder validationErrorString = new StringBuilder();
            foreach (var validationErrors in databaseEx.EntityValidationErrors)
            {
                GetValidationErrorMessages(validationErrorString, validationErrors);
            }

            return validationErrorString;
        }

        private static void GetValidationErrorMessages(StringBuilder validationErrorString, DbEntityValidationResult validationErrors)
        {
            foreach (var validationError in validationErrors.ValidationErrors)
            {
                var message = GetValidationMessage(validationError, validationErrors);
                validationErrorString.AppendLine(message);
            }
        }

        private static string GetValidationMessage(DbValidationError validationError, DbEntityValidationResult validationErrors)
        {
            return string.Format(
                "Entity: {0}, Property: {1}, Error: {2}",
                validationErrors.Entry.Entity.GetType().Name,
                validationError.PropertyName,
                validationError.ErrorMessage);
        }
    }
}
