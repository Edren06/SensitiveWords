# Sensitive Words

Simple .net Core project that demonstrates sensitive word management and message filtering (a.k.a "blooping"). This application includes:

# Features
### API
- Add, update, delete, and retrieve sensitive words.
- Filter messages and replace sensitive words with asterisks (`*`).
- Swagger documentation for easy API testing.
- Dapper Micro ORM implementation

### MVC Frontend
- Admin dashboard to manage sensitive words (CRUD operations).
- Chat page to demonstrate message filtering.
- Inline editing, search, and confirmation popups.

## Running the application
The application has been created in visual studio IDE, you will need to:
- Restore the attached database "SensitiveWords.bak" with MSSQL
- The API and the front end should be run simultaneously.
- The API will connect to local host instance for MSSQL with the sensitive words database.

### Questions
# What would you do to enhance performance of your project?
- Pagination should be added to the words that are loaded and checked with Regex
- Consider caching words as the list is small now but could increase substantially in the future.
- Potentially add rate limiting

# What additional enhancements would add to the project to make it more complete?
- Authentication and security. At the moment the admin section is open for anyone to use.
- Improved useability on the user initerface. Since this is a simple application - so is the UI.
- Audit logs of who might have changed or edited words
- Bulk operations of word management
- Dockerize the application
- Unit and behavioural tests

# Production deployment walkthrough
- API and Front end to be containerized.
- Push images to container registries to kick off deployments (Kubernetes etc)

