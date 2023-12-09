# BulkyBookWeb2

## Getting started 
An application that helps you manage your bookshelf easier.

Main features:
- Log in using Google authentication and personalize your books list.
- Add your favorite books and theirs information (author, type, status,...).
- You can update these information later on.
- Filter your books by year.
- Search your book easily wiht book's name or book's author.

## Project structure
  `BulkyBookWeb2`
  
    ├─ `BulkyBookWeb2`
    
    │   ├── `Controllers` : All APIs implementation and navigation could be found here.
    
    │   ├── `Data` : All code related to infrastructure like context configuration.
    
    │   ├── `Migrations` : All database migrations could be found here.
    
    │   ├── `Models` : All business models and customized enums could be found here.
    
    │   ├── `Views` : All views corresponding to each controller and common shared views throughout all pages.
    
    │   ├── `Program.cs` : Entry point file that configures the app.
    
    ├─ `.gitattributes` :  Defines which language to use when syntax highlighting files and diffs.
    
    ├─ `.gitignore` : All files untracked by git.
    
    └─ `README.md` : Project documentation. Please read it before running the project.

## Configure Google authentication
1. Every users will need a Google account in order to log in and interact with this app. This helps personalize each user's bookshelf.
2. To enable Google sign in screen for users, you will need a __Google OAuth 2.0__ Client ID and Client secret configured using your __Google Console - API & Services__.
3. Follow this step by step [instructions][google-signin-instructions] to setup a Client ID and secret, then store them in the project using __dotnet user-secrets__.
4. As you can see in __Program.cs__ file, these 2 values will be accessed like this:
```C#
    if (builder.Environment.IsDevelopment())
        {
            options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
            options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
        }
```
5. Now, whenever a user logs in, a Google consent screen will display.

## Project compiltation 
1. Installation of [dotnet core 6.0][dotnet-core]
3. Open the terminal in the `BulkyBookWeb2` directory.
4. Restore nuget package `dotnet restore`
5. Build the project `dotnet build`

## Local Development
1. Make sure you have installed [dotnet core 6.0][dotnet-core]
2. Open terminal in the `BulkyBookWeb2` directory
3. Run this command to start the application
```bash
dotnet run --project BulkyBookWeb2
```
4. Configure: Hot reload on save
5. Database used: localdb (see in View > SQL Server Object Explorer)

[dotnet-core]: https://dotnet.microsoft.com/en-us/download
[google-signin-instructions]: https://learn.microsoft.com/en-us/aspnet/core/security/authentication/social/google-logins?view=aspnetcore-6.0
