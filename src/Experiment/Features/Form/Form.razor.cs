using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Logging;

namespace Experiment.Features.Form
{
    public partial class Form
    {
        private const string ThereIsStillSomethingWrong = "There is still something wrong!";
        private const string Revealed = "";
        private const string Hidden = "hide";

        private Person Person => State.Person;

        private string FormTag { get; set; } = Revealed;
        private string Success { get; set; } = Hidden;
        private string Error { get; set; } = Hidden;

        private FormState State => Store.GetState<FormState>();

        private static bool HasError<T>(T obj)
            where T : class, IValidate => obj.HasError<T>();

        private async Task SubmitAsync()
        {
            if (HasError(Person))
            {
                Error = Revealed;
                await JSRuntime.InvokeAsync<bool>("blazorDemo.showAlert", new object[] { ThereIsStillSomethingWrong });
                Logger.LogInformation("### {ThereIsStillSomethingWrong}", ThereIsStillSomethingWrong);
            }
            else
            {
                FormTag = Hidden;
                Error = Hidden;
                Success = Revealed;
            }
        }

        private string IsValid(Expression<Func<object>> property) => Person.IsValid<Person>(property, "is-invalid");

        private void KeyPress(KeyboardEventArgs ev) =>
            Logger.LogInformation("KeyPress: {KeyAndCode}", ev.Key + ", " + ev.Code);
    }
}
