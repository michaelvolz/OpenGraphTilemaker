using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Blazor;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;

namespace OpenGraphTilemaker.Web.Client.Features.Form
{
    public class FormModel : BlazorComponentStateful<FormModel>
    {
        protected Person Person => State.Person;

        protected string Form { get; private set; } = Revealed;
        protected string Success { get; private set; } = Hidden;
        protected string Error { get; private set; } = Hidden;

        private FormState State => Store.GetState<FormState>();

        private const string ThereIsStillSomethingWrong = "There is still something wrong!";
        private const string Revealed = "";
        private const string Hidden = "hide";

        protected async Task SubmitAsync() {
            if (HasError(Person)) {
                Error = Revealed;
                await JSRuntime.Current.InvokeAsync<bool>("blazorDemo.showAlert", ThereIsStillSomethingWrong);
                Log.LogInformation($"### {ThereIsStillSomethingWrong}");
            }
            else {
                Form = Hidden;
                Error = Hidden;
                Success = Revealed;
            }
        }

        protected string IsValid(Expression<Func<object>> property) => Person.IsValid<Person>(property, "is-invalid");

        protected void KeyPress(UIKeyboardEventArgs ev) => Log.LogInformation($"KeyPress: {ev.Key + ", " + ev.Code}");

        private bool HasError<T>(T obj) where T : class, IValidate => obj.HasError<T>();
    }
}
