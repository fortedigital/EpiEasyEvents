using EPiServer;
using EPiServer.Core;

namespace Forte.EpiEasyEvents.EventHandlers
{
    public interface IContentMovingHandler<TContentType>: IContentEventHandler<TContentType, MoveContentEventArgs> where TContentType:IContentData{}
}
