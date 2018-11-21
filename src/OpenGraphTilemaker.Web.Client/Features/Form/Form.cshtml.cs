using System;
using System.Linq.Expressions;

namespace OpenGraphTilemaker.Web.Client.Features.Form
{
    public class FormModel : BlazorComponentStateful
    {
        public Person Person => State.Person;
        public FormState State => Store.GetState<FormState>();

        public string FormVisibility { get; set; } = "revealed";
        public string SuccessVisibility { get; set; } = "hidden";
        public string ErrorVisibility { get; set; } = "hidden";

        protected void Submit() {
            if (!Person.HasError()) {
                FormVisibility = "hidden";
                ErrorVisibility = "hidden";
                SuccessVisibility = "revealed";
            }

            if (Person.HasError()) {
                ErrorVisibility = "revealed";
            }
        }

        protected string IsValid(Expression<Func<object>> property) {
            return Person.IsValid(property);
        }
    }
}
