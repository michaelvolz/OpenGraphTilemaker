using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using FluentValidation;
using FluentValidation.Results;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable ClassNeverInstantiated.Global

namespace OpenGraphTilemaker.Web.Client.Features.Form
{
    public partial class Person : IValidate
    {
        public IList<ValidationFailure> Validate(string property = null) {
            var validator = new PersonValidator();

            var result = string.IsNullOrWhiteSpace(property)
                ? validator.Validate(this)
                : validator.Validate(this, property);

            return result.Errors;
        }

        public async Task<IList<ValidationFailure>> ValidateAsync(string property = null, CancellationToken token = default) {
            var validator = new PersonValidator();

            var result = string.IsNullOrWhiteSpace(property)
                ? await validator.ValidateAsync(this, token)
                : await validator.ValidateAsync(this, token, property);

            return result.Errors;
        }

        public IList<ValidationFailure> Validate(Expression<Func<object>> property) {
            return property != null ? Validate(property.MemberExpressionName()) : Validate();
        }

        public string IsValid(Expression<Func<object>> property) {
            return HasError(property) ? "is-invalid" : "is-valid";
        }

        public bool HasError(Expression<Func<object>> property = null) {
            return Validate(property).Any();
        }
    }
}
