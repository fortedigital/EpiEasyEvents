using EPiServer;
using EPiServer.Core;

namespace Forte.EpiEasyEvents.EventHandlers
{
    public interface IContentLanguageDeletingHandler<TContentType>: IContentEventHandler<TContentType, ContentLanguageEventArgs> where TContentType:IContentData{}
}
