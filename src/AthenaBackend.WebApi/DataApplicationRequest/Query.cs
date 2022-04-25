using AthenaBackend.Application.ReadModel.Core.Themebooks;
using AthenaBackend.Infrastructure.ReadModel.Core.Themebooks.UI;
using HotChocolate;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AthenaBackend.WebApi.DataApplicationRequest
{
    public class Query
    {
        public string Cake => $"It's a lie!";

        public async Task<IEnumerable<ThemebookUI>> Themebooks([Service] ISender Mediator)
            => await Mediator.Send(new GetAllThemebooksQuery());

    }
}
