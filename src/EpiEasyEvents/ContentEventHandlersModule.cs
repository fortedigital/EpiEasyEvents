using System;
using System.Collections.Generic;
using System.Reflection;
using EPiServer;
using EPiServer.Core;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;

namespace ForteDigital.EpiEasyEvents
{
    public abstract class ContentEventHandlersModule : IInitializableModule, IConfigurableModule
    {
        private IEnumerable<IContentEventHandler> contentEventHandlers;
        private readonly Assembly[] eventHandlersAssemblies;

        protected ContentEventHandlersModule(params Assembly[] eventHandlersAssemblies)
        {
            this.eventHandlersAssemblies = eventHandlersAssemblies;
        }
        
        public void Initialize(InitializationEngine context)
        {
            this.contentEventHandlers = context.Locate.Advanced.GetAllInstances<IContentEventHandler>();

            context.Locate.ContentEvents().CheckedInContent +=  ModelModule_CheckedInContent;
            context.Locate.ContentEvents().CheckingInContent +=  ModelModule_CheckingInContent;
            
            context.Locate.ContentEvents().CheckedOutContent +=  ModelModule_CheckedOutContent;
            context.Locate.ContentEvents().CheckingOutContent +=  ModelModule_CheckingOutContent;
            
            context.Locate.ContentEvents().CreatedContentLanguage +=  ModelModule_CreatedContentLanguage;
            context.Locate.ContentEvents().CreatingContentLanguage +=  ModelModule_CreatingContentLanguage;
            
            context.Locate.ContentEvents().CreatedContent += ModelModule_CreatedContent;
            context.Locate.ContentEvents().CreatingContent += ModelModule_CreatingContent;

            context.Locate.ContentEvents().DeletedContent += ModelModule_DeletedContent;
            context.Locate.ContentEvents().DeletingContent += ModelModule_DeletingContent;

            context.Locate.ContentEvents().DeletedContentLanguage+=  ModelModule_DeletedContentLanguage;
            context.Locate.ContentEvents().DeletingContentLanguage+=  ModelModule_DeletingContentLanguage;
            
            context.Locate.ContentEvents().DeletedContentVersion+=  ModelModule_DeletedContentVersion;
            context.Locate.ContentEvents().DeletingContentVersion+=  ModelModule_DeletingContentVersion;
            
            context.Locate.ContentEvents().LoadingContent += ModelModule_LoadingContent;
            context.Locate.ContentEvents().LoadedContent += ModelModule_LoadedContent;
            
            context.Locate.ContentEvents().LoadingDefaultContent += ModelModule_LoadingDefaultContent;
            context.Locate.ContentEvents().LoadedDefaultContent += ModelModule_LoadedDefaultContent;
            
            context.Locate.ContentEvents().FailedLoadingContent += ModelModule_FailedLoadingContent;

            context.Locate.ContentEvents().MovingContent += ModelModule_MovingContent;
            context.Locate.ContentEvents().MovedContent += ModelModule_MovedContent;
            
            context.Locate.ContentEvents().PublishingContent += ModelModule_PublishingContent;
            context.Locate.ContentEvents().PublishedContent += ModelModule_PublishedContent;
            
            context.Locate.ContentEvents().RejectedContent += ModelModule_RejectedContent;
            context.Locate.ContentEvents().RejectingContent += ModelModule_RejectingContent;
            
            context.Locate.ContentEvents().RequestedApproval += ModelModule_RequestedApproval;
            context.Locate.ContentEvents().RequestingApproval += ModelModule_RequestingApproval;
            
            context.Locate.ContentEvents().SavingContent += ModelModule_SavingContent;
            context.Locate.ContentEvents().SavedContent += ModelModule_SavedContent;
            
            context.Locate.ContentEvents().ScheduledContent +=  ModelModule_ScheduledContent;
            context.Locate.ContentEvents().SchedulingContent +=  ModelModule_SchedulingContent;
            
        }

        private void ModelModule_SchedulingContent(object sender, ContentEventArgs e)
        {
            foreach (var handler in this.contentEventHandlers)
            {
                handler.OnContentSheduling(e.Content);
            }
        }

        private void ModelModule_ScheduledContent(object sender, ContentEventArgs e)
        {
            foreach (var handler in this.contentEventHandlers)
            {
                handler.OnContentSheduled(e.Content);
            }
        }

        private void ModelModule_RequestingApproval(object sender, ContentEventArgs e)
        {
            foreach (var handler in this.contentEventHandlers)
            {
                handler.OnContentApprovalRequesting(e.Content);
            }
        }

        private void ModelModule_RequestedApproval(object sender, ContentEventArgs e)
        {
            foreach (var handler in this.contentEventHandlers)
            {
                handler.OnContentApprovalRequested(e.Content);
            }
        }

        private void ModelModule_RejectingContent(object sender, ContentEventArgs e)
        {
            foreach (var handler in this.contentEventHandlers)
            {
                handler.OnContentRejecting(e.Content);
            }
        }

