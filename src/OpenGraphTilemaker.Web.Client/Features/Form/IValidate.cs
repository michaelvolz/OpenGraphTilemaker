using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Results;

namespace OpenGraphTilemaker.Web.Client.Features.Form
{
    public interface IValidate
    {
        IList<ValidationFailure> Validate(string property = null);
        Task<IList<ValidationFailure>> ValidateAsync(string property = null, CancellationToken token = default);
    }
}
