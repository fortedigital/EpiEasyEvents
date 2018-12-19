using EPiServer;
using EPiServer.Core;

namespace Forte.EpiEasyEvents.EventHandlers
{
    public interface IContentLanguageCreatedHandler<TContentType>: IContentEventHandler<TContentType, SaveContentEventArgs> where TContentType:IContent{}
}