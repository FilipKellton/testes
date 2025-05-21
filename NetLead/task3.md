🚦 Task

Implement Rate Limiting on API Endpoints

🎯 Objective

Enhance the stability and security of the API by implementing rate limiting, restricting the number of requests a client can make within a specified time window.

🧩 Context

The API is currently open to authenticated or anonymous users without any restrictions on how frequently endpoints can be accessed. This makes it vulnerable to:

Abuse by high-frequency clients

Accidental or malicious denial of service

Resource overuse under load

Implementing rate limiting will help mitigate these risks and ensure fair usage across consumers.

✅ Specific Requirements

🔒 Rate Limiting Behavior
Clients should be limited to a configurable number of requests per a defined time window.

Example rule (to be configurable):

100 requests per client per 15 minutes

When the limit is exceeded:

The API should return HTTP 429 Too Many Requests

Include a Retry-After header indicating when the client can retry

🎯 Scope of Rate Limiting

Apply rate limiting globally across all endpoints

The implementation should work per client, where a client may be identified by:

Authenticated user ID (preferred)

IP address (for anonymous users or fallback)