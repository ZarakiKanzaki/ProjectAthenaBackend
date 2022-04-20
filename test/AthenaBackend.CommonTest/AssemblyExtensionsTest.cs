using AthenaBackend.Common.DependecyInjection;
using NUnit.Framework;
using Shouldly;
using System.Linq;
using System.Reflection;

namespace AthenaBackend.CommonTest
{
    public class AssemblyExtensionsTest
    {
        Assembly currentAssembly;
        [SetUp]
        public void Setup()
        {
            currentAssembly = Assembly.GetExecutingAssembly();
        }

        [Test]
        public void GetImplementationOf_OneImplementation()
        {
            var types = currentAssembly.GetImplementationsOf<IFakeInterface2>();

            types.Any(t => t == typeof(FakeClass2_1)).ShouldBeTrue();
            types.Any(t => t == typeof(FakeClass1_1)).ShouldBeFalse();
            types.Any(t => t == typeof(FakeClass1_2)).ShouldBeFalse();
        }

        [Test]
        public void GetImplementationOf_MoreThanOneImplementation()
        {
            var types = currentAssembly.GetImplementationsOf<IFakeInterface1>();

            types.Any(t => t == typeof(FakeClass1_1)).ShouldBeTrue();
            types.Any(t => t == typeof(FakeClass1_2)).ShouldBeTrue();
            types.Any(t => t == typeof(FakeClass2_1)).ShouldBeFalse();
        }

        [Test]
        public void GetImplementationOf_NoImplementation()
        {
            var types = currentAssembly.GetImplementationsOf<IFakeInterface3>();

            types.Any().ShouldBeFalse();
        }

        [Test]
        public void GetInterfacesThatInheriteFrom_OneImplementation()
        {
            var types = currentAssembly.GetInterfacesInheritedFrom<IFakeInterface2>();

            types.Any(t => t == typeof(IFakeInterfaceOfInterface2_1)).ShouldBeTrue();
            types.Any(t => t == typeof(IFakeInterfaceOfInterface1_1)).ShouldBeFalse();
            types.Any(t => t == typeof(IFakeInterfaceOfInterface1_2)).ShouldBeFalse();
        }

        [Test]
        public void GetInterfacesThatInheriteFrom_MoreThanOneImplementation()
        {
            var types = currentAssembly.GetInterfacesInheritedFrom<IFakeInterface1>();

            types.Any(t => t == typeof(IFakeInterfaceOfInterface1_1)).ShouldBeTrue();
            types.Any(t => t == typeof(IFakeInterfaceOfInterface1_2)).ShouldBeTrue();
            types.Any(t => t == typeof(IFakeInterfaceOfInterface2_1)).ShouldBeFalse();
        }

        [Test]
        public void GetInterfacesThatInheriteFrom_NoImplementation()
        {
            var types = currentAssembly.GetInterfacesInheritedFrom<IFakeInterface3>();

            types.Any().ShouldBeFalse();
        }
    }

    #region metadata to test

    interface IFakeInterface1 { }
    class FakeClass1_1 : IFakeInterface1 { }
    class FakeClass1_2 : IFakeInterface1 { }
    interface IFakeInterfaceOfInterface1_1 : IFakeInterface1 { }
    interface IFakeInterfaceOfInterface1_2 : IFakeInterface1 { }

    interface IFakeInterface2 { }
    class FakeClass2_1 : IFakeInterface2 { }
    interface IFakeInterfaceOfInterface2_1 : IFakeInterface2 { }

    interface IFakeInterface3 { }

    #endregion
}
