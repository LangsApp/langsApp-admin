# LangApp-Admin-App
Admin App for a language learning application.

## Technologies
- WPF
- .NET 8
- MVVM

## Architecture
The project follows MVVM pattern:
- Models — applications data
- View — Pages and Windows and UI components
- ViewModel — presentation logic and communication between Views and services
- Services - communication with the LangApp API

##  How to run locally

### Requirements
- Windows
- .NET 8 SDK
- Running LangApp API

### Steps
1. Clone repository
2. Open `LangApp.Admin.sln`
3. Configure the LangApp API address.
4. Build and run the application.

## Tests
Tests are not implemented yet.

## CI/CD
GitHub Actions builds the application on pushes and pull requests to `main` and `dev`.

## Project status
In active development