        private void ModelModule_RejectedContent(object sender, ContentEventArgs e)
        {
            foreach (var handler in this.contentEventHandlers)
            {
                handler.OnContentRejected(e.Content);
            }
        }

        private void ModelModule_FailedLoadingContent(object sender, ContentEventArgs e)
        {
            foreach (var handler in this.contentEventHandlers)
            {
                handler.OnContentLoadFailed(e.Content);
            }
        }

        private void ModelModule_LoadedDefaultContent(object sender, ContentEventArgs e)
        {
            foreach (var handler in this.contentEventHandlers)
            {
                handler.OnDefaultContentLoaded(e.Content);
            }
        }

        private void ModelModule_LoadingDefaultContent(object sender, ContentEventArgs e)
        {
            foreach (var handler in this.contentEventHandlers)
            {
                handler.OnDefaultContentLoading(e.Content);
            }
        }

        private void ModelModule_LoadedContent(object sender, ContentEventArgs e)
        {
            foreach (var handler in this.contentEventHandlers)
            {
                handler.OnContentLoaded(e.Content);
            }
        }

        private void ModelModule_LoadingContent(object sender, ContentEventArgs e)
        {
            foreach (var handler in this.contentEventHandlers)
            {
                handler.OnContentLoading(e.Content);
            }
        }

        private void ModelModule_DeletingContentVersion(object sender, ContentEventArgs e)
        {
            foreach (var handler in this.contentEventHandlers)
            {
                handler.OnContentVersionDeleting(e.Content);
            }
        }

        private void ModelModule_DeletedContentVersion(object sender, ContentEventArgs e)
        {
            foreach (var handler in this.contentEventHandlers)
            {
                handler.OnContentVersionDeleted(e.Content);
            }
        }

        private void ModelModule_DeletingContentLanguage(object sender, ContentEventArgs e)
        {
            foreach (var handler in this.contentEventHandlers)
            {
                handler.OnContentLanguageDeleting(e.Content);
            }
        }

        private void ModelModule_DeletedContentLanguage(object sender, ContentEventArgs e)
        {
            foreach (var handler in this.contentEventHandlers)
            {
                handler.OnContentLanguageDeleted(e.Content);
            }
        }

        private void ModelModule_DeletingContent(object sender, DeleteContentEventArgs e)
        {
            foreach (var handler in this.contentEventHandlers)
            {
                handler.OnContentPermanentlyDeleting(e.Content);
            }
        }

        private void ModelModule_CreatingContentLanguage(object sender, ContentEventArgs e)
        {
            foreach (var handler in this.contentEventHandlers)
            {
                handler.OnContentLanguageCreating(e.Content);
            }
        }

        private void ModelModule_CreatedContentLanguage(object sender, ContentEventArgs e)
        {
            foreach (var handler in this.contentEventHandlers)
            {
                handler.OnContentLanguageCreated(e.Content);
            }
        }

        private void ModelModule_CheckingOutContent(object sender, ContentEventArgs e)
        {
            foreach (var handler in this.contentEventHandlers)
            {
                handler.OnContentCheckingOut(e.Content);
            }
        }

        private void ModelModule_CheckedOutContent(object sender, ContentEventArgs e)
        {
            foreach (var handler in this.contentEventHandlers)
            {
                handler.OnContentCheckedOut(e.Content);
            }
        }

        private void ModelModule_CheckingInContent(object sender, ContentEventArgs e)
        {
            foreach (var handler in this.contentEventHandlers)
            {
                handler.OnContentCheckingIn(e.Content);
            }
        }

        private void ModelModule_CheckedInContent(object sender, ContentEventArgs e)
        {
            foreach (var handler in this.contentEventHandlers)
            {
                handler.OnContentCheckedIn(e.Content);
            }
        }

        private void ModelModule_DeletedContent(object sender, DeleteContentEventArgs e)
        {
            foreach (var handler in this.contentEventHandlers)
            {
                handler.OnContentPermanentlyDeleted(e.Content);
            }
        }

        private void ModelModule_MovedContent(object sender, ContentEventArgs e)
        {
            var args = e as MoveContentEventArgs;
            foreach (var handler in this.contentEventHandlers)
            {
                if (e.TargetLink == ContentReference.WasteBasket)
                {
                    handler.OnContentDeleted(e.Content);
                }
                else if (args.OriginalParent == ContentReference.WasteBasket)
                {
                    handler.OnContentRestored(args.TargetLink, e.Content);
                }
                else
                {
                    handler.OnContentMoved(args.OriginalParent, e.Content);
                }
            }
        }

        private void ModelModule_MovingContent(object sender, ContentEventArgs e)
        {
            var args = e as MoveContentEventArgs;
            foreach (var handler in this.contentEventHandlers)
            {
                if (e.TargetLink == ContentReference.WasteBasket)
                {
                    handler.OnContentDeleting(e.Content);
                }
                else if (args.OriginalParent == ContentReference.WasteBasket)
                {
                    handler.OnContentRestoring(args.TargetLink, e.Content);
                }
                else
                {
                    handler.OnContentMoving(args.OriginalParent, args.TargetLink, e.Content);
                }
            }
        }

