using EPiServer;
using EPiServer.Core;
using EPiServer.DataAccess;

namespace ForteDigital.EpiEasyEvents
{
    public abstract class ContentEventHandler<T> : IContentEventHandler
    {
        public void OnContentMoved(ContentReference originalParent, IContent content)
        {
            if (content is T page) this.OnContentMoved(page);
        }

        public void OnContentPublishing(IContent content)
        {
            if (content is T page) this.OnContentPublishing(page);
        }

        public void OnContentPublishing(IContent content, ContentEventArgs eventArgs)
        {
            if (content is T page) this.OnContentPublishing(page, eventArgs);
        }

        public void OnContentPublished(IContent content)
        {
            if (content is T page) this.OnContentPublished(page);
        }

        public void OnContentCreated(IContent content)
        {
            if (content is T page) this.OnContentCreated(page);
        }

        public void OnContentCreating(IContent content)
        {
            if (content is T page) this.OnContentCreating(page);
        }

        public void OnContentSaving(IContent content, SaveAction saveAction)
        {
            if (content is T page) this.OnContentSaving(page, saveAction);
        }

        public void OnContentSaved(IContent content)
        {
            if (content is T page) this.OnContentSaved(page);
        }

        public void OnContentDeleted(ContentReference parent, IContent content)
        {
            if (content is T page) this.OnContentDeleted(parent, page);
        }
        
        protected virtual void OnContentPublishing(T content){}

        protected virtual void OnContentPublishing(T content, ContentEventArgs eventArgs) { }

        protected virtual void OnContentMoved(T content){}
        
        protected virtual void OnContentPublished(T content) {}

        protected virtual void OnContentCreated(T content) { }

        protected virtual void OnContentCreating(T content) { }

        protected virtual void OnContentSaving(T content, SaveAction saveAction) { }
        
        protected virtual void OnContentSaved(T content) {}
        
        protected virtual void OnContentDeleted(ContentReference parent, T content) {}

    }
}
