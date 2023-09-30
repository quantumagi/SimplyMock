using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Moq;

namespace MoqExt.Tests
{
    public interface IFoo
    {
        string DoSomething();
    }

    public interface IBar
    {
        string DoSomethingElse();
    }

    public class Foo : IFoo
    {
        private readonly IBar bar;

        public Foo(IBar bar)
        {
            this.bar = bar;
        }

        public string DoSomething()
        {
            return MockThis();
        }

        public virtual string MockThis()
        {
            return this.bar.DoSomethingElse();
        }
    }

    public class Bar : IBar
    {
        public string DoSomethingElse()
        {
            return "Hello world!";
        }
    }

    public class MockingContextTests
    {
        [Fact]
        public void CanDependencyInjectMock()
        {
            var mockingContext = new MockingContext(new ServiceCollection()
                .AddSingleton<IFoo, Foo>()
                .AddSingleton<IBar, Bar>()
            );

            var foo = mockingContext.GetRequiredService<IFoo>();

            Assert.Equal("Hello world!", foo.DoSomething());
        }

        [Fact]
        public void CanMockInterface()
        {
            var mockingContext = new MockingContext();

            var fooMock = mockingContext.GetRequiredService<Mock<IFoo>>();
            fooMock.Setup(x => x.DoSomething()).Returns("Hello world!");

            Assert.Equal("Hello world!", fooMock.Object.DoSomething());
        }

        [Fact]
        public void CanMockClass()
        {
            var mockingContext = new MockingContext();

            var barMock = mockingContext.GetRequiredService<Mock<IBar>>();
            barMock.Setup(x => x.DoSomethingElse()).Returns("This");

            var fooMock = mockingContext.GetRequiredService<Mock<Foo>>();
            fooMock.Setup(x => x.MockThis()).CallBase();

            Assert.Equal("This", fooMock.Object.DoSomething());
        }
    }
}