        private void ModelModule_PublishedContent(object sender, ContentEventArgs e)
        {
            foreach (var handler in this.contentEventHandlers)
            {
                handler.OnContentPublished(e.Content);
            }
        }


        private void ModelModule_CreatedContent(object sender, ContentEventArgs e)
        {
            foreach (var handler in this.contentEventHandlers)
            {
                handler.OnContentCreated(e.Content);
            }
        }

        private void ModelModule_CreatingContent(object sender, ContentEventArgs e)
        {
            foreach (var handler in this.contentEventHandlers)
            {
                handler.OnContentCreating(e.Content);
            }
        }

        private void ModelModule_SavedContent(object sender, ContentEventArgs e)
        {
            foreach (var handler in this.contentEventHandlers)
            {
                handler.OnContentSaved(e.Content);
            }
        }

        private void ModelModule_SavingContent(object sender, ContentEventArgs e)
        {
            var saveEventArgs = e as SaveContentEventArgs;
            foreach (var handler in this.contentEventHandlers)
            {
                handler.OnContentSaving(e.Content, saveEventArgs.Action);
            }
        }

        private void ModelModule_PublishingContent(object sender, ContentEventArgs e)
        {
            foreach (var handler in this.contentEventHandlers)
            {
                handler.OnContentPublishing(e.Content);
            }
        }

        public void Uninitialize(InitializationEngine context)
        {
            context.Locate.ContentEvents().CheckedInContent -= ModelModule_CheckedInContent;
            context.Locate.ContentEvents().CheckingInContent -= ModelModule_CheckingInContent;

            context.Locate.ContentEvents().CheckedOutContent -= ModelModule_CheckedOutContent;
            context.Locate.ContentEvents().CheckingOutContent -= ModelModule_CheckingOutContent;

            context.Locate.ContentEvents().CreatedContentLanguage -= ModelModule_CreatedContentLanguage;
            context.Locate.ContentEvents().CreatingContentLanguage -= ModelModule_CreatingContentLanguage;

            context.Locate.ContentEvents().CreatedContent -= ModelModule_CreatedContent;
            context.Locate.ContentEvents().CreatingContent -= ModelModule_CreatingContent;

            context.Locate.ContentEvents().DeletedContent -= ModelModule_DeletedContent;
            context.Locate.ContentEvents().DeletingContent -= ModelModule_DeletingContent;

            context.Locate.ContentEvents().DeletedContentLanguage -= ModelModule_DeletedContentLanguage;
            context.Locate.ContentEvents().DeletingContentLanguage -= ModelModule_DeletingContentLanguage;

            context.Locate.ContentEvents().DeletedContentVersion -= ModelModule_DeletedContentVersion;
            context.Locate.ContentEvents().DeletingContentVersion -= ModelModule_DeletingContentVersion;

            context.Locate.ContentEvents().LoadingContent -= ModelModule_LoadingContent;
            context.Locate.ContentEvents().LoadedContent -= ModelModule_LoadedContent;

            context.Locate.ContentEvents().LoadingDefaultContent -= ModelModule_LoadingDefaultContent;
            context.Locate.ContentEvents().LoadedDefaultContent -= ModelModule_LoadedDefaultContent;

            context.Locate.ContentEvents().FailedLoadingContent -= ModelModule_FailedLoadingContent;

            context.Locate.ContentEvents().MovingContent -= ModelModule_MovingContent;
            context.Locate.ContentEvents().MovedContent -= ModelModule_MovedContent;

            context.Locate.ContentEvents().PublishingContent -= ModelModule_PublishingContent;
            context.Locate.ContentEvents().PublishedContent -= ModelModule_PublishedContent;

            context.Locate.ContentEvents().RejectedContent -= ModelModule_RejectedContent;
            context.Locate.ContentEvents().RejectingContent -= ModelModule_RejectingContent;

            context.Locate.ContentEvents().RequestedApproval -= ModelModule_RequestedApproval;
            context.Locate.ContentEvents().RequestingApproval -= ModelModule_RequestingApproval;

            context.Locate.ContentEvents().SavingContent -= ModelModule_SavingContent;
            context.Locate.ContentEvents().SavedContent -= ModelModule_SavedContent;

            context.Locate.ContentEvents().ScheduledContent -= ModelModule_ScheduledContent;
            context.Locate.ContentEvents().SchedulingContent -= ModelModule_SchedulingContent;
        }

        public void ConfigureContainer(ServiceConfigurationContext context)
        {
            context.StructureMap().Configure(c => c.Scan(a =>
            {
                foreach (var assembly in this.eventHandlersAssemblies)
                {
                    a.Assembly(assembly);                    
                }
                
                a.AddAllTypesOf<IContentEventHandler>();
            }));
        }
    }
}
