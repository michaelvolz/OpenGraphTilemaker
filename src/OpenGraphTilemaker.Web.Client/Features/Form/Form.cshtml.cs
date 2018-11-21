using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace OpenGraphTilemaker.Web.Client.Features.Form
{
    public class FormModel : BlazorComponentStateful
    {
        private const string ThereIsStillSomethingWrong = "There is still something wrong!";
        private const string Revealed = "revealed";
        private const string Hidden = "hidden";

        public Person Person => State.Person;
        public FormState State => Store.GetState<FormState>();

        public string FormVisibility { get; set; } = Revealed;
        public string SuccessVisibility { get; set; } = Hidden;
        public string ErrorVisibility { get; set; } = Hidden;

        protected async Task SubmitAsync() {
            if (!Person.HasError()) {
                FormVisibility = Hidden;
                ErrorVisibility = Hidden;
                SuccessVisibility = Revealed;
            }

            if (Person.HasError()) {
                ErrorVisibility = Revealed;
                await JSRuntime.Current.InvokeAsync<bool>("showAlert", ThereIsStillSomethingWrong);
                Console.WriteLine($"### {ThereIsStillSomethingWrong}");
            }
        }

        protected string IsValid(Expression<Func<object>> property) {
            return Person.IsValid(property);
        }
    }
}
