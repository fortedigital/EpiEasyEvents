using EPiServer;
using EPiServer.Core;

namespace Forte.EpiEasyEvents.EventHandlers
{
    public interface IContentMovedHandler<TContentType>: IContentEventHandler<TContentType, MoveContentEventArgs> where TContentType:IContent{}
}