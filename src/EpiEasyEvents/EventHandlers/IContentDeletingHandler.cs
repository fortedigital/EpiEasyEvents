using EPiServer;
using EPiServer.Core;

namespace Forte.EpiEasyEvents.EventHandlers
{
    public interface IContentDeletingHandler<TContentType>: IContentEventHandler<TContentType, MoveContentEventArgs> where TContentType:IContentData{}
}
