﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Results;

namespace Experiment.Features.FormControl
{
    public interface IValidate
    {
        IList<ValidationFailure> Validate<T>(string? propertyName = null)
            where T : class;

        Task<IList<ValidationFailure>> ValidateAsync<T>(T subject, string? propertyName = null, CancellationToken token = default);

        bool HasError<T>(Expression<Func<object>>? propertyExpression = null)
            where T : class;
    }
}
