namespace OpenGraphTilemaker.Web.Client.Features.Form
{
    public class FormModel : BlazorComponentStateful
    {
        public Person Person => Store.GetState<FormState>().Person;
        public FormState FormState => Store.GetState<FormState>();
    }
}
