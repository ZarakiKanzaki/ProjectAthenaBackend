using AthenaBackend.Common.DomainDrivenDesign;
using AthenaBackend.Domain.Core.Themebooks.Dtos;
using AthenaBackend.Domain.Exceptions;
using System;
using System.Threading.Tasks;

namespace AthenaBackend.Domain.Core.Themebooks
{
    public class ThemebookService : IService
    {
        private readonly IThemebookRepository themebookRepository;

        public ThemebookService(IThemebookRepository themebookRepository) => this.themebookRepository = themebookRepository;

        public async Task<Themebook> Create(ThemebookDto dto)
        {
            if (await themebookRepository.IsUniqueByCode(dto.Name) == false)
            {
                throw new CodeAlreadyExistsDomainException(nameof(Themebook), nameof(dto.Name));
            }

            var createdThemebook = Themebook.Create(dto);
            await themebookRepository.Add(createdThemebook);

            return createdThemebook;
        }

        public async Task Update(ThemebookDto dto)
        {
            if (await IsUniqueByCodeAndItIsTheSameEntity(dto) == false)
            {
                throw new CodeAlreadyExistsDomainException(nameof(Themebook), nameof(dto.Name));
            }

            var themebook = await GetThemebook((Guid)dto.Id);

            themebook.Update(dto);
        }

        public async Task Delete(Guid userId, Guid themebookId) => (await GetThemebook(themebookId)).Delete(userId);

        private async Task<Themebook> GetThemebook(Guid id) => await themebookRepository.GetByKey(id);

        private async Task<bool> IsUniqueByCodeAndItIsTheSameEntity(ThemebookDto dto)
            => await themebookRepository.IsUniqueByCode(dto.Name)
            && (await themebookRepository.GetByCode(dto.Name)).Id == dto.Id;
    }
}
