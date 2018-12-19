using EPiServer;
using EPiServer.Core;

namespace Forte.EpiEasyEvents.EventHandlers
{
    public interface IDefaultContentLoadedHandler<TContentType>: IContentEventHandler<TContentType, ContentEventArgs> where TContentType:IContent{}
}