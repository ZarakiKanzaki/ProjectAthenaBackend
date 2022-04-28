using AthenaBackend.Domain.Core.Characters;
using AthenaBackend.Domain.Core.Characters.Dtos;
using AthenaBackend.Domain.Enums;
using AthenaBackend.DomainTest.Core.Characters.Builders;
using AthenaBackend.DomainTest.Core.Characters.Defaults;
using Moq;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthenaBackend.DomainTest.Core.Characters
{

    public class CharacterTest
    {
        private static readonly int LEGAL_NUMBER_OF_THEMEBOOKS = 4;
        private static readonly int MINIMUM_POWERTAG_ALLOWED = 3;
        private static readonly int MINIMUM_WEAKNESSTAG_ALLOWED = 1;
        private static readonly int MINIMUM_WEAKNESSTAGS_FOR_CHARACTER = LEGAL_NUMBER_OF_THEMEBOOKS * MINIMUM_WEAKNESSTAG_ALLOWED;
        private static readonly int MINIMUM_POWERTAGS_FOR_CHARACTER = LEGAL_NUMBER_OF_THEMEBOOKS * MINIMUM_POWERTAG_ALLOWED;

        private CharacterDtoBuilder characterDtoBuilder;
        private CharacterThemebookDtoBuilder characterThemebookDtoBuilder;
        private CharacterThemebookTagDtoBuilder characterThemebookTagDtoBuilder;
        private TagDtoBuilder tagDtoBuilder;

        private Mock<ICharacterRepository> characterRepository;
        private CharacterService characterService;

        [SetUp]
        public void Setup()
        {
            characterDtoBuilder = new CharacterDtoBuilder();
            characterThemebookDtoBuilder = new CharacterThemebookDtoBuilder();
            characterThemebookTagDtoBuilder = new CharacterThemebookTagDtoBuilder();
            tagDtoBuilder = new TagDtoBuilder();

            characterRepository = new Mock<ICharacterRepository>();
            characterRepository.Setup(a => a.IsUniqueByCode(It.IsAny<string>())).ReturnsAsync(true);

            characterService = new CharacterService(characterRepository.Object);
        }

        [Test]
        public void Create_InvalidBasicPropertyDto_ThrowsAggregateException()
        {
            var invalidBasicPropertyDto = characterDtoBuilder.Build();

            Should.ThrowAsync<AggregateException>(async () => await characterService.Create(invalidBasicPropertyDto));
        }

        [Test]
        public void Create_InvalidThemebook_ThrowsAggregateException()
        {
            var invalidThemebookDto = GetValidDtoWithBasicProperties().Build();

            Should.ThrowAsync<AggregateException>(async () => await characterService.Create(invalidThemebookDto));
        }

        [Test]
        public void Create_InvalidThemebookProperties_ThrowsAggregateException()
        {
            var invalidThemebookPropertiesDto = GetValidDtoWithBasicProperties().WithTags(new List<TagDto> { GetValidTagBuilderDto() })
                                                                                .WithThemebooks(new List<CharacterThemebookDto> { GetValidThemebookDto() }).Build();
            Should.ThrowAsync<AggregateException>(async () => await characterService.Create(invalidThemebookPropertiesDto));
        }


        [Test]
        public async Task Create_ValidDto_ShouldBeExecutedCorrectlyOnce()
        {
            var validDto = GetValidDto();


            var createdCharacter = await characterService.Create(validDto);


            characterRepository.Verify(t =>
                        t.Add(It.Is<Character>(character =>
                                character.Name == createdCharacter.Name
                                && character.Logos == createdCharacter.Logos
                                && createdCharacter.Mythos == createdCharacter.Mythos
                        )),
                        Times.Once);

            (createdCharacter.MythosThemebooks.Count() + createdCharacter.LogosThemebooks.Count()).ShouldBeGreaterThanOrEqualTo(LEGAL_NUMBER_OF_THEMEBOOKS);
            createdCharacter.PowerTags.Count().ShouldBeGreaterThanOrEqualTo(MINIMUM_POWERTAGS_FOR_CHARACTER);
            createdCharacter.WeaknessTags.Count().ShouldBeGreaterThanOrEqualTo(MINIMUM_WEAKNESSTAGS_FOR_CHARACTER);
        }




        private CharacterDto GetValidDto()
        {
            var character = GetValidDtoWithBasicProperties().Build();

            var themebookList = new List<CharacterThemebookDto>();
            var tagList = new List<TagDto>();

            HandleThemebook(themebookList, tagList);

            character.Themebooks = themebookList;
            character.Tags = tagList;

            return character;
        }

        private void HandleThemebook(List<CharacterThemebookDto> themebookList, List<TagDto> tagList)
        {
            for (int themebookIndex = 0; themebookIndex < 4; themebookIndex++)
            {
                var themebookId = Guid.NewGuid();
                var themebook = GetValidThemebookDtoBuilder().WithTypeId(RandomBetweenMythosAndLogos()).WithId(themebookId).Build();

                CreateValidSituationOfPowerTags(tagList, themebookIndex, themebookId, themebook);
                CreateValidSituationOfWeaknessTag(tagList, themebookIndex, themebookId, themebook);

                themebookList.Add(themebook);
            }
        }

        private static short RandomBetweenMythosAndLogos()
        {
            return (short)new Random().Next(1, 3);
        }

        private void CreateValidSituationOfWeaknessTag(List<TagDto> tagList, int themebookIndex, Guid themebookId, CharacterThemebookDto themebook)
        {
            var weaknessTag = GetWeaknessTag(themebookIndex);

            tagList.Add(weaknessTag);
            themebook.Tags.Add(GetAssociationWithWeaknessTag(themebookId, weaknessTag));
        }

        private void CreateValidSituationOfPowerTags(List<TagDto> tagList, int themebookIndex, Guid themebookId, CharacterThemebookDto themebook)
        {
            for (int tagIndex = 0; tagIndex < 3; tagIndex++)
            {
                var powerTag = GetPowerTag(themebookIndex, tagIndex);
                tagList.Add(powerTag);
                themebook.Tags.Add(GetAssociationWithPowerTag(themebookId, powerTag));
            }
        }

        private CharacterThemebookTagDto GetAssociationWithWeaknessTag(Guid themebookId, TagDto weaknessTag)
            => characterThemebookTagDtoBuilder.WithTagName(weaknessTag.Name)
                                              .WithCharacterThemebookId(themebookId)
                                              .Build();

        private TagDto GetWeaknessTag(int themebookIndex, int? tagIndex = 0)
            => GetValidTagDtoBuilder().WithName($"WEAKENSS {TagDtoBuilderDefaultValue.name} B{themebookIndex} T{tagIndex}")
                                      .WithType(TagType.Weakness)
                                      .WithIsSubtractive(true)
                                      .Build();

        private CharacterThemebookTagDto GetAssociationWithPowerTag(Guid themebookId, TagDto powerTag)
            => characterThemebookTagDtoBuilder.WithTagName(powerTag.Name)
                                              .WithCharacterThemebookId(themebookId)
                                              .Build();

        private TagDto GetPowerTag(int themebookIndex, int? tagIndex = 0)
            => GetValidTagDtoBuilder().WithName($"POWER {TagDtoBuilderDefaultValue.name} B{themebookIndex} T{tagIndex}")
                                      .WithType(TagType.Power)
                                      .Build();

        private CharacterThemebookDto GetValidThemebookDto()
            => GetValidThemebookDtoBuilder().Build();

        private CharacterThemebookDtoBuilder GetValidThemebookDtoBuilder()
            => characterThemebookDtoBuilder.WithAttentionLevel(CharacterThemebookDtoBuilderDefaultValues.attentionLevel)
                                           .WithConcept(CharacterThemebookDtoBuilderDefaultValues.concept)
                                           .WithFadeCrackLevel(CharacterThemebookDtoBuilderDefaultValues.fadeCrackLevel)
                                           .WithFlipside(CharacterThemebookDtoBuilderDefaultValues.flipside)
                                           .WithIdentityMistery(CharacterThemebookDtoBuilderDefaultValues.identityMistery)
                                           .WithTitle(CharacterThemebookDtoBuilderDefaultValues.title)
                                           .WithTypeId(CharacterThemebookDtoBuilderDefaultValues.typeId);

        private TagDto GetValidTagBuilderDto()
            => GetValidTagDtoBuilder().Build();

        private TagDtoBuilder GetValidTagDtoBuilder()
            => tagDtoBuilder.WithIsSubtractive(TagDtoBuilderDefaultValue.isSubtractive)
                            .WithLevel(TagDtoBuilderDefaultValue.level)
                            .WithName(TagDtoBuilderDefaultValue.name)
                            .WithType(TagDtoBuilderDefaultValue.type);

        private CharacterDtoBuilder GetValidDtoWithBasicProperties()
            => characterDtoBuilder.WithName(CharacterDtoBuilderDefaultValues.name)
                                  .WithMythos(CharacterDtoBuilderDefaultValues.mythos)
                                  .WithLogos(CharacterDtoBuilderDefaultValues.logos);
    }
}
