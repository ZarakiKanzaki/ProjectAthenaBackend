using AthenaBackend.Common.Converters;
using AthenaBackend.Common.DependecyInjection;
using AthenaBackend.Common.DomainDrivenDesign;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Shouldly;
using System.Reflection;
using System.Threading.Tasks;

namespace AthenaBackend.CommonTest
{
    public class IServiceCollectionExtensionsTest
    {
        Assembly currentAssembly;

        [SetUp]
        public void Setup()
        {
            currentAssembly = Assembly.GetExecutingAssembly();
        }

        [Test]
        public void AddServices()
        {

            var services = new ServiceCollection();
            services.AddServices(currentAssembly);

            var provider = services.BuildServiceProvider();

            provider.GetService<FakeService1>().ShouldNotBeNull();
            provider.GetService<FakeService2>().ShouldNotBeNull();
        }

        [Test]
        public void AddConverters()
        {

            var services = new ServiceCollection();
            services.AddConverters(currentAssembly);

            var provider = services.BuildServiceProvider();
            var converter = provider.GetService<IConverter<FakeService1, FakeService2>>();

            converter.ShouldNotBeNull();
        }

        [Test]
        public void AddReadRepositories()
        {

            var services = new ServiceCollection();
            services.AddReadRepositories(currentAssembly);

            var provider = services.BuildServiceProvider();
            var readRepository = provider.GetService<IReadRepository<FakeAggregate, int>>();

            readRepository.ShouldNotBeNull();
        }


        [Test]
        public void AddRepositories()
        {

            var services = new ServiceCollection();
            services.AddRepositories(currentAssembly, currentAssembly);

            var provider = services.BuildServiceProvider();

            var fakeRepository1 = provider.GetService<IFakeRepository1>();
            fakeRepository1.ShouldNotBeNull();
            (fakeRepository1 is FakeRepository1).ShouldBeTrue();

            var fakeRepository2 = provider.GetService<IFakeRepository2>();
            fakeRepository2.ShouldNotBeNull();
            (fakeRepository2 is FakeRepository2).ShouldBeTrue();
        }

        [Test]
        public void AddRepositoriesOfT()
        {

            var services = new ServiceCollection();
            services.AddRepositories(currentAssembly, currentAssembly);

            var provider = services.BuildServiceProvider();

            var fakeRepository1 = provider.GetService<IFakeRepositoryOfT1>();
            fakeRepository1.ShouldNotBeNull();
            (fakeRepository1 is FakeRepositoryOfT1).ShouldBeTrue();

            var fakeRepository2 = provider.GetService<IFakeRepositoryOfT2>();
            fakeRepository2.ShouldNotBeNull();
            (fakeRepository2 is FakeRepositoryOfT2).ShouldBeTrue();
        }


    }

    #region metadata to test

    //Services
    class FakeService1 : IService { }
    class FakeService2 : IService { }

    //Converters
    class FakeService1ToFakeService2Converter : BaseConverterWithValidation<FakeService1, FakeService2, FakeService1ToFakeService2Converter>
    {
        protected override FakeService2 GetConvertedObject(FakeService1 objectToConvert)
        {
            throw new System.NotImplementedException();
        }
    }

    //Repositories
    interface IFakeRepository1 : IRepository { }
    class FakeRepository1 : IFakeRepository1 { }
    interface IFakeRepository2 : IRepository { }
    class FakeRepository2 : IFakeRepository2 { }
    class FakeReadRepositoryOfT1 : IReadRepository<FakeAggregate, int> 
    {
        public Task<FakeAggregate> FindByCode(string code)
        {
            throw new System.NotImplementedException();
        }

        public Task<FakeAggregate> FindByKey(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<FakeAggregate> GetByCode(string code)
        {
            throw new System.NotImplementedException();
        }

        public Task<FakeAggregate> GetByKey(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> IsUniqueByCode(string code)
        {
            throw new System.NotImplementedException();
        }
    }

    interface IFakeRepositoryOfT1 : IRepository<FakeAggregate, int> { }
    class FakeRepositoryOfT1 : IFakeRepositoryOfT1
    {
        public Task Add(FakeAggregate entity) => Task.CompletedTask;

        public Task<FakeAggregate> FindByCode(string code)
        {
            throw new System.NotImplementedException();
        }

        public Task<FakeAggregate> FindByKey(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<FakeAggregate> GetByCode(string code)
        {
            throw new System.NotImplementedException();
        }

        public Task<FakeAggregate> GetByKey(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> IsUniqueByCode(string code)
        {
            throw new System.NotImplementedException();
        }

        public Task SaveChanges() => Task.CompletedTask;
    }
    interface IFakeRepositoryOfT2 : IRepository<FakeAggregate, int> { }
    class FakeRepositoryOfT2 : IFakeRepositoryOfT2
    {
        public Task Add(FakeAggregate entity) => Task.CompletedTask;

        public Task<FakeAggregate> FindByCode(string code)
        {
            throw new System.NotImplementedException();
        }

        public Task<FakeAggregate> FindByKey(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<FakeAggregate> GetByCode(string code)
        {
            throw new System.NotImplementedException();
        }

        public Task<FakeAggregate> GetByKey(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> IsUniqueByCode(string code)
        {
            throw new System.NotImplementedException();
        }

        public Task SaveChanges() => Task.CompletedTask;
    }
    class FakeAggregate : Aggregate<int> { }
    #endregion
}
