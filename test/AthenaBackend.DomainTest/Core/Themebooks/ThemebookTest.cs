using NUnit.Framework;
using AthenaBackend.DomainTest.Core.Themebooks.Builders;
using Shouldly;
using System;
using Moq;
using AthenaBackend.Domain.Core.Themebooks;
using System.Threading.Tasks;
using AthenaBackend.Domain.Core.Themebooks.Dtos;
using AthenaBackend.DomainTest.Core.Themebooks.Defaults;
using AthenaBackend.Domain.Exceptions;
using System.Collections.Generic;

namespace AthenaBackend.DomainTest.Core.Themebooks
{
    public class ThemebookTest
    {
        private TagQuestionDtoBuilder tagQuestionDtoBuilder;
        private ThemebookConceptDtoBuilder themebookConceptDtoBuilder;
        private ThemebookDtoBuilder themebookDtoBuilder;
        private ThemebookImprovementDtoBuilder themebookImprovementDtoBuilder;
        private Mock<IThemebookRepository> themebookRepositoryMock;
        private ThemebookService themebookServiceMock;

        [SetUp]
        public void Setup()
        {
            tagQuestionDtoBuilder = new TagQuestionDtoBuilder();
            themebookConceptDtoBuilder = new ThemebookConceptDtoBuilder();
            themebookDtoBuilder = new ThemebookDtoBuilder();
            themebookImprovementDtoBuilder = new ThemebookImprovementDtoBuilder();
            themebookRepositoryMock = new Mock<IThemebookRepository>();

            themebookServiceMock = new ThemebookService(themebookRepositoryMock.Object);
        }

        [Test]
        public void Create_ValidNameButNonUniqueDto_ThrowsDomainException()
        {
            var invalidDto = GetInvalidDtoWithValidName();

            themebookRepositoryMock.Setup(a => a.IsUniqueByCode(It.IsAny<string>())).ReturnsAsync(false);

            Should.ThrowAsync<DomainException>(async () => await themebookServiceMock.Create(invalidDto));
        }

        [Test]
        public void Create_InvalidDto_ThrowsAggregateException()
        {
            var invalidDto = GetInvalidDto();

            Should.ThrowAsync<AggregateException>(async () => await themebookServiceMock.Create(invalidDto));
        }




        [Test]
        public void Create_InvalidImprovement_ThrowsAggregateException()
        {
            var invalidImprovementDto = GetValidThemebookWithInvalidImprovementDto();

            Should.ThrowAsync<AggregateException>(async () => await themebookServiceMock.Create(invalidImprovementDto));
        }

        [Test]
        public void Create_InvalidTagQuestion_ThrowsDomainException()
        {
            var invalidTagQuestionDto = GetValidThemebookWithInvalidTagQuestionDto();

            Should.ThrowAsync<DomainException>(async () => await themebookServiceMock.Create(invalidTagQuestionDto));
        }

        [Test]
        public async Task Create_ValidDtoWithoutCollections_ExcecutedCorrectly()
        {
            var validDtoWithoutCollections = GetValidDtoWithoutCollections();

            var createdThemebook = await themebookServiceMock.Create(validDtoWithoutCollections);

            themebookRepositoryMock.Verify(t =>
                        t.Add(It.Is<Themebook>(themebook =>
                            IsThemebookTheSameCreated(themebook, createdThemebook)
                        )),
                        Times.Once);

        }

        [Test]
        public async Task Update_ValidNameButNonUniqueCodeDto_ThrowsDomainException()
        {
            var invalidDto = GetInvalidDtoWithValidName();
            var existentDto = GetExistantDto();

            var existentThemebook = await themebookServiceMock.Create(existentDto);

            themebookRepositoryMock.Setup(a => a.IsUniqueByCode(It.IsAny<string>())).ReturnsAsync(false);
            themebookRepositoryMock.Setup(a => a.GetByCode(It.IsAny<string>())).ReturnsAsync(existentThemebook);

            await themebookServiceMock.Update(invalidDto).ShouldThrowAsync<DomainException>();
        }

        [Test]
        public async Task Update_ValidNameButNonUniqueEntityDto_ThrowsDomainException()
        {
            var invalidDto = GetInvalidDtoWithValidName();
            var existentDto = GetExistantDto();

            var existentThemebook = await themebookServiceMock.Create(existentDto);

            themebookRepositoryMock.Setup(a => a.IsUniqueByCode(It.IsAny<string>())).ReturnsAsync(true);
            themebookRepositoryMock.Setup(a => a.GetByCode(It.IsAny<string>())).ReturnsAsync(existentThemebook);

            await themebookServiceMock.Update(invalidDto).ShouldThrowAsync<DomainException>();
        }


        [Test]
        public async Task Update_InvalidDto_ThrowsAggregateException()
        {
            var invalidDto = GetInvalidDtoForUpdate();
            var existentDto = GetExistantDto();
            await SetupForUpdate(invalidDto, existentDto);

            await themebookServiceMock.Update(invalidDto).ShouldThrowAsync<AggregateException>();
        }



        [Test]
        public async Task Update_InvalidImprovement_ThrowsAggregateException()
        {
            var invalidImprovementDto = GetValidThemebookWithInvalidImprovementDto();

            var existentDto = GetExistantDto();
            await SetupForUpdate(invalidImprovementDto, existentDto);

            await themebookServiceMock.Update(invalidImprovementDto).ShouldThrowAsync<AggregateException>();
        }

