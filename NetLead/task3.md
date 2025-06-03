🚦 Task

Generic Batch Validator

🎯 Objective

Implement a generic static method that processes a batch of items, applying a custom validation rule to each, and returns a comprehensive result for the entire batch. The task focuses on effective use of C# generics, LINQ, and testability.

🧩 Context

In various systems, there's a need to process collections of data items and apply a common validation or filtering logic to them. This task requires you to build a reusable utility method that can take any type of data item and a custom validation predicate, returning clear results for each item in the batch.

✅ Specific Requirements

Define a Generic Result Type:

Create a public generic class or record called `ValidationResult<T>` where `T` represents the type of the item being validated.

It should have the following properties:

`T Item: The original item that was validated.`

`bool IsValid: Indicates whether the item passed the validation.`

`string? ErrorMessage: An optional message providing details if IsValid is false.`

Implement the Generic Validation Method:

Create a public static class (e.g., BatchValidator).

Within this class, implement the following static generic method:

`public static IEnumerable<ValidationResult<T>> ValidateBatch<T>(
    IEnumerable<T> items,
    Func<T, bool> validationPredicate,
    string? defaultFailureMessage = "Item failed validation." // Optional default message for failed items
)
`