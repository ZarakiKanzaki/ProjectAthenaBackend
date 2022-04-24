using AthenaBackend.Domain.Core.Themebooks;
using AthenaBackend.Domain.Core.Themebooks.Dtos;
using AthenaBackend.Domain.Exceptions;
using AthenaBackend.DomainTest.Core.Themebooks.Builders;
using AthenaBackend.DomainTest.Core.Themebooks.Defaults;
using AthenaBackend.Infrastructure;
using AthenaBackend.Infrastructure.WriteModel.Core.Themebooks;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using NUnit.Framework;
using Shouldly;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace AthenaBackend.InfrastructureTest.WriteModel.Core.Themebooks
{
    public class ThemebookRepositoryTest
    {
        private TagQuestionDtoBuilder tagQuestionDtoBuilder;
        private ThemebookConceptDtoBuilder themebookConceptDtoBuilder;
        private ThemebookDtoBuilder themebookDtoBuilder;
        private ThemebookImprovementDtoBuilder themebookImprovementDtoBuilder;
        private Mock<IThemebookRepository> themebookRepositoryMock;
        private ThemebookService themebookServiceMock;
        private Mock<WriteDbContext> context;
        private ThemebookRepository themebookRepository;

        [SetUp]
        public async Task Setup()
        {
            InitializeBuilders();
            InitializeService();
            await InitializeDbContext();

            themebookRepository = new ThemebookRepository(context.Object);
        }

        private async Task InitializeDbContext()
        {
            var options = new DbContextOptionsBuilder<WriteDbContext>().Options;
            context = new Mock<WriteDbContext>(options);

            var themebooks = new List<Themebook>()
            {
                await themebookServiceMock.Create(GetValidRandomDto(1)),
                await themebookServiceMock.Create(GetValidRandomDto(2)),
                await themebookServiceMock.Create(GetValidRandomDto(3)),
            };

            themebooks.ForEach(themebook => themebook.SetIdForUniTesting());

            context.Setup(a => a.Themebooks).ReturnsDbSet(themebooks);
            context.Setup(a => a.Set<Themebook>()).ReturnsDbSet(themebooks);
        }

        [Test]
        public async Task FindByCode_ValidCode_ShouldNotBeNull() 
        {
            var validThemebook = await themebookRepository.FindByCode($"{ThemebookDtoDefaultValues.name}-{1}");
            
            validThemebook.ShouldNotBeNull();
        }

        [Test]
        public async Task FindByCode_InvalidCode_ShouldBeNull()
        {
            var validThemebook = await themebookRepository.FindByCode($"TEST");

            validThemebook.ShouldBeNull();
        }

        [Test]
        public async Task GetByCode_ValidCode_ShouldNotBeNull()
        {
            var validThemebook = await themebookRepository.GetByCode($"{ThemebookDtoDefaultValues.name}-{1}");

            validThemebook.ShouldNotBeNull();
        }

        [Test]
        public async Task GetByCode_InvalidCode_ThrowsDomainException()
        {
            await themebookRepository.GetByCode($"TEST").ShouldThrowAsync<DomainException>();
        }

        [Test]
        public async Task IsUniqueByCode_ValidCode_ShouldBeTrue()
        {
            (await themebookRepository.IsUniqueByCode($"TEST")).ShouldBeTrue();
        }
        
        [Test]
        public async Task IsUniqueByCode_ValidCode_ShouldBeFalse()
        {
            (await themebookRepository.IsUniqueByCode($"{ThemebookDtoDefaultValues.name}-{1}")).ShouldBeFalse();
        }


        [Test]
        public async Task FindByKey_InvalidCode_ShouldBeNull()
        {
            var validThemebook = await themebookRepository.FindByKey(Guid.NewGuid());

            validThemebook.ShouldBeNull();
        }

        [Test]
        public async Task GetByKey_InvalidCode_ShouldThrowDomainException()
        {
            await themebookRepository.GetByKey(Guid.NewGuid()).ShouldThrowAsync<DomainException>();
        }

        #region private utilities
        private ThemebookDto GetValidRandomDto(short index) 
            => GetValidDtoBuilder().WithName($"{ThemebookDtoDefaultValues.name}-{index}")
                                   .WithId(ThemebookDtoDefaultValues.id_updated)
                                   .Build();

        private void InitializeService()
        {
            themebookRepositoryMock = new Mock<IThemebookRepository>();
            themebookRepositoryMock.Setup(a => a.IsUniqueByCode(It.IsAny<string>())).ReturnsAsync(false);
            themebookServiceMock = new ThemebookService(themebookRepositoryMock.Object);
        }

        private void InitializeBuilders()
        {
            tagQuestionDtoBuilder = new TagQuestionDtoBuilder();
            themebookConceptDtoBuilder = new ThemebookConceptDtoBuilder();
            themebookDtoBuilder = new ThemebookDtoBuilder();
            themebookImprovementDtoBuilder = new ThemebookImprovementDtoBuilder();
        }


        private ThemebookDtoBuilder GetValidDtoBuilder()
            => GetValidDtoBuilderWithoutCollections()
                        .WithImprovements(GetValidImprovementList())
                        .WithTagQuestions(GetValidTagQuestionList());

        private ThemebookDtoBuilder GetValidDtoBuilderWithoutCollections()
            => themebookDtoBuilder.WithId(ThemebookDtoDefaultValues.id_create)
                                  .WithName(ThemebookDtoDefaultValues.name)
                                  .WithDescription(ThemebookDtoDefaultValues.description)
                                  .WithUserId(ThemebookDtoDefaultValues.userId)
                                  .WithTypeId(ThemebookDtoDefaultValues.typeId)
                                  .WithThemebookConcept(GetValidThemebookConceptDto());

        private List<ThemebookImprovementDto> GetValidImprovementList()
            => new List<ThemebookImprovementDto> { GetValidImprovement() };

        private ThemebookImprovementDto GetValidImprovement()
            => themebookImprovementDtoBuilder.WithTitle(ThemebookImprovementDtoDefaultValues.title)
                                             .WithDecription(ThemebookImprovementDtoDefaultValues.decription)
                                             .Build();

        private List<TagQuestionDto> GetValidTagQuestionList() => new List<TagQuestionDto> { GetValidTagQuestionDto() };

        private TagQuestionDto GetValidTagQuestionDto()
            => tagQuestionDtoBuilder.WithQuestion(TagQuestionDtoDefaultValues.question)
                                    .WithType(TagQuestionDtoDefaultValues.type)
                                    .Build();
        private ThemebookConceptDto GetValidThemebookConceptDto()
            => themebookConceptDtoBuilder.WithUserId(ThemebookConceptDtoDefaultValues.userId)
                                         .WithQuestion(ThemebookConceptDtoDefaultValues.question)
                                         .Build();
        #endregion
    }
}
