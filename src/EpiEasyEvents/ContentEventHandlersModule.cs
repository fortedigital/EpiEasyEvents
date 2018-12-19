using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EPiServer;
using EPiServer.Core;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using Forte.EpiEasyEvents.EventHandlers;
using StructureMap;
using StructureMap.Graph;
using StructureMap.Graph.Scanning;
using StructureMap.TypeRules;

namespace Forte.EpiEasyEvents
{
    public abstract class ContentEventHandlersModule : IInitializableModule, IConfigurableModule
    {
        private readonly Assembly[] eventHandlersAssemblies;
        private IServiceLocator serviceLocator;

        protected ContentEventHandlersModule(params Assembly[] eventHandlersAssemblies)
        {
            this.eventHandlersAssemblies = eventHandlersAssemblies;
        }

        public void Initialize(InitializationEngine context)
        {
            this.serviceLocator = context.Locate.Advanced;

            context.Locate.ContentEvents().CheckedInContent += HandleCheckedInContent;
            context.Locate.ContentEvents().CheckingInContent += HandleCheckingInContent;

            context.Locate.ContentEvents().CheckedOutContent += HandleCheckedOutContent;
            context.Locate.ContentEvents().CheckingOutContent += HandleCheckingOutContent;

            context.Locate.ContentEvents().CreatedContentLanguage += HandleCreatedContentLanguage;
            context.Locate.ContentEvents().CreatingContentLanguage += HandleCreatingContentLanguage;

            context.Locate.ContentEvents().CreatedContent += HandleCreatedContent;
            context.Locate.ContentEvents().CreatingContent += HandleCreatingContent;

            context.Locate.ContentEvents().DeletedContent += HandleDeletedContent;
            context.Locate.ContentEvents().DeletingContent += HandleDeletingContent;

            context.Locate.ContentEvents().DeletedContentLanguage += HandleDeletedContentLanguage;
            context.Locate.ContentEvents().DeletingContentLanguage += HandleDeletingContentLanguage;

            context.Locate.ContentEvents().LoadedContent += HandleLoadedContent;

            context.Locate.ContentEvents().LoadedDefaultContent += HandleLoadedDefaultContent;

            context.Locate.ContentEvents().MovingContent += HandleMovingContent;
            context.Locate.ContentEvents().MovedContent += HandleMovedContent;

            context.Locate.ContentEvents().PublishingContent += HandlePublishingContent;
            context.Locate.ContentEvents().PublishedContent += HandlePublishedContent;

            context.Locate.ContentEvents().RejectedContent += HandleRejectedContent;
            context.Locate.ContentEvents().RejectingContent += HandleRejectingContent;

            context.Locate.ContentEvents().RequestedApproval += HandleRequestedApproval;
            context.Locate.ContentEvents().RequestingApproval += HandleRequestingApproval;

            context.Locate.ContentEvents().SavingContent += HandleSavingContent;
            context.Locate.ContentEvents().SavedContent += HandleSavedContent;

            context.Locate.ContentEvents().ScheduledContent += HandleScheduledContent;
            context.Locate.ContentEvents().SchedulingContent += HandleSchedulingContent;
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
            var args = (e as MoveContentEventArgs);
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
            var args = (e as MoveContentEventArgs);
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
            var pageType = eventArgs.Content?.GetType() ?? typeof(IContent);
            var eventHandlers = GetAllEventHandlers(pageType, handlerInterface);

            foreach (var handler in eventHandlers)
            {
                var handleMethod = handler.GetType()
                    .GetMethod(nameof(IContentEventHandler<PageData, ContentEventArgs>.Handle));
                handleMethod.Invoke(handler, new object[] {eventArgs.Content, eventArgs});
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
                var currentHandlers = this.serviceLocator
                    .GetAllInstances(handlerInterface);
                if (currentHandlers.Any())
                {
                    handlers = handlers.Concat(currentHandlers);
                }
            }

            return handlers;
        }

        private IEnumerable<Type> GetInheritanceHierarchy(Type pageType)
        {
            while (pageType != null)
            {
                yield return pageType;
                pageType = pageType.BaseType;
            }
        }

        public void Uninitialize(InitializationEngine context)
        {
            context.Locate.ContentEvents().CheckedInContent -= HandleCheckedInContent;
            context.Locate.ContentEvents().CheckingInContent -= HandleCheckingInContent;

            context.Locate.ContentEvents().CheckedOutContent -= HandleCheckedOutContent;
            context.Locate.ContentEvents().CheckingOutContent -= HandleCheckingOutContent;

            context.Locate.ContentEvents().CreatedContentLanguage -= HandleCreatedContentLanguage;
            context.Locate.ContentEvents().CreatingContentLanguage -= HandleCreatingContentLanguage;

            context.Locate.ContentEvents().CreatedContent -= HandleCreatedContent;
            context.Locate.ContentEvents().CreatingContent -= HandleCreatingContent;

            context.Locate.ContentEvents().DeletedContent -= HandleDeletedContent;
            context.Locate.ContentEvents().DeletingContent -= HandleDeletingContent;

            context.Locate.ContentEvents().DeletedContentLanguage -= HandleDeletedContentLanguage;
            context.Locate.ContentEvents().DeletingContentLanguage -= HandleDeletingContentLanguage;

            context.Locate.ContentEvents().LoadedContent -= HandleLoadedContent;

            context.Locate.ContentEvents().LoadedDefaultContent -= HandleLoadedDefaultContent;

            context.Locate.ContentEvents().MovingContent -= HandleMovingContent;
            context.Locate.ContentEvents().MovedContent -= HandleMovedContent;

            context.Locate.ContentEvents().PublishingContent -= HandlePublishingContent;
            context.Locate.ContentEvents().PublishedContent -= HandlePublishedContent;

            context.Locate.ContentEvents().RejectedContent -= HandleRejectedContent;
            context.Locate.ContentEvents().RejectingContent -= HandleRejectingContent;

            context.Locate.ContentEvents().RequestedApproval -= HandleRequestedApproval;
            context.Locate.ContentEvents().RequestingApproval -= HandleRequestingApproval;

            context.Locate.ContentEvents().SavingContent -= HandleSavingContent;
            context.Locate.ContentEvents().SavedContent -= HandleSavedContent;

            context.Locate.ContentEvents().ScheduledContent -= HandleScheduledContent;
            context.Locate.ContentEvents().SchedulingContent -= HandleSchedulingContent;
        }

        public void ConfigureContainer(ServiceConfigurationContext context)
        {
            context.StructureMap().Configure(c => c.Scan(a =>
            {
                foreach (var assembly in this.eventHandlersAssemblies)
                {
                    a.Assembly(assembly);
                }
                a.Convention<AllEventHandlersConvention>();

            }));
        }

        private class AllEventHandlersConvention : IRegistrationConvention
        {
            public void ScanTypes(TypeSet types, Registry registry)
            {
                // Only work on concrete types
                var typesToRegister = types.FindTypes(TypeClassification.Concretes | TypeClassification.Closed)
                    .Where(type => type.GetInterfaces().Any(x=>x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IContentEventHandler<,>)));
                foreach (var typeToRegister in typesToRegister)
                {
                    
                    // Register against all the interfaces implemented
                    // by this concrete class
                    var interfacesToRegister = typeToRegister.GetInterfaces();
                    foreach (var interfaceToRegister in interfacesToRegister)
                    {
                        registry.For(interfaceToRegister).Use(typeToRegister);
                    }
                }
            }
        }
    }
}
