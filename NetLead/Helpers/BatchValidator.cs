using NetLead.Records;

namespace NetLead.Helpers;

public static class BatchValidator
{
    public static IEnumerable<ValidationResult<T>> ValidateBatch<T>(IEnumerable<T> items, Func<T, bool> validationPredicate, string? defaultFailureMessage = "Item failed validation.")
    {
        if (items == null)
        {
            throw new ArgumentNullException(nameof(items), "The input 'items' collection cannot be null.");
        }
        if (validationPredicate == null)
        {
            throw new ArgumentNullException(nameof(validationPredicate), "The 'validationPredicate' cannot be null.");
        }

        var results = new List<ValidationResult<T>>();

        foreach (T item in items)
        {
            var isValid = validationPredicate(item);
            var errorMessage = isValid ? null : defaultFailureMessage;
            results.Add(new ValidationResult<T>(item, isValid, errorMessage));
        }

        return results;
    }
}
