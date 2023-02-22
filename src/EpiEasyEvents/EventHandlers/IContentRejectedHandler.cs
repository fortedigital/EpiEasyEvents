using EPiServer;
using EPiServer.Core;

namespace Forte.EpiEasyEvents.EventHandlers
{
    public interface IContentRejectedHandler<TContentType>: IContentEventHandler<TContentType, SaveContentEventArgs> where TContentType:IContentData{}
}
