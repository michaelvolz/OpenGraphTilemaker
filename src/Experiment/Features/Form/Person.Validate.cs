﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using FluentValidation;
using FluentValidation.Results;

namespace Experiment.Features.Form
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "Utility class")]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1649:File name should match first type name", Justification = "TODO")]
    public class ValidationBase<TValidator> : IValidate
    {
        public IList<ValidationFailure> Validate<T>(string? propertyName = null)
            where T : class
        {
            var validator = Activator.CreateInstance<TValidator>() as IValidator<T>;

            if (validator == null) throw new InvalidOperationException("Couldn't initialize validator!");

            var result = string.IsNullOrWhiteSpace(propertyName)
                ? validator.Validate((this as T)!)
                : validator!.Validate(this as T, options => options.IncludeProperties(propertyName));

            return result.Errors;
        }

        public async Task<IList<ValidationFailure>> ValidateAsync<T>(T subject, string? propertyName = null, CancellationToken token = default)
        {
            var validator = Activator.CreateInstance<TValidator>() as IValidator<T>;

            if (validator == null) throw new InvalidOperationException("Couldn't initialize validator!");

            var result = string.IsNullOrWhiteSpace(propertyName)
                ? await validator.ValidateAsync(subject, token)
                : await validator.ValidateAsync(subject, options => options.IncludeProperties(propertyName), token);

            return result.Errors;
        }

        public bool HasError<T>(Expression<Func<object>>? propertyExpression = null)
            where T : class => Validate<T>(propertyExpression).Any();

        public IList<ValidationFailure> Validate<T>(Expression<Func<object>>? property)
            where T : class =>
            property != null ? Validate<T>(property.MemberExpressionName()) : Validate<T>();

        public string IsValid<T>(Expression<Func<object>> property, string failureClass)
            where T : class => HasError<T>(property) ? failureClass : string.Empty;
    }
}
