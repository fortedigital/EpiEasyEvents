using EPiServer;
using EPiServer.Core;
using EPiServer.DataAccess;

namespace ForteDigital.EpiEasyEvents
{
    public abstract class ContentEventHandler<T> : IContentEventHandler
    {
        public void OnContentMoved(ContentReference originalParent, IContent content)
        {
            if (content is T page) this.OnContentMoved(page, originalParent);
        }

        public void OnContentMoving(ContentReference originalParent, IContent content)
        {
            if (content is T page) this.OnContentMoving(page, originalParent);
        }

        public void OnContentPublishing(IContent content)
        {
            if (content is T page) this.OnContentPublishing(page);
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

        public void OnContentDeleted(IContent content)
        {
            if (content is T page) this.OnContentDeleted(page);
        }

        public void OnContentSheduling(IContent content)
        {
            if (content is T page) this.OnContentSheduling(page);
        }

        public void OnContentSheduled(IContent content)
        {
            if (content is T page) this.OnContentSheduled(page);
        }

        public void OnContentApprovalRequesting(IContent content)
        {
            if (content is T page) this.OnContentApprovalRequesting(page);
        }

        public void OnContentApprovalRequested(IContent content)
        {
            if (content is T page) this.OnContentApprovalRequested(page);
        }

        public void OnContentRejecting(IContent content)
        {
            if (content is T page) this.OnContentRejecting(page);
        }

        public void OnContentRejected(IContent content)
        {
            if (content is T page) this.OnContentRejected(page);
        }

        public void OnContentLoadFailed(IContent content)
        {
            if (content is T page) this.OnContentLoadFailed(page);
        }

        public void OnDefaultContentLoaded(IContent content)
        {
            if (content is T page) this.OnDefaultContentLoaded(page);
        }

        public void OnDefaultContentLoading(IContent content)
        {
            if (content is T page) this.OnDefaultContentLoading(page);
        }

        public void OnContentLoaded(IContent content)
        {
            if (content is T page) this.OnContentLoaded(page);
        }

        public void OnContentLoading(IContent content)
        {
            if (content is T page) this.OnContentLoading(page);
        }

        public void OnContentVersionDeleting(IContent content)
        {
            if (content is T page) this.OnContentVersionDeleting(page);
        }

        public void OnContentVersionDeleted(IContent content)
        {
            if (content is T page) this.OnContentVersionDeleted(page);
        }

        public void OnContentLanguageDeleting(IContent content)
        {
            if (content is T page) this.OnContentLanguageDeleting(page);
        }

        public void OnContentLanguageDeleted(IContent content)
        {
            if (content is T page) this.OnContentLanguageDeleted(page);
        }

        public void OnContentDeleting(IContent content)
        {
            if (content is T page) this.OnContentDeleting(page);
        }

        public void OnContentLanguageCreating(IContent content)
        {
            if (content is T page) this.OnContentLanguageCreating(page);
        }

        public void OnContentLanguageCreated(IContent content)
        {
            if (content is T page) this.OnContentLanguageCreated(page);
        }

        public void OnContentCheckingOut(IContent content)
        {
            if (content is T page) this.OnContentCheckingOut(page);
        }

        public void OnContentCheckedOut(IContent content)
        {
            if (content is T page) this.OnContentCheckedOut(page);
        }

        public void OnContentCheckingIn(IContent content)
        {
            if (content is T page) this.OnContentCheckingIn(page);
        }

        public void OnContentCheckedIn(IContent content)
        {
            if (content is T page) this.OnContentCheckedIn(page);
        }

        public void OnContentRestored(ContentReference target, IContent content)
        {
            if (content is T page) this.OnContentRestored(page, target);
        }

        public void OnContentRestoring(ContentReference target, IContent content)
        {
            if (content is T page) this.OnContentRestoring(page, target);
        }

        public void OnContentPermanentlyDeleting(IContent content)
        {
            if (content is T page) this.OnContentPermanentlyDeleting(page);
        }

        public void OnContentPermanentlyDeleted(IContent content)
        {
            if (content is T page) this.OnContentPermanentlyDeleted(page);
        }

        protected virtual void OnContentPermanentlyDeleted(T content) { }

        protected virtual void OnContentPublishing(T content) { }

        protected virtual void OnContentMoved(T content, ContentReference originalParent) { }
        
        protected virtual void OnContentPublished(T content) { }

        protected virtual void OnContentCreated(T content) { }

        protected virtual void OnContentCreating(T content) { }

        protected virtual void OnContentSaving(T content, SaveAction saveAction) { }
        
        protected virtual void OnContentSaved(T content) {}
        
        protected virtual void OnContentSheduling(T content) {}
        
        protected virtual void OnContentSheduled(T content) {}

        protected virtual void OnContentApprovalRequesting(T content) {}

        protected virtual void OnContentApprovalRequested(T content) {}

        protected virtual void OnContentRejecting(T content) {}

        protected virtual void OnContentRejected(T content) {}

        protected virtual void OnContentLoadFailed(T content) {}

        protected virtual void OnDefaultContentLoaded(T content) {}

        protected virtual void OnDefaultContentLoading(T content) {}

        protected virtual void OnContentLoaded(T content) {}

        protected virtual void OnContentLoading(T content) {}

        protected virtual void OnContentVersionDeleting(T content) {}

        protected virtual void OnContentVersionDeleted(T content) {}

        protected virtual void OnContentLanguageDeleting(T content) {}

        protected virtual void OnContentLanguageDeleted(T content) {}

        protected virtual void OnContentDeleting(T content) {}

        protected virtual void OnContentLanguageCreating(T content) {}

        protected virtual void OnContentLanguageCreated(T content) {}

        protected virtual void OnContentCheckingOut(T content) {}

        protected virtual void OnContentCheckedOut(T content) {}

        protected virtual void OnContentCheckingIn(T content) {}

        protected virtual void OnContentCheckedIn(T content) {}
        
        protected virtual void OnContentPermanentlyDeleting(T content) {}
        
        protected virtual void OnContentRestoring(T content, ContentReference target) {}
        
        protected virtual void OnContentRestored(T content, ContentReference target) {}
        
        protected virtual void OnContentDeleted(T content) {}
        
        protected virtual void OnContentMoving(T content, ContentReference originalParent) {}
        

    }
}
