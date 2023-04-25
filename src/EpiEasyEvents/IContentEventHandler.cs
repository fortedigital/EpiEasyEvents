using EPiServer;
using EPiServer.Core;

namespace Forte.EpiEasyEvents
{
    public interface IContentEventHandler<TContentType, TEventArgsType> : IContentChangedHandler<TContentType, TEventArgsType>
        where TEventArgsType : ContentEventArgs where TContentType : IContentData
    {
    }
}