        [Test]
        public async Task Update_InvalidTagQuestion_ThrowsDomainException()
        {
            var invalidTagQuestionDto = GetValidThemebookWithInvalidTagQuestionDto();
            var existentDto = GetExistantDto();
            await SetupForUpdate(invalidTagQuestionDto, existentDto);

            await themebookServiceMock.Update(invalidTagQuestionDto).ShouldThrowAsync<DomainException>();

        }

        #region Utility Functions
        private async Task SetupForUpdate(ThemebookDto invalidDto, ThemebookDto existentDto)
        {
            var existentThemebook = await themebookServiceMock.Create(existentDto);

            invalidDto.Id = existentThemebook.Id;

            themebookRepositoryMock.Setup(a => a.IsUniqueByCode(It.IsAny<string>())).ReturnsAsync(true);
            themebookRepositoryMock.Setup(a => a.GetByCode(It.IsAny<string>())).ReturnsAsync(existentThemebook);
            themebookRepositoryMock.Setup(a => a.GetByKey(It.IsAny<Guid>())).ReturnsAsync(existentThemebook);
        }

        private static bool IsThemebookTheSameCreated(Themebook themebook, Themebook createdThemebook)
            => themebook.Id == createdThemebook.Id
            && themebook.Name == createdThemebook.Name
            && themebook.Description == createdThemebook.Description
            && themebook.Type == createdThemebook.Type
            && themebook.Concept == createdThemebook.Concept;


        private ThemebookDto GetExistantDto() => GetValidDtoBuilder().WithId(ThemebookDtoDefaultValues.id_updated).Build();

        private ThemebookDto GetValidDto() => GetValidDtoBuilder().Build();

        private ThemebookDtoBuilder GetValidDtoBuilder()
            => GetValidDtoBuilderWithoutCollections()
                        .WithImprovements(GetValidImprovementList())
                        .WithTagQuestions(GetValidTagQuestionList());

        private ThemebookDto GetValidThemebookWithInvalidTagQuestionDto()
            => GetValidDtoBuilderWithoutCollections()
                        .WithTagQuestions(GetInvalidTagQuestionList())
                        .Build();

        private List<TagQuestionDto> GetInvalidTagQuestionList() => new List<TagQuestionDto> { GetInvalidTagQuestionDto() };

        private TagQuestionDto GetInvalidTagQuestionDto()
            => tagQuestionDtoBuilder.WithQuestion(null)
                                    .WithType(-1)
                                    .Build();

        private List<TagQuestionDto> GetValidTagQuestionList() => new List<TagQuestionDto> { GetValidTagQuestionDto() };

        private TagQuestionDto GetValidTagQuestionDto()
            => tagQuestionDtoBuilder.WithQuestion(TagQuestionDtoDefaultValues.question)
                                    .WithType(TagQuestionDtoDefaultValues.type)
                                    .Build();

        private ThemebookDto GetValidThemebookWithInvalidImprovementDto()
            => GetValidDtoBuilderWithoutCollections().WithImprovements(GetInvalidImprovementList()).Build();

        private List<ThemebookImprovementDto> GetInvalidImprovementList()
            => new List<ThemebookImprovementDto> { GetInvalidImprovement() };

        private ThemebookImprovementDto GetInvalidImprovement()
            => themebookImprovementDtoBuilder.WithTitle(null)
                                             .WithDecription(null)
                                             .Build();


        private List<ThemebookImprovementDto> GetValidImprovementList()
            => new List<ThemebookImprovementDto> { GetValidImprovement() };

        private ThemebookImprovementDto GetValidImprovement()
            => themebookImprovementDtoBuilder.WithTitle(ThemebookImprovementDtoDefaultValues.title)
                                             .WithDecription(ThemebookImprovementDtoDefaultValues.decription)
                                             .Build();


        private ThemebookDto GetValidDtoWithoutCollections()
            => GetValidDtoBuilderWithoutCollections()
                                  .Build();

        private ThemebookDtoBuilder GetValidDtoBuilderWithoutCollections()
            => themebookDtoBuilder.WithId(ThemebookDtoDefaultValues.id_create)
                                  .WithName(ThemebookDtoDefaultValues.name)
                                  .WithDescription(ThemebookDtoDefaultValues.description)
                                  .WithUserId(ThemebookDtoDefaultValues.userId)
                                  .WithTypeId(ThemebookDtoDefaultValues.typeId)
                                  .WithThemebookConcept(GetValidThemebookConceptDto());

        private ThemebookConceptDto GetValidThemebookConceptDto()
            => themebookConceptDtoBuilder.WithUserId(ThemebookConceptDtoDefaultValues.userId)
                                         .WithQuestion(ThemebookConceptDtoDefaultValues.question)
                                         .Build();

        private ThemebookDto GetInvalidDtoWithValidName()
            => themebookDtoBuilder.WithId(ThemebookDtoDefaultValues.id_create)
                                  .WithName(ThemebookDtoDefaultValues.name)
                                  .Build();

        private ThemebookDto GetInvalidDto() => GetInvalidBuilderDto().Build();
        private ThemebookDto GetInvalidDtoForUpdate() => GetInvalidBuilderDto().WithId(ThemebookDtoDefaultValues.id_updated).Build();



        private ThemebookDtoBuilder GetInvalidBuilderDto()
            => themebookDtoBuilder.WithId(ThemebookDtoDefaultValues.id_create)
                                  .WithName(null)
                                  .WithDescription(null)
                                  .WithThemebookConcept(null);
        #endregion
    }
}
