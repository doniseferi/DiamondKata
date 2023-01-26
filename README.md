# Diamond Kata Console Application

A console application that when given an english character will print a diamond using the english character as the diamond midpoint. This is an implementation of [the diamond kata](https://github.com/davidwhitney/CodeDojos/tree/master/Diamond%20Kata).

___

## Prerequisites
.NET 6+ SDK
___

## Building the solution:

To build the solution via the .NET CLI run:

`dotnet build .\DiamondKata.sln`

_Example:_
<img src="./assets/Build.png?sanitize=true" />

___

## Running the application:
 
`dotnet run --project .\src\DiamondKata.Console\DiamondKata.Console.csproj u` or via the executable `.\src\DiamondKata.Console\bin\Debug\net6.0\DiamondKata.Console.exe u`

_Example:_
<img src="./assets/Run.png?sanitize=true" />

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

## Running the tests

Via the .NET cli: 
`dotnet test .\test\DiamondKata.EndToEndTests\DiamondKata.EndToEndTests.csproj`

_Example:_

<img src="./assets/EndToEndTests.png?sanitize=true" />


`dotnet test .\test\DiamondKata.Domain.UnitTests\DiamondKata.Domain.UnitTests.csproj`

_Example:_

<img src="./assets/UnitTests.png?sanitize=true" />
