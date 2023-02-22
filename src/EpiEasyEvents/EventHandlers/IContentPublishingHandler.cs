using EPiServer;
using EPiServer.Core;

namespace Forte.EpiEasyEvents.EventHandlers
{
    public interface IContentPublishingHandler<TContentType>: IContentEventHandler<TContentType, SaveContentEventArgs> where TContentType:IContentData{}
}
