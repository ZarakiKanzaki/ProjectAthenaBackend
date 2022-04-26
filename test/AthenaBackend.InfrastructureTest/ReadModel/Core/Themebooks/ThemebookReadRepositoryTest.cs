using AthenaBackend.Common.DomainDrivenDesign;
using AthenaBackend.Domain.Exceptions;
using AthenaBackend.Domain.WellKnownInstances;
using AthenaBackend.Infrastructure;
using AthenaBackend.Infrastructure.ReadModel.Core.Themebooks;
using AthenaBackend.Infrastructure.ReadModel.Core.Themebooks.UI;
using AthenaBackend.InfrastructureTest.ReadModel.Core.Themebooks.Builders;
using AthenaBackend.InfrastructureTest.ReadModel.Core.Themebooks.Defaults;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthenaBackend.InfrastructureTest.ReadModel.Core.Themebooks
{
    public class ThemebookReadRepositoryTest
    {
        private TagQuestionUIBuilder tagQuestionUIBuilder;
        private ThemebookConceptUIBuilder themebookConceptUIBuilder;
        private ThemebookUIBuilder themebookUIBuilder;
        private ThemebookImprovementUIBuilder themebookImprovementUIBuilder;
        private Mock<ReadDbContext> context;
        private IReadRepository<ThemebookUI, Guid> themebookRepository;

        [SetUp]
        public void Setup()
        {
            InitializeBuilders();
            InitializeDbContext();
            themebookRepository = new ThemebookReadRepository(context.Object);
        }

        [Test]
        public async Task FindByCode_ValidCode_ShouldNotBeNull()
        {
            var validThemebook = await themebookRepository.FindByCode($"{ThemebookUIDefaultValues.name}-{1}");

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
            var validThemebook = await themebookRepository.GetByCode($"{ThemebookUIDefaultValues.name}-{1}");

            validThemebook.ShouldNotBeNull();
        }

        [Test]
        public async Task GetByCode_InvalidCode_ThrowsDomainException()
        {
            await themebookRepository.GetByCode($"TEST").ShouldThrowAsync<DomainException>();
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

        [Test]
        public async Task GetList_ListPopulated()
        {
            var list = await themebookRepository.GetList();
            list.Count().ShouldBe(3);
        }

        #region MyRegion
        private void InitializeDbContext()
        {
            var options = new DbContextOptionsBuilder<ReadDbContext>().Options;

            var themebooks = GetThemebooks();
            var concepts = new List<ThemebookConceptUI>();
            var tags = new List<TagQuestionUI>();
            var improvements = new List<ThemebookImprovementUI>();

            HandleThemebookIdForEntities(themebooks, concepts, tags, improvements);

            context = new Mock<ReadDbContext>(options);
            SetupDbSets(themebooks, concepts, tags, improvements);
        }

        private List<ThemebookUI> GetThemebooks()
        {
            return new List<ThemebookUI>()
            {
                GetValidRandomUI(1),
                GetValidRandomUI(2),
                GetValidRandomUI(3),
            };
        }

        private void SetupDbSets(List<ThemebookUI> themebooks, List<ThemebookConceptUI> concepts, List<TagQuestionUI> tags, List<ThemebookImprovementUI> improvements)
        {
            context.Setup(c => c.ThemebookUI).ReturnsDbSet(themebooks);
            context.Setup(c => c.ThemebookConceptUI).ReturnsDbSet(concepts);
            context.Setup(c => c.TagQuestionUI).ReturnsDbSet(tags);
            context.Setup(c => c.ThemebookImprovementUI).ReturnsDbSet(improvements);
            context.Setup(c => c.ThemebookUI.FindAsync(It.IsAny<Guid>())).ReturnsAsync(themebooks.FirstOrDefault(x => x.Id == It.IsAny<Guid>()));
        }

        private static void HandleThemebookIdForEntities(List<ThemebookUI> themebooks, List<ThemebookConceptUI> concepts, List<TagQuestionUI> tags, List<ThemebookImprovementUI> improvements)
        {
            themebooks.ForEach(themebook =>
            {
                themebook.ThemebookConcept.ThemebookId = themebook.Id;

                themebook.TagQuestions.ForEach(tagQuestion => tagQuestion.ThemebookId = themebook.Id);
                themebook.Improvements.ForEach(improvement => improvement.ThemebookId = themebook.Id);

                concepts.Add(themebook.ThemebookConcept);
                tags.AddRange(themebook.TagQuestions);
                improvements.AddRange(themebook.Improvements);
            });
        }

        private void InitializeBuilders()
        {
            tagQuestionUIBuilder = new TagQuestionUIBuilder();
            themebookConceptUIBuilder = new ThemebookConceptUIBuilder();
            themebookUIBuilder = new ThemebookUIBuilder();
            themebookImprovementUIBuilder = new ThemebookImprovementUIBuilder();
        }

        private ThemebookUI GetValidRandomUI(short index)
            => GetValidUIBuilder().WithName($"{ThemebookUIDefaultValues.name}-{index}")
                                  .Build();
        private ThemebookUIBuilder GetValidUIBuilder()
            => GetValidUIBuilderWithoutCollections()
                        .WithImprovements(GetValidImprovementList())
                        .WithTagQuestions(GetValidTagQuestionList());

        private ThemebookUIBuilder GetValidUIBuilderWithoutCollections()
            => themebookUIBuilder.WithId(ThemebookUIDefaultValues.id_updated)
                                 .WithName(ThemebookUIDefaultValues.name)
                                 .WithDescription(ThemebookUIDefaultValues.description)
                                 .WithType(ThemebookTypes.GetThemebookByKey(ThemebookUIDefaultValues.typeId))
                                 .WithThemebookConcept(GetValidThemebookConceptUI());

        private List<ThemebookImprovementUI> GetValidImprovementList()
            => new List<ThemebookImprovementUI> { GetValidImprovement() };

        private ThemebookImprovementUI GetValidImprovement()
            => themebookImprovementUIBuilder.WithTitle(ThemebookImprovementUIDefaultValues.title)
                                            .WithDecription(ThemebookImprovementUIDefaultValues.decription)
                                            .Build();

        private List<TagQuestionUI> GetValidTagQuestionList() => new List<TagQuestionUI> { GetValidTagQuestionUI() };

        private TagQuestionUI GetValidTagQuestionUI()
            => tagQuestionUIBuilder.WithQuestion(TagQuestionUIDefaultValues.question)
                                   .WithType(TagQuestionUIDefaultValues.type)
                                   .Build();
        private ThemebookConceptUI GetValidThemebookConceptUI()
            => themebookConceptUIBuilder.WithQuestion(ThemebookConceptUIDefaultValues.question)
                                        .Build();
        #endregion
    }
}
