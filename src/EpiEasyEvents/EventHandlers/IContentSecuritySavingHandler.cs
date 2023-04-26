using EPiServer.Core;
using EPiServer.DataAbstraction;

namespace Forte.EpiEasyEvents.EventHandlers;

public interface IContentSecuritySavingHandler<TContentType>: IContentSecurityEventHandler<TContentType, ContentSecurityCancellableEventArgs> where TContentType:IContentData{}
