using System;
using System.Linq.Expressions;

namespace OpenGraphTilemaker.Web.Client.Features.Form
{
    public class FormModel : BlazorComponentStateful
    {
        public Person Person => State.Person;
        public FormState State => Store.GetState<FormState>();

        protected string IsValid(Expression<Func<object>> property) {
            return Person.IsValid(property);
        }
    }
}
