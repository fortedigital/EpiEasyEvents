using EPiServer;
using EPiServer.Core;

namespace Forte.EpiEasyEvents.EventHandlers
{
    public interface IContentRestoredHandler<TContentType>: IContentEventHandler<TContentType, MoveContentEventArgs> where TContentType:IContentData{}
}
