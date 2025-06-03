🔄 Task

Implement Transaction Handling Logic in `TransactionController`

🎯 Objective

Implement a method called Handle within the TransactionController that processes incoming transactions based on their type.

🧩 Context

The API exposes a `TransactionController` responsible for processing various transaction requests.

Each transaction is represented by a model of type `TransactionRequest`, which includes a field for `TransactionType`.

✅ Specific Requirements

1. Based on the `TransactionType`, the method should return a response indicating which type was handled. 
> Handling logic can be simple, such as:
`return $"Transaction of type {request.TransactionType} was handled.";`

2. Request Validation (Optional but Recommended) Validate the incoming TransactionRequest:

> Ensure TransactionType is present and valid.

> Validate other relevant fields, if any (e.g., Amount should be greater than zero for monetary transactions).

> Return appropriate HTTP error codes (e.g., 400 Bad Request) with a helpful error message for invalid requests.

3. Integration Tests (Optional)
Create integration test cases to cover at least the following:

> Valid transaction handling for each supported TransactionType.

> Handling of unsupported or invalid TransactionType values.

> Validation failure scenarios (e.g., missing fields).

> Successful HTTP status codes and expected message responses.

> Tests should hit the actual HTTP endpoint (not just the controller logic in isolation).

