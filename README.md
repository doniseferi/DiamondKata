# DiamondKata Project

## Overview
A .NET 8 minimal API and a console application that, when given an English character, will print a diamond starting with 'A' at the top and the specified character at its widest point. The width of the diamond at its widest point follows the "2n-1" rule, where 'n' is the position of the input character in the English alphabet. This project follows the YAGNI principle to avoid unnecessary complexity. It's an implementation of [the diamond kata](https://github.com/davidwhitney/CodeDojos/tree/master/Diamond%20Kata).

## Key Features
- **API Layer**: A streamlined gateway for HTTP requests, facilitating diamond pattern generation.
- **Console Application**: An intuitive, CLI-based interface for interacting with the core functionality.
- **Domain Services**: The heart of the application, executing the intricate logic of diamond pattern creation.
- **Comprehensive Testing Suite**: A robust set of tests (unit, integration, end-to-end) to ensure application integrity and performance.

## Modules Description

### 1. DiamondKata.Api
- **Purpose**: Acts as the interface for HTTP-based interaction with the diamond generation logic.
- **Key Components**: Includes configuration, endpoint, and service registrations.

### 2. DiamondKata.Console
- **Purpose**: Provides a direct, interactive CLI experience for the application.
- **Usage**: Handles command-line argument parsing and application configurations.

### 3. DiamondKata.DomainService
- **Purpose**: Central to implementing the algorithms for diamond pattern creation.
- **Highlights**: Features query handlers, services, and algorithmic logic.

### 4. DiamondKata.Domain.UnitTests
- **Objective**: Tests the domain logic for accuracy and robustness.

### 5. DiamondKata.Api.IntegrationTests
- **Focus**: Assesses the API's integration with the core logic, ensuring cohesive functionality.

### 6. DiamondKata.Console.EndToEndTests
- **Scope**: Emulates user scenarios to validate the complete application flow.

## Getting Started

### Prerequisites
- .NET 8.0 SDK.
- An IDE like Visual Studio or VSCode.

### Installation and Setup
1. **Clone the Repository**: `git clone https://github.com/doniseferi/diamondkata`.
2. **Navigate to the Project Directory**: `cd .\DiamondKata\`.

## Running the Application

### Using Command Line
- **API**: Run `dotnet run --project .\src\DiamondKata.Api\DiamondKata.Api.csproj` from the root of the repository or `dotnet run` in the `DiamondKata.Api` directory.
- **Console**: Run `dotnet run --project .\src\DiamondKata.Console\DiamondKata.Console.csproj ENGLISH_CHAR` from the root of the repository or `dotnet run ENGLISH_CHAR` in the `DiamondKata.Console` directory.
    - Example: `dotnet run --project .\src\DiamondKata.Console\DiamondKata.Console.csproj E`
Example output:
>           A
>          B-B
>         C---C
>        D-----D
>       E-------E
>        D-----D
>         C---C
>          B-B
>           A
### Using Visual Studio or VSCode
- **Visual Studio**:
  1. Open the `DiamondKata.sln` solution file in Visual Studio.
  2. Set the desired project (API or Console) as the startup project.
  3. Press F5 to run the application.
- **VSCode**:
  1. Open the repository folder in VSCode.
  2. Open the terminal in VSCode.
  3. Use the `dotnet run` command with the appropriate project path and arguments as described above.

### Testing
- Run `dotnet test .\DiamondKata.sln` in the root directory of the repo.
