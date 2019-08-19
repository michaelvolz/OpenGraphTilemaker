using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Experiment.Features.App;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

namespace Experiment.Features.Form
{
    public class FormModel : BlazorComponentStateful<FormModel>
    {
        private const string ThereIsStillSomethingWrong = "There is still something wrong!";
        private const string Revealed = "";
        private const string Hidden = "hide";

        protected Person Person => State.Person;

        protected string Form { get; private set; } = Revealed;
        protected string Success { get; private set; } = Hidden;
        protected string Error { get; private set; } = Hidden;

        private FormState State => Store.GetState<FormState>();

        protected async Task SubmitAsync()
        {
            if (HasError(Person))
            {
                Error = Revealed;
                await JSRuntime.InvokeAsync<bool>("blazorDemo.showAlert", ThereIsStillSomethingWrong);
                Logger.LogInformation("### {ThereIsStillSomethingWrong}", ThereIsStillSomethingWrong);
            }
            else
            {
                Form = Hidden;
                Error = Hidden;
                Success = Revealed;
            }
        }

        protected string IsValid(Expression<Func<object>> property) => Person.IsValid<Person>(property, "is-invalid");

        protected void KeyPress(UIKeyboardEventArgs ev) =>
            Logger.LogInformation("KeyPress: {KeyAndCode}", ev.Key + ", " + ev.Code);

        private bool HasError<T>(T obj) where T : class, IValidate => obj.HasError<T>();
    }
}