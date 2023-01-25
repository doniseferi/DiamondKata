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
<img src="https://raw.githubusercontent.com/doniseferi/DiamondKata/master/assets/Build.png?token=GHSAT0AAAAAAB5V2VGVHVQZRIUJELZLZ77EY6RSFSQ" />

___

## Running the application:
 
`dotnet run --project .\src\DiamondKata.Console\DiamondKata.Console.csproj u` or via the executable `.\src\DiamondKata.Console\bin\Debug\net6.0\DiamondKata.Console.exe u`

_Example:_
<img src="https://raw.githubusercontent.com/doniseferi/DiamondKata/master/assets/Run.png?token=GHSAT0AAAAAAB5V2VGU5QFFUGIRSP3OD2H4Y6RSGQQ" />

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

<img src="https://raw.githubusercontent.com/doniseferi/DiamondKata/master/assets/EndToEndTests.png?token=GHSAT0AAAAAAB5V2VGV2DFV3ELD6PM4JLRWY6RSHPA" />


`dotnet test .\test\DiamondKata.Domain.UnitTests\DiamondKata.Domain.UnitTests.csproj`

_Example:_

<img src="https://raw.githubusercontent.com/doniseferi/DiamondKata/master/assets/UnitTests.png?token=GHSAT0AAAAAAB5V2VGVZ57LDRN3KSJ4EQ7YY6RSIBQ" />
