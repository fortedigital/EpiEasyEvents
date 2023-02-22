using EPiServer;
using EPiServer.Core;

namespace Forte.EpiEasyEvents.EventHandlers
{
    public interface IContentRestoringHandler<TContentType>: IContentEventHandler<TContentType, MoveContentEventArgs> where TContentType:IContentData{}
}
