# SimplyMock
Simple no-nonsense mocking and auto-mock generator

    var mockingContext = new MockingContext(new ServiceCollection()
        .AddSingleton<IFoo, Foo>()
        .AddSingleton<IBar, Bar>()
    );

    var foo = mockingContext.GetRequiredService<IFoo>();

    Assert.Equal("Hello world!", foo.DoSomething());

https://github.com/quantumagi/SimplyMock/blob/master/Moq.SimplyMock.Tests/MockingContextTests.cs#L49-L56
