using EPiServer;
using EPiServer.Core;

namespace Forte.EpiEasyEvents.EventHandlers
{
    public interface IContentPermanentlyDeletingHandler<TContentType>: IContentEventHandler<TContentType, DeleteContentEventArgs> where TContentType:IContent{}
}