using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Results;

// ReSharper disable UnusedMemberInSuper.Global

namespace OpenGraphTilemaker.Web.Client.Features.Form
{
    public interface IValidate
    {
        IList<ValidationFailure> Validate<T>(string property = null) where T : class;
        Task<IList<ValidationFailure>> ValidateAsync<T>(T obj, string property = null, CancellationToken token = default);
    }
}
