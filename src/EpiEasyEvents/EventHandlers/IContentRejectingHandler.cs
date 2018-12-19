using EPiServer;
using EPiServer.Core;

namespace Forte.EpiEasyEvents.EventHandlers
{
    public interface IContentRejectingHandler<TContentType>: IContentEventHandler<TContentType, SaveContentEventArgs> where TContentType:IContent{}
}