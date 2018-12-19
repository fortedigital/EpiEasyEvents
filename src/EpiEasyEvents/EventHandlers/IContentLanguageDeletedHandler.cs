using EPiServer;
using EPiServer.Core;

namespace Forte.EpiEasyEvents.EventHandlers
{
    public interface IContentLanguageDeletedHandler<TContentType>: IContentEventHandler<TContentType, ContentLanguageEventArgs> where TContentType:IContent{}
}