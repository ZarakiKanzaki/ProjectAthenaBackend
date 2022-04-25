using AthenaBackend.Domain.Exceptions;
using AthenaBackend.Domain.WellKnownInstances;
using NUnit.Framework;
using Shouldly;

namespace AthenaBackend.DomainTest.WellKnownInstances
{
    public class ThemebookTypeTest
    {
        [SetUp]
        public void Setup() { }

        [Test]
        public void FindByName_ValidName_ShouldNotBeNull()
        {
            var themebookType = ThemebookTypes.FindThemebookByName("Mythos");
            themebookType.ShouldNotBeNull();
            themebookType.Name.ShouldBe("Mythos");
        }

        [Test]
        public void FindByName_InvalidName_ShouldBeNull()
        {
            var themebookType = ThemebookTypes.FindThemebookByName("Test");
            themebookType.ShouldBeNull();
        }

        [Test]
        public void FindByKey_ValidKey_ShouldNotBeNull()
        {
            ThemebookTypes.FindThemebookByKey(2).ShouldNotBeNull();
        }

        [Test]
        public void FindByKey_InvalidKey_ShouldBeNull()
        {
            ThemebookTypes.FindThemebookByKey(-1).ShouldBeNull();
        }

        [Test]
        public void GetByName_ValidName_ShouldNotBeNull()
        {
            ThemebookTypes.GetThemebookByName("Mythos").ShouldNotBeNull();
        }

        [Test]
        public void GetByName_InvalidName_ShouldThrowException()
        {
            Should.Throw<DomainException>(() => ThemebookTypes.GetThemebookByName("Test"));
        }

    }
}
