using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using FluentValidation;
using FluentValidation.Results;

// ReSharper disable CheckNamespace

namespace Blazor.Validation.Shared
{
    public class PersonValidator : AbstractValidator<Person>
    {
        public PersonValidator() {
            RuleFor(p => p.FirstName).NotNull().NotEmpty().NotEqual("John");
            RuleFor(p => p.LastName).NotEmpty();
        }
    }

    public interface IValidate
    {
        IList<ValidationFailure> GetValidationErrors(string property = null);
    }

    public class Person : IDataErrorInfo, INotifyDataErrorInfo, IValidate
    {
        private const int MinAge = 18;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public string this[string property] => GetErrors(property).FirstOrDefault<string>();

        public string Error => null;

        public bool HasErrors => GetErrors(null).Any();

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public IEnumerable GetErrors(string property) {
            if (property == null || property == nameof(FirstName)) {
                if (string.IsNullOrEmpty(FirstName)) {
                    yield return $"{nameof(FirstName)} is mandatory";
                }

                if (FirstName.Length < 2) {
                    yield return $"{nameof(FirstName)} '{FirstName}' is too short.";
                }

                if (FirstName == "Q") {
                    yield return $"{nameof(FirstName)} 'Q' is reserved for extra-dimensional beings!";
                }
            }

            if (property == null || property == nameof(LastName)) {
                if (string.IsNullOrEmpty(LastName)) {
                    yield return $"{nameof(LastName)} is mandatory";
                }

                if (LastName == "Doe") {
                    yield return $"{nameof(LastName)} cannot be 'Doe'";
                }
            }

            if (property == null || property == nameof(Age)) {
                if (Age < MinAge) {
                    yield return $"{nameof(Age)} should be at least {MinAge}";
                }
            }
        }

        public IList<ValidationFailure> GetValidationErrors(string property) {
            var validator = new PersonValidator();

            var result = string.IsNullOrWhiteSpace(property) ? validator.Validate(this) : validator.Validate(this, property);

            return result.Errors;
        }
    }

    public static class EnumerableExtensions
    {
        public static bool Any(this IEnumerable e) => e.GetEnumerator().MoveNext();

        public static T FirstOrDefault<T>(this IEnumerable e) {
            var enumerator = e.GetEnumerator();
            if (enumerator.MoveNext()) {
                return (T)enumerator.Current;
            }

            return default;
        }
    }
}
