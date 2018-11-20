using System.Collections.Generic;
using FluentValidation.Results;

namespace OpenGraphTilemaker.Web.Client.Features.Form
{
    public interface IValidate
    {
        IList<ValidationFailure> Validate(string property = null);
    }
}
