//using BlazorState;
//using MediatR;
//using Microsoft.AspNetCore.Blazor.Components;
//
//// ReSharper disable MemberCanBeProtected.Global
//
//namespace OpenGraphTilemakerWeb.App.Pages.Counter
//{
//    public class CounterModel : BlazorComponent, IBlazorStateComponent
//    {
//        public CounterState CounterState => Store.GetState<CounterState>();
//
//        [Inject] public IMediator Mediator { get; set; }
//        [Inject] public IStore Store { get; set; }
//
//        public void ButtonClick()
//        {
//            var incrementCounterRequest = new IncrementCounterRequest {Amount = 2};
//            Mediator.Send(incrementCounterRequest);
//        }
//    }
//}