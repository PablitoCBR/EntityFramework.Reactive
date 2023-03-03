using EntityFramework.Reactive.ChangeDetection.Changes;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EntityFramework.Reactive.ChangeDetection.Tests.Changes;

public class ChangeContextTests
{
    [Fact]
    public void Should_DetectChange_OnSingleProperty()
    {
        // Arrange
        var changeContext = new ChangeContext(new ChangeDetectionOrchiestrator());

        // Act
        //changeContext.DetectChanges() // those tests requires in-memory DB


        // Assert
        Assert.True(true); //TODO
        //changeContext.ChangedEntities.Should().HaveCount(1);
    }
}