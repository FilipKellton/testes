📚 Task

Implement Role-Based and Ownership-Based Authorization in .NET API

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

These will be compared against the id of the book being deleted.

## eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJPbmxpbmUgSldUIEJ1aWxkZXIiLCJpYXQiOjE3NDc4MTYyNzksImV4cCI6MTc3OTM1MjI3OSwiYXVkIjoid3d3LmV4YW1wbGUuY29tIiwic3ViIjoianJvY2tldEBleGFtcGxlLmNvbSIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IkVkaXRvciIsImJvb2tzIjoiMSwyLDMifQ.MzoGVPLOSvLLiXpF4OGRh9vrICaZsAPM7RM-dE9uOqM