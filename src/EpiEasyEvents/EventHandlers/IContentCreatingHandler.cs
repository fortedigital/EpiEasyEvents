using EPiServer;
using EPiServer.Core;

namespace Forte.EpiEasyEvents.EventHandlers
{
    /// <summary>
    /// Content event args are either SaveContentEventArgs or CopyContentEventArgs, depending on scenario
    /// </summary>
    public interface IContentCreatingHandler<TContentType>: IContentEventHandler<TContentType, ContentEventArgs> where TContentType:IContent{}
}