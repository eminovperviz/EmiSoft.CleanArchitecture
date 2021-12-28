# Clean Architecture

A starting point for Clean Architecture with ASP.NET Core. [Clean Architecture](https://8thlight.com/blog/uncle-bob/2012/08/13/the-clean-architecture.html) is just the latest in a series of names for the same loosely-coupled, dependency-inverted architecture. You will also find it named [hexagonal](http://alistair.cockburn.us/Hexagonal+architecture), [ports-and-adapters](http://www.dossier-andreas.net/software_architecture/ports_and_adapters.html), or [onion architecture](http://jeffreypalermo.com/blog/the-onion-architecture-part-1/).

## Table Of Contents
  
- [Design Decisions and Dependencies](#design-decisions-and-dependencies)
  * [The Application Project](#the-application-project)
  * [The Domain Project](#the-domain-project)
  * [The SharedKernel Project](#the-sharedkernel-project)
  * [The Infrastructure Project](#the-infrastructure-project)
  * [The Web Project](#the-web-project)  

## Give a Star! :star:
If you like or are using this project to learn or start your solution, please give it a star. Thanks!
 
## Versions

The master branch is now using .NET 6. If you need a previous version use one of these tagged commits:

# Design Decisions and Dependencies

The goal of this sample is to provide a fairly bare-bones starter kit for new projects. It does not include every possible framework, tool, or feature that a particular enterprise application might benefit from. Its choices of technology for things like data access are rooted in what is the most common, accessible technology for most business software developers using Microsoft's technology stack. It doesn't (currently) include extensive support for things like logging, monitoring, or analytics, though these can all be added easily. Below is a list of the technology dependencies it includes, and why they were chosen. Most of these can easily be swapped out for your technology of choice, since the nature of this architecture is to support modularity and encapsulation.

## The Core Layer

The Core layer is the center of the Clean Architecture design, and all other project dependencies should point toward it. As such, it has very few external dependencies. 
The Core layer contains The Application Project and The Domain Project. The Core layer should include things like:

- Entities 
- DTOs
- Interfaces  
- Services

## The SharedKernel Project

Many solutions will also reference a separate **Shared Kernel** project/package. I recommend creating a separate SharedKernel project and solution if you will require sharing code between multiple [bounded contexts](https://ardalis.com/encapsulation-boundaries-large-and-small/) (see [DDD Fundamentals](https://www.pluralsight.com/courses/domain-driven-design-fundamentals)). I further recommend this be published as a NuGet package (most likely privately within your organization) and referenced as a NuGet dependency by those projects that require it. For this sample, in the interest of simplicity, I've added a SharedKernel project to the solution. It contains types that would likely be shared between multiple bounded contexts (VS solutions, typically), in my experience. 

## The Infrastructure Project

Most of your application's dependencies on external resources should be implemented in classes defined in the Infrastructure project. These classes should implement interfaces defined in Core. If you have a very large project with many dependencies, it may make sense to have multiple Infrastructure projects (e.g. Infrastructure.Data), but for most projects one Infrastructure project with folders works fine. The sample includes data access, but you would also add things like email providers, file access, web api clients, etc. to this project so they're not adding coupling to your Core or UI projects.

The Infrastructure project depends on `Microsoft.EntityFrameworkCore.SqlServer`. The former is used because it's built into the default ASP.NET Core templates and is the least common denominator of data access. If desired, it can easily be replaced with a lighter-weight ORM like Dapper. In this case, ConfigureServices class can be used in the Infrastructure class to allow wireup of dependencies there, without the entry point of the application even having to have a reference to the project or its types.

## The Web Project

The entry point of the application is the ASP.NET Core Web API project. This is actually a console application, with  `Program.cs` Minimal API implementation. It currently uses the default  ASP.NET Core API project template code. This includes its configuration system, which uses the default `appsettings.json` file plus environment variables and configured services. The project delegates to the `Infrastructure` project.
