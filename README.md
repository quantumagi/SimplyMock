## MockingContext Usage Guide

### Introduction

The `MockingContext` class in the `MoqExt` namespace serves as an `IServiceProvider` implementation for .NET. It's designed to instantiate services on-demand and also provide mocked instances for services that can't be concretized.

### Installation

First, install the NuGet package:

```bash
dotnet add package MoqExt
```

### Basic Usage

Initialize `MockingContext` with or without an existing `IServiceCollection`.

```csharp
// Without IServiceCollection
var mockingContext = new MoqExt.MockingContext();

// With IServiceCollection
IServiceCollection services = new ServiceCollection();
var mockingContext = new MoqExt.MockingContext(services);
```

#### Retrieve Services

Use `GetService` to retrieve an instance of the required service type.

```csharp
var myService = (MyService)mockingContext.GetService(typeof(MyService));
```

If `MyService` is not registered, `MockingContext` will return a mocked instance.

#### Retrieve Mocks

To retrieve a mock for setup, pass the mock type.

```csharp
var myServiceMock = (Mock<MyService>)mockingContext.GetService(typeof(Mock<MyService>));
```

### Advanced Features

#### Handle Multiple Instances

If you've registered multiple services of the same type, `MockingContext` will throw an `InvalidOperationException`.

#### Enumerable Services

Pass an enumerable type to get multiple instances of a service.

```csharp
var myServices = (List<MyService>)mockingContext.GetService(typeof(IEnumerable<MyService>));
```

### Notes

- Value type constructor arguments in the service classes will use their default values.
- Ambiguity in the number of constructors for a service class will result in an `InvalidOperationException`.

