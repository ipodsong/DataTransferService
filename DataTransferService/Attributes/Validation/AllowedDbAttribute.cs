using DataTransferService.Enums;
using System.ComponentModel.DataAnnotations;

namespace DataTransferService.Attributes.Validation
{
    public class AllowedDbTypesAttribute : ValidationAttribute
    {
        private static readonly HashSet<string> AllowedDbTypes = Enum.GetNames(typeof(DbTypeEnum)).ToHashSet(StringComparer.OrdinalIgnoreCase);

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not string dbType || !AllowedDbTypes.Contains(dbType))
            {
                return new ValidationResult($"DbType must be one of: {string.Join(", ", AllowedDbTypes)}");
            }
            return ValidationResult.Success;
        }
    }
}
