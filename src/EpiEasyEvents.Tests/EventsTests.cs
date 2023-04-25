using EpiEasyEvents.Tests.TestHandlers;
using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAccess;
using EPiServer.Security;
using Forte.EpiEasyEvents;
using Forte.EpiEasyEvents.EventHandlers;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NUnit.Framework;

namespace EpiEasyEvents.Tests;

[TestFixture]
public partial class EventsTests
{
    [Test]
    public void GivenNoHandlersAreSubscribed_WhenEventIsRaised_ThenNoExceptionIsThrown()
    {
        // Arrange
        var serviceCollection = new ServiceCollection();
        var serviceProvider = serviceCollection.BuildServiceProvider();

        var contentLoader = Substitute.For<IContentLoader>();
        var contentEvents = Substitute.For<IContentEvents>();
        var contentSecurityEvents = Substitute.For<IContentSecurityEvents>();

        var eventRegistry = new EventsRegistry(serviceProvider, contentLoader, contentEvents, contentSecurityEvents);
        eventRegistry.RegisterEvents();

        // Act
        contentEvents.PublishedContent += Raise.EventWith(new ContentEventArgs(new ContentReference(1)));

        contentSecurityEvents.ContentSecuritySaved +=
            Raise.EventWith(new ContentSecurityEventArg(new ContentReference(1), new ContentAccessControlList(), SecuritySaveType.Replace));

        // Assert
        Assert.Pass();
    }

    [Test]
    public void GivenHandlerIsSubscribed_WhenEventIsRaised_ThenHandlerIsCalled()
    {
        // Arrange
        var serviceCollection = new ServiceCollection();
        var handler = new PublishedHandler();
        serviceCollection.AddSingleton<IContentPublishedHandler<PageData>>(handler);
        var serviceProvider = serviceCollection.BuildServiceProvider();

        var contentLoader = Substitute.For<IContentLoader>();
        var contentEvents = Substitute.For<IContentEvents>();
        var contentSecurityEvents = Substitute.For<IContentSecurityEvents>();

        var eventRegistry = new EventsRegistry(serviceProvider, contentLoader, contentEvents, contentSecurityEvents);
        eventRegistry.RegisterEvents();

        // Act
        contentEvents.PublishedContent +=
            Raise.EventWith<ContentEventArgs>(new SaveContentEventArgs(new ContentReference(1), Substitute.For<PageData>(), SaveAction.Default, new StatusTransition()));

        // Assert
        Assert.That(handler.RaisedEvents, Has.Count.EqualTo(1));
    }

    [Test]
    public void GivenHandlerIsSubscribed_WhenEventIsRaisedMultipleTimes_ThenHandlerIsCalledMultipleTimes()
    {
        // Arrange
        var serviceCollection = new ServiceCollection();
        var handler = new PublishedHandler();
        serviceCollection.AddSingleton<IContentPublishedHandler<PageData>>(handler);
        var serviceProvider = serviceCollection.BuildServiceProvider();

        var contentLoader = Substitute.For<IContentLoader>();
        var contentEvents = Substitute.For<IContentEvents>();
        var contentSecurityEvents = Substitute.For<IContentSecurityEvents>();

        var eventRegistry = new EventsRegistry(serviceProvider, contentLoader, contentEvents, contentSecurityEvents);
        eventRegistry.RegisterEvents();

        // Act
        contentEvents.PublishedContent +=
            Raise.EventWith<ContentEventArgs>(new SaveContentEventArgs(new ContentReference(1), Substitute.For<PageData>(), SaveAction.Default, new StatusTransition()));

        contentEvents.PublishedContent +=
            Raise.EventWith<ContentEventArgs>(new SaveContentEventArgs(new ContentReference(2), Substitute.For<PageData>(), SaveAction.Default, new StatusTransition()));

        // Assert
        Assert.That(handler.RaisedEvents, Has.Count.EqualTo(2));

        Assert.Multiple(
            () =>
            {
                Assert.That(handler.RaisedEvents[0].EventArgs, Is.TypeOf<SaveContentEventArgs>());
                Assert.That(((SaveContentEventArgs) handler.RaisedEvents[0].EventArgs).ContentLink.ID, Is.EqualTo(1));

                Assert.That(handler.RaisedEvents[1].EventArgs, Is.TypeOf<SaveContentEventArgs>());
                Assert.That(((SaveContentEventArgs) handler.RaisedEvents[1].EventArgs).ContentLink.ID, Is.EqualTo(2));
            });
    }

