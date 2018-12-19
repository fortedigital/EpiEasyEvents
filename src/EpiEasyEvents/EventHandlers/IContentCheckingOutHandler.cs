using EPiServer;
using EPiServer.Core;

namespace Forte.EpiEasyEvents.EventHandlers
{
    public interface IContentCheckingOutHandler<TContentType>: IContentEventHandler<TContentType, SaveContentEventArgs> where TContentType:IContent{}
}