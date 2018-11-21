using EPiServer.Core;
using EPiServer.DataAccess;

namespace ForteDigital.EpiEasyEvents
{
    public interface IContentEventHandler
    {
        void OnContentMoved(ContentReference originalParent, IContent content);
        void OnContentMoving(ContentReference originalParent, IContent content);
        void OnContentPublishing(IContent content);
        void OnContentPublished(IContent content);
        void OnContentCreated(IContent content);
        void OnContentCreating(IContent content);
        void OnContentSaving(IContent content, SaveAction saveAction);
        void OnContentSaved(IContent content);
        void OnContentDeleted(IContent content);
        void OnContentSheduling(IContent content);
        void OnContentSheduled(IContent content);
        void OnContentApprovalRequesting(IContent content);
        void OnContentApprovalRequested(IContent content);
        void OnContentRejecting(IContent content);
        void OnContentRejected(IContent content);
        void OnContentLoadFailed(IContent content);
        void OnDefaultContentLoaded(IContent content);
        void OnDefaultContentLoading(IContent content);
        void OnContentLoaded(IContent content);
        void OnContentLoading(IContent content);
        void OnContentVersionDeleting(IContent content);
        void OnContentVersionDeleted(IContent content);
        void OnContentLanguageDeleting(IContent content);
        void OnContentLanguageDeleted(IContent content);
        void OnContentDeleting(IContent content);
        void OnContentLanguageCreating(IContent content);
        void OnContentLanguageCreated(IContent content);
        void OnContentCheckingOut(IContent content);
        void OnContentCheckedOut(IContent content);
        void OnContentCheckingIn(IContent content);
        void OnContentCheckedIn(IContent content);
        void OnContentRestored(ContentReference target, IContent content);
        void OnContentRestoring(ContentReference target, IContent content);
        void OnContentPermanentlyDeleting(IContent content);
        void OnContentPermanentlyDeleted(IContent content);
    }
}
