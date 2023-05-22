# Ferreira Challenge Back-end
This is a sample C#.NET API project that demonstrates the usage of Entity Framework Core (EF Core) for data access.

# Prerequisites

### .NET SDK installed

### Database oracle setup and connection string configured

#### You can set the database connection string into the appsettings.json file inside the main project

# Getting Started

1 - Clone the repository: git clone https://github.com/your-username/your-repo.git

2 - Navigate to the project directory: cd project-name

3 - Update the database connection string in appsettings.json with your database details.

4 - Run the database migrations: dotnet ef database update

5 - Build the project: dotnet build

6 - Run the project: dotnet run

7 - The API will be available at https://localhost:7059

# API Endpoints

GET     /api/user/{id} - Get a user by ID

GET     /api/user - Get every user based into the filter

POST    /api/user - Create User

PUT     /api/user/{id} - Edit User informations

PUT     /api/user/{id}/Status - Edit Status

DELETE  /api/user/{id} - Delete by ID

DELETE  /api/user/DeleteAll - Delete Every user

POST    /api/auth/Login - To login

POST    /api/auth/Password/Recover - To reset the password

##### For more informations run the project and check the swagger https://localhost:7059/swagger/


# Dependencies

ASP.NET Core
Entity Framework Core
AutoMapper
BCrypt

# Contributing
Contributions are welcome! If you find any issues or want to add new features, please feel free to submit a pull request.

# License
This project is licensed under the MIT License.