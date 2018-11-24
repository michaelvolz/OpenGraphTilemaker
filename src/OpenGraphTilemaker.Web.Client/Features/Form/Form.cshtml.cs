using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Blazor;
using Microsoft.JSInterop;

namespace OpenGraphTilemaker.Web.Client.Features.Form
{
    public class FormModel : BlazorComponentStateful
    {
        private const string ThereIsStillSomethingWrong = "There is still something wrong!";
        private const string Revealed = "";
        private const string Hidden = "hide";

        protected Person Person => State.Person;
        private FormState State => Store.GetState<FormState>();

        protected string Form { get; private set; } = Revealed;
        protected string Success { get; private set; } = Hidden;
        protected string Error { get; private set; } = Hidden;

        protected async Task SubmitAsync() {
            if (HasError(Person)) {
                Error = Revealed;
                await JSRuntime.Current.InvokeAsync<bool>("blazorDemo.showAlert", ThereIsStillSomethingWrong);
                Console.WriteLine($"### {ThereIsStillSomethingWrong}");
            }
            else {
                Form = Hidden;
                Error = Hidden;
                Success = Revealed;
            }
        }

        private bool HasError<T>(T obj) where T : class, IValidate {
            return obj.HasError<T>();
        }

        protected string IsValid(Expression<Func<object>> property) {
            return Person.IsValid<Person>(property, "is-invalid");
        }

        protected void KeyPress(UIKeyboardEventArgs ev) => Console.WriteLine($"KeyPress: {ev.Key + ", " + ev.Code}");
    }
}
