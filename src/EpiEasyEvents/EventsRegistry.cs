using System;
using System.Collections.Generic;
using System.Linq;
using EPiServer;
using EPiServer.Core;
using Forte.EpiEasyEvents.EventHandlers;
using Microsoft.Extensions.DependencyInjection;

namespace Forte.EpiEasyEvents
{
    internal class EventsRegistry
    {
        private readonly IContentEvents _contentEvents;
        private readonly IServiceProvider _services;

        public EventsRegistry(IContentEvents contentEvents, IServiceProvider services)
        {
            _contentEvents = contentEvents;
            _services = services;
        }

        public void RegisterEvents()
        {
             _contentEvents.CheckedInContent += HandleCheckedInContent;
             _contentEvents.CheckingInContent += HandleCheckingInContent;

             _contentEvents.CheckedOutContent += HandleCheckedOutContent;
             _contentEvents.CheckingOutContent += HandleCheckingOutContent;

             _contentEvents.CreatedContentLanguage += HandleCreatedContentLanguage;
             _contentEvents.CreatingContentLanguage += HandleCreatingContentLanguage;

             _contentEvents.CreatedContent += HandleCreatedContent;
             _contentEvents.CreatingContent += HandleCreatingContent;

             _contentEvents.DeletedContent += HandleDeletedContent;
             _contentEvents.DeletingContent += HandleDeletingContent;

             _contentEvents.DeletedContentLanguage += HandleDeletedContentLanguage;
             _contentEvents.DeletingContentLanguage += HandleDeletingContentLanguage;

             _contentEvents.LoadedContent += HandleLoadedContent;

             _contentEvents.LoadedDefaultContent += HandleLoadedDefaultContent;

             _contentEvents.MovingContent += HandleMovingContent;
             _contentEvents.MovedContent += HandleMovedContent;

             _contentEvents.PublishingContent += HandlePublishingContent;
             _contentEvents.PublishedContent += HandlePublishedContent;

             _contentEvents.RejectedContent += HandleRejectedContent;
             _contentEvents.RejectingContent += HandleRejectingContent;

             _contentEvents.RequestedApproval += HandleRequestedApproval;
             _contentEvents.RequestingApproval += HandleRequestingApproval;

             _contentEvents.SavingContent += HandleSavingContent;
             _contentEvents.SavedContent += HandleSavedContent;

             _contentEvents.ScheduledContent += HandleScheduledContent;
             _contentEvents.SchedulingContent += HandleSchedulingContent;
        }

        private void HandleCheckedInContent(object sender, ContentEventArgs eventArgs)
        {
            HandleEvent(typeof(IContentCheckedInHandler<>), eventArgs);
        }

        private void HandleCheckingInContent(object sender, ContentEventArgs eventArgs)
        {
            HandleEvent(typeof(IContentCheckingInHandler<>), eventArgs);
        }

        private void HandleCheckedOutContent(object sender, ContentEventArgs eventArgs)
        {
            HandleEvent(typeof(IContentCheckedOutHandler<>), eventArgs);
        }

        private void HandleCheckingOutContent(object sender, ContentEventArgs eventArgs)
        {
            HandleEvent(typeof(IContentCheckingOutHandler<>), eventArgs);
        }

        private void HandleCreatedContentLanguage(object sender, ContentEventArgs eventArgs)
        {
            HandleEvent(typeof(IContentLanguageCreatedHandler<>), eventArgs);
        }

        private void HandleCreatingContentLanguage(object sender, ContentEventArgs eventArgs)
        {
            HandleEvent(typeof(IContentLanguageCreatingHandler<>), eventArgs);
        }

        private void HandleCreatedContent(object sender, ContentEventArgs eventArgs)
        {
            HandleEvent(typeof(IContentCreatedHandler<>), eventArgs);
        }

        private void HandleCreatingContent(object sender, ContentEventArgs eventArgs)
        {
            HandleEvent(typeof(IContentCreatingHandler<>), eventArgs);
        }

        private void HandleDeletedContent(object sender, ContentEventArgs eventArgs)
        {
            HandleEvent(typeof(IContentPermanentlyDeletedHandler<>), eventArgs);
        }

        private void HandleDeletingContent(object sender, ContentEventArgs eventArgs)
        {
            HandleEvent(typeof(IContentPermanentlyDeletingHandler<>), eventArgs);
        }

        private void HandleDeletedContentLanguage(object sender, ContentEventArgs eventArgs)
        {
            HandleEvent(typeof(IContentLanguageDeletedHandler<>), eventArgs);
        }

        private void HandleDeletingContentLanguage(object sender, ContentEventArgs eventArgs)
        {
            HandleEvent(typeof(IContentLanguageDeletingHandler<>), eventArgs);
        }

