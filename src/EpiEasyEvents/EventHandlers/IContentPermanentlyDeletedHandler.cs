using EPiServer;
using EPiServer.Core;

namespace Forte.EpiEasyEvents.EventHandlers
{
    public interface IContentPermanentlyDeletedHandler<TContentType>: IContentEventHandler<TContentType, DeleteContentEventArgs> where TContentType:IContentData{}
}
