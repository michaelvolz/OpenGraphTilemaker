namespace OpenGraphTilemaker.Web.Client.Features.Form
{
    public class FormModel : BlazorComponentStateful
    {
        public FormState FormState => Store.GetState<FormState>();
    }
}
