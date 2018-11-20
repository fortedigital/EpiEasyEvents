using EPiServer;
using EPiServer.Core;
using EPiServer.DataAccess;

namespace ForteDigital.EpiEasyEvents
{
    public interface IContentEventHandler
    {
        void OnContentMoved(ContentReference originalParent, IContent content);
        void OnContentPublishing(IContent content);
        void OnContentPublishing(IContent content, ContentEventArgs eventArgs);
        void OnContentPublished(IContent content);
        void OnContentCreated(IContent content);
        void OnContentCreating(IContent content);
        void OnContentSaving(IContent content, SaveAction saveAction);
        void OnContentSaved(IContent content);
        void OnContentDeleted(ContentReference parent, IContent content);
    }
}
