using AthenaBackend.Common.Converters;
using AthenaBackend.Common.DomainDrivenDesign;
using AthenaBackend.Domain.Core.Characters;
using AthenaBackend.Infrastructure.ReadModel.Core.User.UI;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using ApplicationCharacterDto = AthenaBackend.Application.WriteModel.Core.Characters.Dtos.CharacterDto;
using DomainCharacterDto = AthenaBackend.Domain.Core.Characters.Dtos.CharacterDto;

namespace AthenaBackend.Application.WriteModel.Core.Characters
{

    public class CreateCharacterCommand : IRequest<bool>
    {
        public ApplicationCharacterDto Character { get; set; }
        public CreateCharacterCommand(ApplicationCharacterDto character) => Character = character;
    }

    public class CreateCharacterCommandHandler : IRequestHandler<CreateCharacterCommand, bool>
    {
        private readonly IConverter<ApplicationCharacterDto, DomainCharacterDto> converter;
        private readonly IReadRepository<UserUI, Guid> readRepository;
        private readonly CharacterService characterService;

        public CreateCharacterCommandHandler(IConverter<ApplicationCharacterDto, DomainCharacterDto> converter, IReadRepository<UserUI, Guid> readRepository, CharacterService characterService)
        {
            this.converter = converter;
            this.readRepository = readRepository;
            this.characterService = characterService;
        }

        public async Task<bool> Handle(CreateCharacterCommand request, CancellationToken cancellationToken)
        {
            var userId = (await readRepository.GetByCode(request.Character.GuildMember)).Id;
            request.Character.UserId = userId;
            var createdCharacter = await characterService.Create(converter.Convert(request.Character));

            return createdCharacter != null;
        }
    }
}
