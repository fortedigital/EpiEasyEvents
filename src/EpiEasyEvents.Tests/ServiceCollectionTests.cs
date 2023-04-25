using System.Reflection;
using EpiEasyEvents.Tests.TestHandlers;
using EPiServer.Core;
using Forte.EpiEasyEvents.EventHandlers;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NUnit.Framework;

namespace EpiEasyEvents.Tests;

[TestFixture]
public class ServiceCollectionTests
{
    [Test]
    public void GivenTypesAreDefinedInAssembly_UsingEpiEasyEvents_CorrectlyRegisterThemInContainer()
    {
        // Arrange
        var serviceCollection = Substitute.For<IServiceCollection>();

        // Act
        serviceCollection.AddEpiEasyEvents(Assembly.GetAssembly(typeof(ServiceCollectionTests)));

        // Assert
        serviceCollection.Received(1).Add(
            Arg.Is<ServiceDescriptor>(
                descriptor => descriptor.ServiceType == typeof(IContentPublishedHandler<PageData>) && descriptor.ImplementationType == typeof(PublishedHandler)));

        serviceCollection.Received(1).Add(
            Arg.Is<ServiceDescriptor>(
                descriptor => descriptor.ServiceType == typeof(IContentCreatedHandler<PageData>) && descriptor.ImplementationType == typeof(MultipleEventsHandler)));

        serviceCollection.Received(1).Add(
            Arg.Is<ServiceDescriptor>(
                descriptor => descriptor.ServiceType == typeof(IContentDeletedHandler<PageData>) && descriptor.ImplementationType == typeof(MultipleEventsHandler)));

        serviceCollection.Received(1).Add(
            Arg.Is<ServiceDescriptor>(
                descriptor => descriptor.ServiceType == typeof(IContentSecuritySavedHandler<PageData>) && descriptor.ImplementationType == typeof(SecurityEventsHandler)));

        serviceCollection.DidNotReceive().Add(Arg.Is<ServiceDescriptor>(descriptor => descriptor.ImplementationType == typeof(InvalidChangedHandler)));
        serviceCollection.DidNotReceive().Add(Arg.Is<ServiceDescriptor>(descriptor => descriptor.ImplementationType == typeof(InvalidContentHandler)));
        serviceCollection.DidNotReceive().Add(Arg.Is<ServiceDescriptor>(descriptor => descriptor.ImplementationType == typeof(InvalidSecurityHandler)));
    }
}
