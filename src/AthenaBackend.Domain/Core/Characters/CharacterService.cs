using AthenaBackend.Common.DomainDrivenDesign;
using AthenaBackend.Domain.Core.Characters.Dtos;
using AthenaBackend.Domain.Exceptions;
using System;
using System.Threading.Tasks;

namespace AthenaBackend.Domain.Core.Characters
{
    public class CharacterService : IService
    {
        private readonly ICharacterRepository characterRepository;

        public CharacterService(ICharacterRepository characterRepository) => this.characterRepository = characterRepository;

        public async Task<Character> Create(CharacterDto dto)
        {
            if (await characterRepository.IsUniqueByCode(dto.Name) == false)
            {
                throw new CodeAlreadyExistsDomainException(nameof(Character), nameof(dto.Name));
            }

            var createdCharacter = Character.Create(dto);
            await characterRepository.Add(createdCharacter);

            return createdCharacter;
        }

        public async Task Update(CharacterDto dto)
        {
            if (await IsUniqueByCodeAndItIsTheSameEntity(dto) == false)
            {
                throw new CodeAlreadyExistsDomainException(nameof(Character), nameof(dto.Name));
            }

            var character = await GetCharacter((Guid)dto.Id);

            character.Update(dto);
        }

        public async Task Delete(Guid userId, Guid characterId) => (await GetCharacter(characterId)).Delete(userId);

        private async Task<Character> GetCharacter(Guid id) => await characterRepository.GetByKey(id);

        private async Task<bool> IsUniqueByCodeAndItIsTheSameEntity(CharacterDto dto)
            => await characterRepository.IsUniqueByCode(dto.Name)
            && (await characterRepository.GetByCode(dto.Name)).Id == dto.Id;
    }
}
