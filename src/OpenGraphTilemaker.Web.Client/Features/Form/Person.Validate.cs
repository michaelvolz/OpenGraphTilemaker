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
    public class ValidationBase<TValidator> : IValidate
    {
        public IList<ValidationFailure> Validate<T>(string property = null) where T : class {
            var validator = (IValidator<T>)Activator.CreateInstance<TValidator>();

            var result = string.IsNullOrWhiteSpace(property)
                ? validator.Validate(this as T)
                : validator.Validate(this as T, property);

            return result.Errors;
        }

        public async Task<IList<ValidationFailure>> ValidateAsync<T>(T obj, string property = null, CancellationToken token = default) {
            var validator = (IValidator<T>)Activator.CreateInstance<TValidator>();

            var result = string.IsNullOrWhiteSpace(property)
                ? await validator.ValidateAsync(obj, token)
                : await validator.ValidateAsync(obj, token, property);

            return result.Errors;
        }

        public IList<ValidationFailure> Validate<T>(Expression<Func<object>> property) where T : class {
            return property != null ? Validate<T>(property.MemberExpressionName()) : Validate<T>();
        }

        public string IsValid<T>(Expression<Func<object>> property, string failureClass) where T : class {
            return HasError<T>(property) ? failureClass : "";
        }

        public bool HasError<T>(Expression<Func<object>> property = null) where T : class {
            return Validate<T>(property).Any();
        }
    }
}