    [Test]
    public void GivenOneHandlerImplementsMultipleInterfaces_WhenTheseEventsAreRaised_ThenHandlerIsCalled()
    {
        // Arrange
        var serviceCollection = new ServiceCollection();
        var handler = new MultipleEventsHandler();
        serviceCollection.AddSingleton<IContentCreatedHandler<PageData>>(handler);
        serviceCollection.AddSingleton<IContentDeletedHandler<PageData>>(handler);
        var serviceProvider = serviceCollection.BuildServiceProvider();

        var contentLoader = Substitute.For<IContentLoader>();
        var contentEvents = Substitute.For<IContentEvents>();
        var contentSecurityEvents = Substitute.For<IContentSecurityEvents>();

        var content = Substitute.For<PageData>();

        contentLoader.TryGet(Arg.Is<ContentReference>(value => value.ID == 1), out Arg.Any<IContent>())
            .Returns(
                callInfo =>
                {
                    callInfo[1] = content;
                    return true;
                });

        var eventRegistry = new EventsRegistry(serviceProvider, contentLoader, contentEvents, contentSecurityEvents);
        eventRegistry.RegisterEvents();

        // Act
        contentEvents.CreatedContent +=
            Raise.EventWith<ContentEventArgs>(new SaveContentEventArgs(new ContentReference(1), content, SaveAction.Default, new StatusTransition()));

        contentEvents.MovedContent += Raise.EventWith<ContentEventArgs>(new MoveContentEventArgs(new ContentReference(1), ContentReference.WasteBasket));

        // Assert
        Assert.That(handler.RaisedEvents, Has.Count.EqualTo(2));

        Assert.Multiple(
            () =>
            {
                Assert.That(handler.RaisedEvents[0].EventArgs, Is.TypeOf<SaveContentEventArgs>());
                Assert.That(((SaveContentEventArgs) handler.RaisedEvents[0].EventArgs).ContentLink.ID, Is.EqualTo(1));

                Assert.That(handler.RaisedEvents[1].EventArgs, Is.TypeOf<MoveContentEventArgs>());
                Assert.That(((MoveContentEventArgs) handler.RaisedEvents[1].EventArgs).ContentLink.ID, Is.EqualTo(1));
                Assert.That(handler.RaisedEvents[1].Content, Is.SameAs(content));
            });
    }

    [Test]
    public void GivenHandlerIsSubscribedToSecurityEvent_WhenEventIsRaised_ThenHandlerIsCalled()
    {
        // Arrange
        var serviceCollection = new ServiceCollection();
        var handler = new SecurityEventsHandler();
        serviceCollection.AddSingleton<IContentSecuritySavedHandler<PageData>>(handler);
        var serviceProvider = serviceCollection.BuildServiceProvider();

        var contentLoader = Substitute.For<IContentLoader>();
        var contentEvents = Substitute.For<IContentEvents>();
        var contentSecurityEvents = Substitute.For<IContentSecurityEvents>();

        var content = Substitute.For<PageData>();

        contentLoader.TryGet(Arg.Is<ContentReference>(value => value.ID == 1), out Arg.Any<IContent>())
            .Returns(
                callInfo =>
                {
                    callInfo[1] = content;
                    return true;
                });

        var eventRegistry = new EventsRegistry(serviceProvider, contentLoader, contentEvents, contentSecurityEvents);
        eventRegistry.RegisterEvents();

        // Act
        contentSecurityEvents.ContentSecuritySaved +=
            Raise.EventWith(new ContentSecurityEventArg(new ContentReference(1), new ContentAccessControlList(), SecuritySaveType.Replace));

        // Assert
        Assert.That(handler.RaisedEvents, Has.Count.EqualTo(1));
        Assert.Multiple(() =>
        {
            Assert.That(handler.RaisedEvents[0].EventArgs, Is.TypeOf<ContentSecurityEventArg>());
            Assert.That(((ContentSecurityEventArg)handler.RaisedEvents[0].EventArgs).ContentLink.ID, Is.EqualTo(1));
            Assert.That(handler.RaisedEvents[0].Content, Is.SameAs(content));
        });
    }
}