        private void HandleLoadedContent(object sender, ContentEventArgs eventArgs)
        {
            HandleEvent(typeof(IContentLoadedHandler<>), eventArgs);
        }

        private void HandleLoadedDefaultContent(object sender, ContentEventArgs eventArgs)
        {
            HandleEvent(typeof(IDefaultContentLoadedHandler<>), eventArgs);
        }

        private void HandlePublishingContent(object sender, ContentEventArgs eventArgs)
        {
            HandleEvent(typeof(IContentPublishingHandler<>), eventArgs);
        }

        private void HandlePublishedContent(object sender, ContentEventArgs eventArgs)
        {
            HandleEvent(typeof(IContentPublishedHandler<>), eventArgs);
        }

        private void HandleRejectedContent(object sender, ContentEventArgs eventArgs)
        {
            HandleEvent(typeof(IContentRejectedHandler<>), eventArgs);
        }

        private void HandleRejectingContent(object sender, ContentEventArgs eventArgs)
        {
            HandleEvent(typeof(IContentRejectingHandler<>), eventArgs);
        }

        private void HandleRequestedApproval(object sender, ContentEventArgs eventArgs)
        {
            HandleEvent(typeof(IApprovalRequestedHandler<>), eventArgs);
        }

        private void HandleRequestingApproval(object sender, ContentEventArgs eventArgs)
        {
            HandleEvent(typeof(IApprovalRequestingHandler<>), eventArgs);
        }

        private void HandleSavingContent(object sender, ContentEventArgs eventArgs)
        {
            HandleEvent(typeof(IContentSavingHandler<>), eventArgs);
        }

        private void HandleSavedContent(object sender, ContentEventArgs eventArgs)
        {
            HandleEvent(typeof(IContentSavedHandler<>), eventArgs);
        }

        private void HandleScheduledContent(object sender, ContentEventArgs eventArgs)
        {
            HandleEvent(typeof(IContentScheduledHandler<>), eventArgs);
        }

        private void HandleSchedulingContent(object sender, ContentEventArgs eventArgs)
        {
            HandleEvent(typeof(IContentSchedulingHandler<>), eventArgs);
        }

        private void HandleMovedContent(object sender, ContentEventArgs e)
        {
            var args = e as MoveContentEventArgs;
            if (e.TargetLink == ContentReference.WasteBasket)
            {
                HandleEvent(typeof(IContentDeletedHandler<>), e);
            }
            else if (args.OriginalParent == ContentReference.WasteBasket)
            {
                HandleEvent(typeof(IContentRestoredHandler<>), e);
            }
            else
            {
                HandleEvent(typeof(IContentMovedHandler<>), e);
            }
        }

        private void HandleMovingContent(object sender, ContentEventArgs e)
        {
            var args = e as MoveContentEventArgs;
            if (e.TargetLink == ContentReference.WasteBasket)
            {
                HandleEvent(typeof(IContentDeletingHandler<>), e);
            }
            else if (args.OriginalParent == ContentReference.WasteBasket)
            {
                HandleEvent(typeof(IContentRestoringHandler<>), e);
            }
            else
            {
                HandleEvent(typeof(IContentMovingHandler<>), e);
            }
        }

        private void HandleEvent<TEventArgs>(Type handlerInterface, TEventArgs eventArgs)
            where TEventArgs : ContentEventArgs
        {
            if (EventsHandlerScopeConfiguration.IsHandlingDisabled)
            {
                return;
            }

            var pageType = eventArgs.Content?.GetType() ?? typeof(IContent);
            var eventHandlers = GetAllEventHandlers(pageType, handlerInterface);

            foreach (var handler in eventHandlers)
            {
                var handleMethod = handler.GetType()
                    .GetMethod(nameof(IContentEventHandler<PageData, ContentEventArgs>.Handle));
                handleMethod?.Invoke(handler, new object[] {eventArgs.Content, eventArgs});
            }
        }

        private IEnumerable<object> GetAllEventHandlers(Type pageType, Type eventGenericInterface)
        {
            var handledContentTypes = pageType.GetInterfaces()
                .Concat(GetInheritanceHierarchy(pageType))
                .Where(type => typeof(IContent).IsAssignableFrom(type));
            var handlers = Enumerable.Empty<object>();
            foreach (var handledType in handledContentTypes)
            {
                var handlerInterface = eventGenericInterface.MakeGenericType(handledType);
                var currentHandlers = _services.GetServices(handlerInterface).ToList();
                if (currentHandlers.Any())
                {
                    handlers = handlers.Concat(currentHandlers);
                }
            }

            return handlers;
        }

        private static IEnumerable<Type> GetInheritanceHierarchy(Type pageType)
        {
            while (pageType != null)
            {
                yield return pageType;
                pageType = pageType.BaseType;
            }
        }
    }
}