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
        private readonly Assembly eventHandlersAssembly;

        protected ContentEventHandlersModule(Assembly eventHandlersAssembly)
        {
            this.eventHandlersAssembly = eventHandlersAssembly;
        }
        
        public void Initialize(InitializationEngine context)
        {
            this.contentEventHandlers = context.Locate.Advanced.GetAllInstances<IContentEventHandler>();

            context.Locate.ContentEvents().CreatedContent += ModelModule_CreatedContent;
            context.Locate.ContentEvents().CreatingContent += ModelModule_CreatingContent;
            context.Locate.ContentEvents().SavingContent += ModelModule_SavingContent;
            context.Locate.ContentEvents().SavedContent += ModelModule_SavedContent;
            context.Locate.ContentEvents().PublishingContent += ModelModule_PublishingContent;
            context.Locate.ContentEvents().PublishedContent += ModelModule_PublishedContent;
            context.Locate.ContentEvents().MovingContent += ModelModule_MovingContent;
            context.Locate.ContentEvents().MovedContent += ModelModule_MovedContent;
            context.Locate.ContentEvents().DeletedContent += ModelModule_DeletedContent;
        }

        private void ModelModule_DeletedContent(object sender, DeleteContentEventArgs e)
        {
            foreach (var handler in this.contentEventHandlers)
            {
                handler.OnContentDeleted(e.TargetLink, e.Content);
            }
        }

        private void ModelModule_MovedContent(object sender, ContentEventArgs e)
        {
            var args = e as MoveContentEventArgs;
            foreach (var handler in this.contentEventHandlers)
            {
                if (e.TargetLink == ContentReference.WasteBasket)
                {
                    handler.OnContentDeleted(args.OriginalParent, e.Content);
                }
                else
                {
                    handler.OnContentMoved(args.OriginalParent, e.Content);
                }
            }
        }

        private void ModelModule_MovingContent(object sender, ContentEventArgs e)
        {
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
            
        }

        public void ConfigureContainer(ServiceConfigurationContext context)
        {
            context.StructureMap().Configure(c => c.Scan(a =>
            {
                a.Assembly(this.eventHandlersAssembly);
                a.AddAllTypesOf<IContentEventHandler>();
            }));
        }
    }
}
