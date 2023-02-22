using EPiServer;
using EPiServer.Core;

namespace Forte.EpiEasyEvents.EventHandlers
{
    public interface IContentLoadedHandler<TContentType>: IContentEventHandler<TContentType, ContentEventArgs> where TContentType:IContentData{}
}
