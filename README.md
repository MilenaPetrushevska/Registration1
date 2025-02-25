Project Overview: 

This project is a simple CRUD application that allows users to manage User, Project, and Task entities.
The application includes user registration, login, and secure token-based authentication for protecting API endpoints. 
The backend is built using .NET MVC, and it implements the necessary operations for creating, reading, updating, and deleting data related to users, projects, and tasks.

How to Run the Project Locally:

To set up the project locally, I used Visual Studio for development and pgAdmin for managing the PostgreSQL database.

Step 1: Set Up the Database:

Create a new database in PostgreSQL using pgAdmin.
Import the provided .sql file into your database. This file will create the necessary tables and data structures.
Update the connection string in the appsettings.json file to reflect your local PostgreSQL database configuration.

Step 2: Running the Application:

Once the database is set up and the connection string is updated, run the application in Visual Studio. The application will start, and you can access it through your web browser.

The first page that appears is the Login page. If you are a new user, you can register by entering your name, email, and password.
If you already have an account, simply log in using your email and password.
After successful login, you will be redirected to the Your Projects page, where you can see all the projects associated with your account.

Step 3: CRUD Operations for Projects

You can create a new project by providing a name and description.
You can edit or delete an existing project, but these actions are only available for projects that belong to the logged-in user.

Step 4: CRUD Operations for Tasks

Clicking on a project name will open a new page where you can view all the tasks associated with that project.
You can create new tasks, edit, or delete existing tasks, but these operations are also restricted to the authenticated user’s own projects.

User Authentication and Session Management:

When a user logs in, a session is initiated. The user remains logged in until they log out.
Upon log out, the session ends, and the user will be redirected to the login page.

API Endpoints and JWT Authentication:

Since the application is built using the MVC architecture, I created separate API controllers for handling the API endpoints.For authentication, I generate a JWT token, which is used to verify the identity of the user and secure access to protected resources.

These are the main functionalities secured by JWT authentication:

Project-related operations (Create, Read, Update, Delete) are only allowed for authenticated users who are associated with the projects.

Task-related operations (Create, Read, Update, Delete) are restricted to authenticated users who own the tasks within their own projects.

I tested these API endpoints using Swagger, where a successful login generates the authentication token. This token is required to perform any operations on projects and tasks.
