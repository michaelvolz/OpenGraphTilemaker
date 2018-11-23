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

        public Person Person => State.Person;
        public FormState State => Store.GetState<FormState>();

        public string FormVisibility { get; set; } = Revealed;
        public string SuccessVisibility { get; set; } = Hidden;
        public string ErrorVisibility { get; set; } = Hidden;

        protected async Task SubmitAsync() {
            if (!HasError(Person)) {
                FormVisibility = Hidden;
                ErrorVisibility = Hidden;
                SuccessVisibility = Revealed;
            }

            if (HasError(Person)) {
                ErrorVisibility = Revealed;
                await JSRuntime.Current.InvokeAsync<bool>("blazorDemo.showAlert", ThereIsStillSomethingWrong);
                Console.WriteLine($"### {ThereIsStillSomethingWrong}");
            }
        }

        protected bool HasError<T>(T obj) where T : class, IValidate {
            return obj.HasError<T>();
        }

        protected string IsValidTag(Expression<Func<object>> property) {
            return Person.IsValid<Person>(property, "is-invalid");
        }

        protected string IsValidLabel(Expression<Func<object>> property) {
            return Person.IsValid<Person>(property, "xis-invalid-label");
        }

        protected string IsValidInput(Expression<Func<object>> property) {
            return Person.IsValid<Person>(property, "xis-invalid-input");
        }

        protected void KeyPress(UIKeyboardEventArgs ev) => Console.WriteLine($"KeyPress: { ev.Key + ", " + ev.Code }");
    }
}
