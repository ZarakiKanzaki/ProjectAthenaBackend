using NUnit.Framework;
using AthenaBackend.DomainTest.Core.Themebooks.Builders;
using Shouldly;
using System;
using System.Linq;
using Moq;
using AthenaBackend.Domain.Core.Themebooks;
using System.Threading.Tasks;
using AthenaBackend.Domain.Core.Themebooks.Dtos;
using AthenaBackend.DomainTest.Core.Themebooks.Defaults;
using AthenaBackend.Domain.Exceptions;

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
        public void Create_ValidNameButNonUniqueDto_ThrowsAggregateException()
        {
            var invalidDto = GetInvalidDtoWithValidName();

            themebookRepositoryMock.Setup(a => a.IsUniqueByCode(It.IsAny<string>())).ReturnsAsync(false);

            Should.ThrowAsync<CodeAlreadyExistsDomainException>(async () => await themebookServiceMock.Create(invalidDto));
        }

        [Test]
        public void Create_InvalidDto_ThrowsAggregateException()
        {
            var invalidDto = GetInvalidDto();

            Should.ThrowAsync<AggregateException>(async () => await themebookServiceMock.Create(invalidDto));
        }



        [Test]
        public async Task Create_ValidDtoWithoutCollections_ExcecutedCorrectly()
        {
            var validDtoWithoutCollections = GetValidDtoWithoutCollections();

            var createdThemebook = await themebookServiceMock.Create(validDtoWithoutCollections);

            themebookRepositoryMock.Verify(t => 
                        t.Add(It.Is<Themebook>(themebook =>
                            themebook.Id == createdThemebook.Id
                            && themebook.Name == createdThemebook.Name
                            && themebook.Description == createdThemebook.Description
                            && themebook.Type == createdThemebook.Type
                            && themebook.Concept == createdThemebook.Concept
                        
                        )),
                        Times.Once);

        }

        private ThemebookDto GetValidDtoWithoutCollections()
            => themebookDtoBuilder.WithId(ThemebookDtoDefaultValues.id_create)
                                  .WithName(ThemebookDtoDefaultValues.name)
                                  .WithDescription(ThemebookDtoDefaultValues.description)
                                  .WithUserId(ThemebookDtoDefaultValues.userId)
                                  .WithTypeId(ThemebookDtoDefaultValues.typeId)
                                  .WithThemebookConcept(GetValidThemebookConceptDto())
                                  .Build();

        private ThemebookConceptDto GetValidThemebookConceptDto() 
            => themebookConceptDtoBuilder.WithUserId(ThemebookConceptDtoDefaultValues.userId)
                                         .WithQuestion(ThemebookConceptDtoDefaultValues.question)
                                         .Build();

        private ThemebookDto GetInvalidDtoWithValidName()
        {
            return themebookDtoBuilder.WithId(ThemebookDtoDefaultValues.id_create)
                                      .WithName(ThemebookDtoDefaultValues.name)
                                      .Build();
        }

        private ThemebookDto GetInvalidDto() 
            => themebookDtoBuilder.WithId(ThemebookDtoDefaultValues.id_create)
                                  .WithName(null)
                                  .WithDescription(null)
                                  .WithThemebookConcept(null)
                                  .Build();
    }
}
