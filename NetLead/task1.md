📚 Task: Implement Role-Based and Ownership-Based Authorization in .NET API

🎯 Objective
Enhance the security of the BooksController in an existing .NET API by implementing granular authorization rules based on user roles and resource ownership.

🧩 Context
The API is configured to accept JSON Web Tokens (JWT) for authentication.

JWT tokens include:

User roles (e.g., "Admin", "Editor")

A list of book IDs authored by the user (e.g., "Books": "1,2,3")

✅ Specific Requirements
🔐 GET Method Authorization
The GET method in BooksController must be accessible only to users who have either of the following roles:

"Admin"

"Editor"

🔐 DELETE Method Authorization
The DELETE method in BooksController must be accessible under one of the following conditions:

The user has the "Admin" role.

The user is the author of the specific book being deleted.

The user’s authored book IDs will be extracted from the Books claim in the JWT.

These will be compared against the AuthorId of the book being deleted.

