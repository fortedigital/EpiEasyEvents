using EPiServer;
using EPiServer.Core;

namespace Forte.EpiEasyEvents.EventHandlers
{
    public interface IApprovalRequestedHandler<TContentType>: IContentEventHandler<TContentType, SaveContentEventArgs> where TContentType:IContent{}
}