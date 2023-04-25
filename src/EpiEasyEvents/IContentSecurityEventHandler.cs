using EPiServer.Core;
using EPiServer.DataAbstraction;

namespace Forte.EpiEasyEvents;

public interface IContentSecurityEventHandler<TContentType, TEventArgsType> : IContentChangedHandler<TContentType, TEventArgsType>
    where TEventArgsType : ContentSecurityEventArg where TContentType : IContentData
{
}
