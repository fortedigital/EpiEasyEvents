using EPiServer;
using EPiServer.Core;

namespace Forte.EpiEasyEvents.EventHandlers
{
    public interface IContentSavedHandler<TContentType>: IContentEventHandler<TContentType, SaveContentEventArgs> where TContentType:IContentData{}
}
