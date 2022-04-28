using AthenaBackend.Application.WriteModel.Core.Characters;
using AthenaBackend.Application.WriteModel.Core.Characters.Dtos;
using AthenaBackend.Application.WriteModel.Core.Themebooks;
using AthenaBackend.Application.WriteModel.Core.Themebooks.Dtos;
using HotChocolate;
using MediatR;
using System.Threading.Tasks;

namespace AthenaBackend.WebApi.DataApplicationRequest
{
    public class Mutation
    {

        public async Task<bool> CreateCharacter([Service] ISender Mediator, CharacterDto character)
            => await Mediator.Send(new CreateCharacterCommand(character));

        public async Task<bool> CreateThemebook([Service] ISender Mediator, ThemebookDto themebook)
            => await Mediator.Send(new CreateThemebookCommand(themebook));

    }
}
