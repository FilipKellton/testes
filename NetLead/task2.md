🔄 Task

Implement Transaction Handling Logic in TransactionController

🎯 Objective

Implement a method called Handle within the TransactionController that processes incoming transactions based on their type.

🧩 Context

The API exposes a `TransactionController` responsible for processing various transaction requests.

Each transaction is represented by a model of type `TransactionRequest`, which includes a field for `TransactionType`.

✅ Specific Requirements

Based on the `TransactionType`, the method should return a response indicating which type was handled.

Handling logic can be simple, such as:
`return $"Transaction of type {request.TransactionType} was handled.";`


