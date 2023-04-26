using System;
using EPiServer.Core;

namespace Forte.EpiEasyEvents;

public interface IContentChangedHandler<TContentType, TEventArgsType> where TEventArgsType : EventArgs where TContentType : IContentData
{
    void Handle(TContentType content, TEventArgsType eventArgs);
}
