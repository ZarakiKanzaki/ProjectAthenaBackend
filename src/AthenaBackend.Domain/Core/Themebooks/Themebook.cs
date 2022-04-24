using AthenaBackend.Common.DomainDrivenDesign;
using AthenaBackend.Domain.Core.Themebooks.Dtos;
using AthenaBackend.Domain.Enums;
using AthenaBackend.Domain.Exceptions;
using AthenaBackend.Domain.WellKnownInstances;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AthenaBackend.Domain.Core.Themebooks
{
    public class Themebook : Aggregate<Guid>
    {
        public virtual string Name { get; protected set; }
        public virtual string Description { get; protected set; }
        public virtual ThemebookType Type { get; protected set; }
        public virtual ThemebookConcept Concept { get; protected set; }

        private List<string> examplesOfApplication = new();
        private List<string> misteryOptions = new();
        private List<string> titleExamples = new();
        private List<string> crewRelationships = new();

        public virtual IEnumerable<string> ExamplesOfApplication => examplesOfApplication ??= new List<string>();
        public virtual IEnumerable<string> MisteryOptions => misteryOptions ??= new List<string>();
        public virtual IEnumerable<string> TitleExamples => titleExamples ??= new List<string>();
        public virtual IEnumerable<string> CrewRelationships => crewRelationships ??= new List<string>();


        private ICollection<TagQuestion> tagQuestions = new List<TagQuestion>();
        public virtual IEnumerable<TagQuestion> TagQuestions => tagQuestions ??= new List<TagQuestion>();

        private ICollection<ThemebookImprovement> improvements = new List<ThemebookImprovement>();
        public virtual IEnumerable<ThemebookImprovement> Improvements => improvements ??= new List<ThemebookImprovement>();


        protected virtual IEnumerable<TagQuestion> PowerTagQuestions => TagQuestions.Where(a => a.Type == TagType.Power);
        protected virtual IEnumerable<TagQuestion> WeaknessTagQuestions => TagQuestions.Where(a => a.Type == TagType.Weakness);

        protected Themebook()
        {

        }

        protected Themebook(Guid userCreatedId) : base(userCreatedId)
        {
        }


        protected internal static Themebook Create(ThemebookDto dto)
        {
            Validate(dto);

            var themebook = new Themebook(dto.UserId)
            {
                Name = dto.Name,
                Description = dto.Description,
                Type = ThemebookTypes.GetThemebookByKey(dto.TypeId),
            };

            themebook.Concept = ThemebookConcept.Create(themebook, dto.ThemebookConcept);
            themebook.AddExamplesOfApplication(dto.ExamplesOfApplication);
            themebook.AddMysteryOptions(dto.MisteryOptions);
            themebook.AddTitleExamples(dto.TitleExamples);
            themebook.AddCrewRelationships(dto.CrewRelationships);

            themebook.AddTagQuestions(dto.TagQuestions);
            themebook.AddImprovements(dto.Improvements);

            return themebook;
        }

        protected internal void Update(ThemebookDto dto)
        {
            Validate(dto);

            Name = dto.Name;
            Description = dto.Description;
            Type = ThemebookTypes.GetThemebookByKey(dto.TypeId);
            Concept.Update(dto.ThemebookConcept);
            
            HandleExamplesOfApplications(dto.ExamplesOfApplication);
            HandleMisteryOptions(dto.MisteryOptions);
            HandleTitleExamples(dto.TitleExamples);
            HandleCrewRelationships(dto.CrewRelationships);

            HandleTagQuestions(dto.TagQuestions);
            HandleImprovements(dto.Improvements);

            UpdateOperationLog(dto.UserId);
        }

        protected internal void Delete(Guid userId)
        {
            Delete();
            DeleteOperationLog(userId);
        }

        public void SetIdForUniTesting() => Id = Guid.NewGuid();

        #region Improvements
        private void HandleImprovements(List<ThemebookImprovementDto> improvements)
        {
            var toAdd = improvements.Where(imp => imp.Id is null);
            var toUpdate = improvements.Where(imp => Improvements.Any(improvement => improvement.IsDeleted == false
                                                                                  && improvement.Id == imp.Id));
            var toRemove = Improvements.Where(pt => pt.IsDeleted == false)
                                            .Select(pt => pt.Id)
                                            .Except(toUpdate.Select(pt => (Guid)pt.Id));
            AddImprovements(toAdd);
            UpdateImprovements(toUpdate);
            DeleteImprovements(toRemove);
        }

        private void DeleteImprovements(IEnumerable<Guid> toRemove)
        {
            foreach (var improvementId in toRemove)
            {
                var improvement = GetImprovementByKey(improvementId);
                improvement.Delete();
            }
        }

        private void UpdateImprovements(IEnumerable<ThemebookImprovementDto> toUpdate)
        {
            foreach (var improvement in toUpdate)
            {
                var improvementToUpdate = GetImprovementByKey((Guid)improvement.Id);
                improvementToUpdate.Update(improvement);
            }
        }

        private ThemebookImprovement GetImprovementByKey(Guid improvementId)
        {
            return Improvements.FirstOrDefault(imp => imp.Id == improvementId);
        }

        private void AddImprovements(IEnumerable<ThemebookImprovementDto> toAdd)
        {
            foreach (var improvement in toAdd)
            {
                ThemebookImprovement.Create(this, improvement);
            }
        }
        #endregion

        #region Power and WeaknessTag
        private void HandleTagQuestions(List<TagQuestionDto> tagQuestions)
        {
            var toAdd = tagQuestions.Where(pt => pt.Id is null);
            var toUpdate = tagQuestions.Where(pt => TagQuestions.Any(power => power.IsDeleted == false
                                                                                     && power.Id == pt.Id));
            var toRemove = TagQuestions.Where(pt => pt.IsDeleted == false)
                                            .Select(pt => pt.Id)
                                            .Except(toUpdate.Select(pt => (Guid)pt.Id));
            AddTagQuestions(toAdd);

            UpdateTagToUpdate(toUpdate);
            DeletTagQuestions(toRemove);
        }

        private void DeletTagQuestions(IEnumerable<Guid> toRemove)
        {
            foreach (var powerId in toRemove)
            {
                var tagToRemove = GetTagByKey(powerId);
                tagToRemove.Delete();
            }
        }

        private void UpdateTagToUpdate(IEnumerable<TagQuestionDto> toUpdate)
        {
            foreach (var power in toUpdate)
            {
                var tagToUpdate = GetTagByKey((Guid)power.Id);
                tagToUpdate.Update(power);
            }
        }

        private void AddTagQuestions(IEnumerable<TagQuestionDto> toAdd)
        {
            foreach (var tagToAdd in toAdd)
            {
                TagQuestion.Create(this, tagToAdd);
            }
        }

        private TagQuestion GetTagByKey(Guid id)
            => TagQuestions.FirstOrDefault(pt => pt.Id == id);


        #endregion

        #region String collections
        private void HandleCrewRelationships(List<string> crewRelationships)
        {
            this.crewRelationships.Clear();
            AddCrewRelationships(crewRelationships);
        }

        private void AddCrewRelationships(List<string> crewRelationships)
            => this.crewRelationships.AddRange(crewRelationships);

        private void HandleTitleExamples(List<string> titleExamples)
        {
            this.titleExamples.Clear();
            AddTitleExamples(titleExamples);
        }

        private void AddTitleExamples(List<string> titleExamples)
            => this.titleExamples.AddRange(titleExamples);

        private void HandleMisteryOptions(List<string> misteryOptions)
        {
            this.misteryOptions.Clear();
            AddMysteryOptions(misteryOptions);
        }

        private void AddMysteryOptions(List<string> misteryOptions)
            => this.misteryOptions.AddRange(misteryOptions);

        private void HandleExamplesOfApplications(List<string> examplesOfApplication)
        {
            this.examplesOfApplication.Clear();
            AddExamplesOfApplication(examplesOfApplication);
        }

        private void AddExamplesOfApplication(List<string> examplesOfApplication)
            => this.examplesOfApplication.AddRange(examplesOfApplication);


        #endregion

        #region Private Functions
        private static void Validate(ThemebookDto dto)
        {
            var domainExceptions = new List<DomainException>();

            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                domainExceptions.Add(new NullOrWhiteSpaceDomainException(nameof(dto.Name)));
            }

            if (string.IsNullOrWhiteSpace(dto.Description))
            {
                domainExceptions.Add(new NullOrWhiteSpaceDomainException(nameof(dto.Description)));
            }

            if (dto.ThemebookConcept == null)
            {
                domainExceptions.Add(new NullReferenceDomainException(nameof(dto.Description)));
            }

            if (domainExceptions.Any())
            {
                throw new AggregateException(domainExceptions);
            }
        }
        #endregion
    }
}
