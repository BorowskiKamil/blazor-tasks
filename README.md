# Blazor To-Do App

Hey! This repository contains an API written in ASP.NET Core 2.1 and front-end web-application written in experimental .NET web framework Blazor that runs via WebAssembly 🤓

Implemented features:
- Creating and editing tasks
- Managing categories and assigning tasks to them

## Getting Started with Blazor

To get setup with Blazor:

1. Install the [.NET Core 2.1 SDK](https://go.microsoft.com/fwlink/?linkid=873092) (2.1.300 or later).
2. Install [Visual Studio 2017 (15.7)](https://go.microsoft.com/fwlink/?linkid=873093) with the *ASP.NET and web development* workload.
3. Install the latest [Blazor Language Services extension](https://go.microsoft.com/fwlink/?linkid=870389) from the Visual Studio Marketplace.

## Setup Database Connections

When running on Windows, the server is using in memory database on default configuration. You can change that in the file appsettings.json. 

>Note: .NET Core doesn't allow to use in memory database on other OS than Windows, so if you're not Windows user you have to configure database connection string in appsettings.json and use MySQL.

## Screenshots