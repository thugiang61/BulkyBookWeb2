# BulkyBookWeb2

## Getting started 
An application that helps you manage your bookshelf easier.

Main features:
- Add you favorite books and theirs information (author, type, status,...).
- You can update these information later on.
- Filter your books by year.
- Search your book easily.

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

## Project compiltation 
1. Installation of [dotnet core 6.0][dotnet-core]
2. Open the terminal in the `BulkyBookWeb2` directory.
3. Restore nuget package `dotnet restore`
4. Build the project `dotnet build`

## Local Development
1. Make sure you have installed [dotnet core 6.0][dotnet-core]
2. Open terminal in the `BulkyBookWeb2` directory
3. Run this command to start the application
```bash
dotnet run --project BulkyBookWeb2
```
4. Hot reload on save settings
5. Database used: localdb 

[dotnet-core]: https://dotnet.microsoft.com/en-us/download
