using AthenaBackend.Domain.Exceptions;
using NUnit.Framework;
using Shouldly;
using System.Linq;
namespace AthenaBackend.DomainTest.Exceptions
{
    public class DomainExceptionTest
    {
        [SetUp]
        public void Setup() { }

        [Test]
        public void Constructor_Base()
        {
            Should.Throw<DomainException>(() => throw new DomainException());
        }

        [Test]
        public void Constructor_WithMessageCodeParameters_PropertiesInitialized()
        {
            var exception = new DomainException($"MESSAGE", "CODE {0}", new[] { $"Parameters" });
            Should.Throw<DomainException>(() => throw exception);
            exception.ExceptionPaths.ShouldNotBeNull();
            exception.Parameters.ShouldNotBeNull();
            exception.Parameters.Any().ShouldBeTrue();
            exception.Code.ShouldNotBeNull();
        }

        [Test]
        public void Constructor_WithCodeParameters_PropertiesInitialized()
        {
            var exception = new DomainException("CODE {0}", new[] { $"Parameters" });
            Should.Throw<DomainException>(() => throw exception);
            exception.ExceptionPaths.ShouldNotBeNull();
            exception.Parameters.ShouldNotBeNull();
            exception.Parameters.Any().ShouldBeTrue();
        }

        [Test]
        public void Constructor_WithInnerExceptionCodeParameters_PropertiesInitialized()
        {
            var exception = new DomainException(new DomainException($"TEST"), "CODE {0}", new[] { $"Parameters" });
            Should.Throw<DomainException>(() => throw exception);
            exception.ExceptionPaths.ShouldNotBeNull();
            exception.Parameters.ShouldNotBeNull();
            exception.Parameters.Any().ShouldBeTrue();
        }

        [Test]
        public void Constructor_WithInnerExceptionAndInnerInnerExceptionCodeParameters_PropertiesInitialized()
        {
            var exception = new DomainException(new DomainException($"TEST", new DomainException()), "CODE {0}", new[] { $"Parameters" });
            Should.Throw<DomainException>(() => throw exception);
            exception.ExceptionPaths.ShouldNotBeNull();
            exception.Parameters.ShouldNotBeNull();
            exception.Parameters.Any().ShouldBeTrue();
        }

        [Test]
        public void ConstructorNullReference_WithMessageCodeParameters()
        {
            var exception = new NullReferenceDomainException($"MESSAGE");
            Should.Throw<DomainException>(() => throw exception);
            Should.Throw<NullReferenceDomainException>(() => throw exception);
        }

        [Test]
        public void ConstructorNullOrWhiteSpace_WithMessageCodeParameters()
        {
            var exception = new NullOrWhiteSpaceDomainException($"MESSAGE");
            Should.Throw<DomainException>(() => throw exception);
            Should.Throw<NullOrWhiteSpaceDomainException>(() => throw exception);
        }


        [Test]
        public void ConstructorCodeAlreadyExists_WithMessageCodeParameters()
        {
            var exception = new CodeAlreadyExistsDomainException($"MESSAGE", $"TEST");
            Should.Throw<DomainException>(() => throw exception);
            Should.Throw<CodeAlreadyExistsDomainException>(() => throw exception);
        }

        [Test]
        public void ConstructorCannotFindEntity_WithMessageCodeParameters()
        {
            var exception = new CannotFindEntityDomainException($"MESSAGE", $"TEST", $"TEST");
            Should.Throw<DomainException>(() => throw exception);
            Should.Throw<CannotFindEntityDomainException>(() => throw exception);
        }

    }
}
