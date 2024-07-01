# Out of Office
> This application is designed to help HR Managers and Project Managers efficiently manage employees, their leave requests, and project assignments. It offers features for tracking employee details, approving leave requests, and assigning projects to employees.

## Table of Contents
* [General Info](#general-information)
* [Technologies Used](#technologies-used)
* [Setup](#setup)
* [Project Status](#project-status)
* [Contact](#contact)

## General Information
- **User Registration and Login:** Utilizes ASP.NET Core Identity for authorization and authentication.
- **Employee management:** Easily view, add, and update employees and their information, including position, people partner, and project assignment.
- **Leave and approval requests:** Employees can submit leave requests, and the corresponding HR Manager or Project Manager can approve or reject these requests.
- **Projects** Project Managers can view, add, and update projects and assign employees to projects.
- **Roles management:** Administrator can assign roles to users. Different roles (Employee, HR Manager, and Project Manager) result in varying permissions within the app.

## Technologies Used
- .NET 8.0
- ASP.NET Core MVC
- Entity Framework Core
- ASP.NET Core Identity
- Microsoft SQL Server
- C#
- HTML, CSS, Bootstrap

## Setup

Steps to run the project locally:
- Download and install .NET 8.0 from https://dotnet.microsoft.com/en-us/download/dotnet/8.0.
- Download and install Visual Studio from https://visualstudio.microsoft.com/en-us/downloads/ (ensure ASP.NET and web development is selected).
- Clone the repository to the selected folder.
- Open the Out of Office solution in Visual Studio.
- Set OutOfOffice.MVC as startup project.
- Run the project (Ctrl + F5).

## Project Status
The assumed functionality is done. It is possible to improve the project in future.
Improvements:
- Administrator will have enhanced abilities to manage all data within the application.
- HR Managers and Project Managers will be viewable in the Employees table.
- The ability to delete employees, along with their corresponding leave requests and approval requests, could be implemented. 

## Contact
Created by Lukasz Welna. For inquiries, please reach out on [LinkedIn](https://www.linkedin.com/in/lukasz-welna) or [Website](https://lukasz-welna.profesjonalnyprogramista.pl/).
