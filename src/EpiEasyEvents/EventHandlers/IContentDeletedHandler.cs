using EPiServer;
using EPiServer.Core;

namespace Forte.EpiEasyEvents.EventHandlers
{
    public interface IContentDeletedHandler<TContentType>: IContentEventHandler<TContentType, MoveContentEventArgs> where TContentType:IContent{}
}