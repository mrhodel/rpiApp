# rpiApp

A cross-platform project template for Windows and Linux, using the MVVM design pattern for easier maintenance and testing.

## Table of Contents
- [Introduction](#introduction)
- [Features](#features)
- [Getting Started](#getting-started)
- [Publishing](#publishing)
- [Contributing](#contributing)
- [License](#license)

## Introduction
rpiApp is a project template designed to facilitate the development of cross-platform applications for both Windows and Linux. It leverages the Model-View-ViewModel (MVVM) design pattern to ensure that the codebase is maintainable and testable. 

## Features
- [Avalonia](https://avaloniaui.net/?utm_source=nuget&utm_medium=referral&utm_content=project_homepage_link) GUI Framework
- Cross-platform support for Windows and Linux
- [MVVM](https://github.com/CommunityToolkit/dotnet) design pattern
- [Semantic versioning](https://semver.org/spec/v2.0.0-rc.2.html) for version management
- Examples using the following NuGet Packages
	- [Serilog](https://github.com/serilog/serilog) logging
	- [IoT Device Bindings](https://github.com/dotnet/iot) for various IoT boards, chips, displays and PCBs including RPi.
	- [Property Grid](https://github.com/bodong1987/Avalonia.PropertyGrid) for easy editing of a class properties.
	- [MVVM Dialogs](https://github.com/mysteryx93/HanumanInstitute.MvvmDialogs)Library simplifying the concept of opening dialogs and message boxes from a view model when using MVVM
	- [LiveChart2](https://github.com/beto-rodriguez/LiveCharts2) Simple, flexible, interactive & powerful charts, maps and gauges for .Net
	- [Microsoft Extensions Dependency Injection](https://www.nuget.org/packages/Microsoft.Extensions.DependencyInjection/9.0.1#show-readme-container) Supports the dependency injection (DI) software design pattern which is a technique for achieving Inversion of Control (IoC) between classes and their dependencies.
	- [Avalonia Edit](https://github.com/AvaloniaUI/AvaloniaEdit) Text editor with syntax highlighting, code completion, and tons of other features.
## Getting Started

### Prerequisites
- .NET 9 SDK
- Visual Studio 2022 or any other compatible IDE

### Installation
Select ![Use This Template](Pics/UseThisTemplate.png) from GitHub (https://github.com/mrhodel/rpiApp.git) to create a new repository

## Publishing
To publish the application, you can use Visual Studio:

1. **Open the project in Visual Studio**.
2. **Right-click the project in Solution Explorer** and select **Publish**.
3. **Select a publish profile** ![Publish Files](Pics/PublishProfiles.png) (Use linux-arm64 for Raspberry Pi)
4. **Click Publish**.

The published files will be available in the specified folder. You can then distribute these files as needed.
Note: For debugging on Linux do not check "Produce Single File" found in File Publish Options.

## Debugging

### Remote Debugging on Linux

 1. Copy the published files to the target system. I recommend [WinSCP](//winscp.net/) to sync your local publish folder with the remote device.
 2. Run the application on the remote device.
 3. Select **Attach to Process...** from the Visual Studio Debug menu.
 4. Enter the Connection type and connection target, select the application from the list, and click connect.
-
	![Attach to Process](Pics/AttachToProcess.png)

### On Windows use Visual Studio as usual.

## Contributing
Contributions are welcome! Please fork the repository and submit a pull request with your changes. Ensure that your code adheres to the existing coding style and includes appropriate tests.

## License
This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for more details.



