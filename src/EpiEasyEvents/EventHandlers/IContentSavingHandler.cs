using EPiServer;
using EPiServer.Core;

namespace Forte.EpiEasyEvents.EventHandlers
{
    public interface IContentSavingHandler<TContentType>: IContentEventHandler<TContentType, SaveContentEventArgs> where TContentType:IContentData{}
}
