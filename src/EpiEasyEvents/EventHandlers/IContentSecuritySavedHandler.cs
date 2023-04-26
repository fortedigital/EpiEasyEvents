using EPiServer.Core;
using EPiServer.DataAbstraction;

namespace Forte.EpiEasyEvents.EventHandlers;

public interface IContentSecuritySavedHandler<TContentType>: IContentSecurityEventHandler<TContentType, ContentSecurityEventArg> where TContentType:IContentData{}
