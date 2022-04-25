using AthenaBackend.Application.WriteModel.Core.Characters;
using AthenaBackend.Application.WriteModel.Core.Characters.Dtos;
using HotChocolate;
using MediatR;
using System.Threading.Tasks;

namespace AthenaBackend.WebApi.DataApplicationRequest
{
    public class Mutation
    {

        public async Task<bool> CreateCharacter([Service] ISender Mediator, CharacterDto character) 
            => await Mediator.Send(new CreateCharacterCommand(character));

    }
}
