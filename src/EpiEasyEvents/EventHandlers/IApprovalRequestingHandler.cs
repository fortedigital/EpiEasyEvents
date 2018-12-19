using EPiServer;
using EPiServer.Core;

namespace Forte.EpiEasyEvents.EventHandlers
{
    public interface IApprovalRequestingHandler<TContentType>: IContentEventHandler<TContentType, SaveContentEventArgs> where TContentType:IContent{}
}