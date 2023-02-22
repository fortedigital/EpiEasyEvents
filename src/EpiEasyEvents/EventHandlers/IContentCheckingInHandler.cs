using EPiServer;
using EPiServer.Core;

namespace Forte.EpiEasyEvents.EventHandlers
{
    public interface IContentCheckingInHandler<TContentType>: IContentEventHandler<TContentType, SaveContentEventArgs> where TContentType:IContentData{}
}
