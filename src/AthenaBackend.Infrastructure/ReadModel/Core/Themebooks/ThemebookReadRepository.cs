using AthenaBackend.Common.DomainDrivenDesign;
using AthenaBackend.Domain.Exceptions;
using AthenaBackend.Infrastructure.ReadModel.Core.Themebooks.UI;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthenaBackend.Infrastructure.ReadModel.Core.Themebooks
{
    public class ThemebookReadRepository : BaseReadRepository<ThemebookUI, Guid>, IReadRepository<ThemebookUI, Guid>
    {
        public ThemebookReadRepository(ReadDbContext context) : base(context)
        {
        }

        public async Task<ThemebookUI> FindByCode(string code)
        {
            var themebook = await FindThemebookByCode(code);

            if (themebook == null)
            {
                return null;
            }
            await HandleRelationships(themebook);

            return themebook;
        }


        public async Task<ThemebookUI> FindByKey(Guid id)
        {
            var themebook = await FindThemebookByKey(id);

            if (themebook == null)
            {
                return null;
            }
            await HandleRelationships(themebook);

            return themebook;
        }

        public async Task<ThemebookUI> GetByCode(string code)
        {
            var themebook = await FindThemebookByCode(code)
                            ?? throw new CannotFindEntityDomainException(nameof(ThemebookUI), nameof(code), code);

            await HandleRelationships(themebook);

            return themebook;
        }

        public async Task<ThemebookUI> GetByKey(Guid id)
        {
            var themebook = await FindThemebookByKey(id)
                            ?? throw new CannotFindEntityDomainException(nameof(ThemebookUI), nameof(id), id);
            await HandleRelationships(themebook);

            return themebook;
        }

        public async Task<IEnumerable<ThemebookUI>> GetList()
        {
            return await Task.FromResult(Context.ThemebookUI.Select(t =>
                    new ThemebookUI
                    {
                        CrewRelationships = t.CrewRelationships,
                        Description = t.Description,
                        ExamplesOfApplication = t.ExamplesOfApplication,
                        Id = t.Id,
                        Name = t.Name,
                        MisteryOptions = t.MisteryOptions,
                        TitleExamples = t.TitleExamples,
                        Type = t.Type,
                        Improvements = GetImprovements(t).ToList(),
                        TagQuestions = GetTagQuestions(t).ToList(),
                        ThemebookConcept = GetConceptSync(t),
                    }
                ).AsEnumerable());
        }

        #region Private functions
        private ThemebookConceptUI GetConceptSync(ThemebookUI themebook)
            => Context.ThemebookConceptUI.FirstOrDefault(tc => tc.ThemebookId == themebook.Id);

        private async Task<ThemebookUI> FindThemebookByKey(Guid id)
            => await Context.ThemebookUI.FindAsync(id);

        private async Task<ThemebookUI> FindThemebookByCode(string code)
            => await Context.ThemebookUI.FirstOrDefaultAsync(t => t.Name.ToLower().Equals(code.ToLower()));

        private async Task HandleRelationships(ThemebookUI themebook)
        {
            var concept = await GetConcept(themebook);
            var tagQuestions = GetTagQuestions(themebook);
            var improvements = GetImprovements(themebook);

            await AssignRelationships(themebook, concept, tagQuestions, improvements);
        }

        private static async Task AssignRelationships(ThemebookUI themebook, ThemebookConceptUI concept, IQueryable<TagQuestionUI> tagQuestions, IQueryable<ThemebookImprovementUI> improvements)
        {
            themebook.ThemebookConcept = concept;
            themebook.TagQuestions = await tagQuestions.ToListAsync();
            themebook.Improvements = await improvements.ToListAsync();
        }

        private IQueryable<ThemebookImprovementUI> GetImprovements(ThemebookUI themebook)
            => Context.ThemebookImprovementUI.Where(ti => ti.ThemebookId == themebook.Id);

        private IQueryable<TagQuestionUI> GetTagQuestions(ThemebookUI themebook)
            => Context.TagQuestionUI.Where(tq => tq.ThemebookId == themebook.Id);

        private async Task<ThemebookConceptUI> GetConcept(ThemebookUI themebook)
            => await Context.ThemebookConceptUI.FirstOrDefaultAsync(tc => tc.ThemebookId == themebook.Id);
        #endregion
    }
}
