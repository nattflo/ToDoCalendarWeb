using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.EntityFrameworkCore;
using ToDoCalendarWeb.Controllers;
using ToDoCalendarWeb.Domain;
using Task = System.Threading.Tasks.Task;

namespace ToDoCalendarWeb.Server.Tests.Controllers;

public class PeriodsControllerTests
{
    #region GET_TASKS

    [Fact]
    public async Task GetTasks_WhenTasksExist_ReturnsListOfTasks()
    {
        // Arrange
        var context = new Mock<AppDbContext>();

        var period = new Period()
        {
            Id = Guid.NewGuid(),
            Name = "Test1",
            RoutineId = Guid.NewGuid(),
            DayOfWeek = Domain.DayOfWeek.Monday,
            TimePeriod = new TimePeriod()
        };
        var tasks = new List<Domain.Task>()
        {
            new ()
            {
                Id = Guid.NewGuid(),
                Name = "Test1",
                PeriodId = period.Id
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Test2",
                PeriodId = period.Id
            }
        };
        context.Setup(x => x.Set<Period>()).ReturnsDbSet(new[] { period });
        context.Setup(x => x.Set<Domain.Task>()).ReturnsDbSet(tasks);

        var controller = new PeriodsController(context.Object);

        // Act

        var result = await controller.GetTasks(period.Id);

        // Assert

        Assert.NotNull(result.Value);
        Assert.Equal(tasks, result.Value);
    }

    [Fact]
    public async Task GetTasks_WhenTasksDoNotExist_ReturnsNoContent()
    {
        // Arrange

        var context = new Mock<AppDbContext>();

        var period = new Period() { Id = Guid.NewGuid(), Name = "Test1", RoutineId = Guid.NewGuid(), DayOfWeek = Domain.DayOfWeek.Monday, TimePeriod = new TimePeriod() };
        context.Setup(x => x.Set<Period>()).ReturnsDbSet(new[] { period });
        context.Setup(x => x.Set<Domain.Task>()).ReturnsDbSet(new List<Domain.Task>() { });

        var controller = new PeriodsController(context.Object);

        // Act

        var result = await controller.GetTasks(period.Id);

        // Assert

        Assert.IsType<NoContentResult>(result.Result);

    }
    #endregion

    #region GET_ALL

    [Fact]
    public async Task GetAll_ReturnsAllPeriods()
    {
        // Arrange
        var context = new Mock<AppDbContext>();

        var routine = new Routine() { Name = "Test1", Id = Guid.NewGuid() };
        var periods = new List<Period>()
        {
            new ()
            {
                Id = Guid.NewGuid(),
                Name = "Test1",
                RoutineId = routine.Id,
                DayOfWeek = Domain.DayOfWeek.Monday,
                TimePeriod = new TimePeriod()
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Test2",
                RoutineId = routine.Id,
                DayOfWeek = Domain.DayOfWeek.Monday,
                TimePeriod = new TimePeriod()
            }
        };
        context.Setup(x => x.Set<Routine>()).ReturnsDbSet(new[] { routine });
        context.Setup(x => x.Set<Period>()).ReturnsDbSet(periods);

        var controller = new PeriodsController(context.Object);

        // Act
        var result = await controller.GetAll();

        // Assert
        Assert.NotNull(result.Value);
        Assert.Equal(periods, result.Value);
    }

    #endregion

    #region GET

    [Fact]
    public async Task Get_WhenPeriodDoNotExist_ReturnsNotFound()
    {
        // Arrange

        var context = new Mock<AppDbContext>();

        var periods = new List<Period>()
        {
            new()
            {
                Id = Guid.Empty,
                Name = "Test1",
                RoutineId = Guid.NewGuid(),
                DayOfWeek = Domain.DayOfWeek.Monday,
                TimePeriod = new TimePeriod()
            }
        };
        context.Setup(x => x.Set<Period>()).ReturnsDbSet(periods);

        var controller = new PeriodsController(context.Object);

        // Act
        var result = await controller.Get(Guid.NewGuid());

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task Get_WhenPeriodExist_ReturnsPeriod()
    {
        // Arrange

        var context = new Mock<AppDbContext>();

        var periods = new List<Period>()
        {
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Test1",
                RoutineId = Guid.NewGuid(),
                DayOfWeek = Domain.DayOfWeek.Monday,
                TimePeriod = new TimePeriod()
            }
        };
        context.Setup(x => x.Set<Period>()).ReturnsDbSet(periods);

        var controller = new PeriodsController(context.Object);

        // Act
        var result = await controller.Get(periods[0].Id);

        // Assert
        Assert.Equal(result.Value, periods[0]);
    }

    #endregion

    #region PUT

    [Fact]
    public async Task Put_UpdateExistingPeriodButIdIsNotExisting_ReturnsBadRequest()
    {
        // Arrange
        var context = new Mock<AppDbContext>();

        var period = new Period() { Id = Guid.NewGuid(), Name = "Test1", RoutineId = Guid.NewGuid(), DayOfWeek = Domain.DayOfWeek.Monday, TimePeriod = new TimePeriod() };
        context.Setup(x => x.Set<Period>()).ReturnsDbSet(new[] { period } );

        var controller = new PeriodsController(context.Object);

        // Act
        var result = await controller.Put(Guid.NewGuid(), period);

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task Put_UpdateExistingPeriod_ReturnsOK()
    {
        // Arrange
        var context = new Mock<AppDbContext>();

        var period = new Period() { Id = Guid.NewGuid(), Name = "Test1", RoutineId = Guid.NewGuid(), DayOfWeek = Domain.DayOfWeek.Monday, TimePeriod = new TimePeriod() };
        context.Setup(x => x.Set<Period>()).ReturnsDbSet(new[] { period });
        context.Setup(x => x.Set<Diff>()).ReturnsDbSet(new List<Diff>());

        var controller = new PeriodsController(context.Object);

        // Act
        var result = await controller.Put(period.Id, period);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task Put_UpdatePeriodWithEntityChanges_AddRangeCalledOnceForDiffs()
    {
        // Arrange
        var context = new Mock<AppDbContext>();

        var period = new Period() { Id = Guid.NewGuid(), Name = "Test1", RoutineId = Guid.NewGuid(), DayOfWeek = Domain.DayOfWeek.Monday, TimePeriod = new TimePeriod() };
        var changedPeriod = new Period() { Id = period.Id, Name = "Test2", RoutineId = period.RoutineId, DayOfWeek = period.DayOfWeek, TimePeriod = period.TimePeriod };

        context.Setup(x => x.Set<Period>()).ReturnsDbSet(new[] { period });
        context.Setup(x => x.Set<Diff>()).ReturnsDbSet(new List<Diff>());

        var controller = new PeriodsController(context.Object);

        // Act
        await controller.Put(period.Id, changedPeriod);

        // Assert
        context.Verify(x => x.Set<Diff>().AddRange(It.IsAny<IEnumerable<Diff>>()), Times.Once());
    }

    [Fact]
    public async Task Put_UpdatePeriodWithoutEntityChanges_AddRangeCalledOnceForDiffsWithEmptyList()
    {
        // Arrange
        var context = new Mock<AppDbContext>();

        var period = new Period() { Id = Guid.NewGuid(), Name = "Test1", RoutineId = Guid.NewGuid(), DayOfWeek = Domain.DayOfWeek.Monday, TimePeriod = new TimePeriod() };
        var changedPeriod = new Period() { Id = period.Id, Name = "Test1", RoutineId = period.RoutineId, DayOfWeek = period.DayOfWeek, TimePeriod = period.TimePeriod };

        context.Setup(x => x.Set<Period>()).ReturnsDbSet(new[] { period });
        context.Setup(x => x.Set<Diff>()).ReturnsDbSet(new List<Diff>());
        var controller = new PeriodsController(context.Object);

        // Act
        await controller.Put(period.Id, changedPeriod);

        // Assert
        context.Verify(x => x.Set<Diff>().AddRange(new List<Diff>()), Times.Once());
    }

    #endregion

    #region POST

    [Fact]
    public async Task Post_AddMethodCalledOnce()
    {
        // Arrange
        var context = new Mock<AppDbContext>();

        var period = new Period() { Name = "Test1", RoutineId = Guid.NewGuid(), DayOfWeek = Domain.DayOfWeek.Monday, TimePeriod = new TimePeriod() };

        context.Setup(x => x.Set<Period>()).ReturnsDbSet(new List<Period>());

        var controller = new PeriodsController(context.Object);

        // Act
        await controller.Post(period);

        // Assert
        context.Verify(x => x.Set<Period>().Add(period), Times.Once);
    }


    #endregion

    #region DELETE

    [Fact]
    public async Task Delete_DeletingExistedPeriod_RemoveMethodCalledOnce()
    {
        // Arrange
        var context = new Mock<AppDbContext>();

        var period = new Period()
        {
            Id = Guid.NewGuid(),
            Name = "Test1",
            RoutineId = Guid.NewGuid(),
            DayOfWeek = Domain.DayOfWeek.Monday,
            TimePeriod = new TimePeriod()
        };

        context.Setup(x => x.Set<Period>()).ReturnsDbSet(new[] { period });

        var controller = new PeriodsController(context.Object);

        // Act
        var result = await controller.Delete(period.Id);

        // Assert
        context.Verify(x => x.Set<Period>().Remove(period), Times.Once());
    }

    [Fact]
    public async Task Delete_DeletingNotExistedPeriod_ReturnsNotFound()
    {
        // Arrange
        var context = new Mock<AppDbContext>();

        var period = new Period()
        {
            Id = Guid.NewGuid(),
            Name = "Test1",
            RoutineId = Guid.NewGuid(),
            DayOfWeek = Domain.DayOfWeek.Monday,
            TimePeriod = new TimePeriod()
        };

        context.Setup(x => x.Set<Period>()).ReturnsDbSet(new[] { period });

        var controller = new PeriodsController(context.Object);

        // Act
        var result = await controller.Delete(Guid.Empty);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    #endregion

    #region GET_DIFFS

    [Fact]
    public async Task GetDiffs_PeriodHasDiffs_ReturnsDiffsForThisPeriod()
    {
        // Arrange

        var context = new Mock<AppDbContext>();

        var period = new Period()
        {
            Id = Guid.NewGuid(),
            Name = "Test1",
            RoutineId = Guid.NewGuid(),
            DayOfWeek = Domain.DayOfWeek.Monday,
            TimePeriod = new TimePeriod()
        };

        var thisPeriodDiff = new Diff()
        {
            Id = Guid.NewGuid(),
            ObjectId = period.Id,
            ObjectType = typeof(Period).Name,
            ChangeTime = DateTime.UtcNow,
            PropName = "Name",
            From = "PreviousName",
            To = "CurrentName"
        };
        var otherPeriodDiff = new Diff()
        {
            Id = Guid.NewGuid(),
            ObjectId = Guid.NewGuid(),
            ObjectType = typeof(Period).Name,
            ChangeTime = DateTime.UtcNow,
            PropName = "Name",
            From = "PreviousNameOfOtherPeriod",
            To = "CurrentNameOfOtherPeriod"
        };

        context.Setup(x => x.Set<Period>()).ReturnsDbSet(new[] { period });
        context.Setup(x => x.Set<Diff>()).ReturnsDbSet(new[] { thisPeriodDiff, otherPeriodDiff });

        var controller = new PeriodsController(context.Object);

        // Act

        var result = await controller.GetDiffs(period.Id);

        // Assert

        Assert.NotNull(result);
        Assert.True(result.Value.Count() == 1);
        Assert.Equal(thisPeriodDiff, result.Value.First());
    }

    [Fact]
    public async Task GetDiffs_PeriodHasNotDiffs_ReturnsNotFoundResult()
    {
        // Arrange

        var context = new Mock<AppDbContext>();

        var period = new Period()
        {
            Id = Guid.NewGuid(),
            Name = "Test1",
            RoutineId = Guid.NewGuid(),
            DayOfWeek = Domain.DayOfWeek.Monday,
            TimePeriod = new TimePeriod()
        };

        var otherPeriodDiff = new Diff()
        {
            Id = Guid.NewGuid(),
            ObjectId = Guid.NewGuid(),
            ObjectType = typeof(Period).Name,
            ChangeTime = DateTime.UtcNow,
            PropName = "Name",
            From = "PreviousNameOfOtherPeriod",
            To = "CurrentNameOfOtherPeriod"
        };

        context.Setup(x => x.Set<Period>()).ReturnsDbSet(new[] { period });
        context.Setup(x => x.Set<Diff>()).ReturnsDbSet(new[] { otherPeriodDiff });

        var controller = new PeriodsController(context.Object);

        // Act

        var result = await controller.GetDiffs(period.Id);

        // Assert

        Assert.NotNull(result);
        Assert.IsType<NotFoundResult>(result.Result);
    }

    #endregion

    #region GET_DIFF

    [Fact]
    public async Task GetDiff_PeriodHasDiff_ReturnsDiffForThisPeriodById()
    {
        // Arrange
        var context = new Mock<AppDbContext>();

        var period = new Period()
        {
            Id = Guid.NewGuid(),
            Name = "Test1",
            RoutineId = Guid.NewGuid(),
            DayOfWeek = Domain.DayOfWeek.Monday,
            TimePeriod = new TimePeriod()
        };

        var thisPeriodDiff = new Diff()
        {
            Id = Guid.NewGuid(),
            ObjectId = period.Id,
            ObjectType = typeof(Period).Name,
            ChangeTime = DateTime.UtcNow,
            PropName = "Name",
            From = "PreviousName",
            To = "CurrentName"
        };

        context.Setup(x => x.Set<Period>()).ReturnsDbSet(new[] { period });
        context.Setup(x => x.Set<Diff>()).ReturnsDbSet(new[] { thisPeriodDiff });

        var controller = new PeriodsController(context.Object);

        // Act

        var result = await controller.GetDiff(period.Id, thisPeriodDiff.Id);

        // Assert

        Assert.NotNull(result);
        Assert.Equal(thisPeriodDiff, result.Value);


    }

    [Fact]
    public async Task GetDiff_PeriodHasNotDiff_ReturnsNotFoundResult()
    {
        // Arrange

        var context = new Mock<AppDbContext>();

        var period = new Period()
        {
            Id = Guid.NewGuid(),
            Name = "Test1",
            RoutineId = Guid.NewGuid(),
            DayOfWeek = Domain.DayOfWeek.Monday,
            TimePeriod = new TimePeriod()
        };

        var otherPeriodDiff = new Diff()
        {
            Id = Guid.NewGuid(),
            ObjectId = Guid.NewGuid(),
            ObjectType = typeof(Period).Name,
            ChangeTime = DateTime.UtcNow,
            PropName = "Name",
            From = "PreviousNameOfOtherPeriod",
            To = "CurrentNameOfOtherPeriod"
        };

        context.Setup(x => x.Set<Period>()).ReturnsDbSet(new[] { period });
        context.Setup(x => x.Set<Diff>()).ReturnsDbSet(new[] { otherPeriodDiff });

        var controller = new PeriodsController(context.Object);

        // Act

        var result = await controller.GetDiff(period.Id, otherPeriodDiff.Id);

        // Assert

        Assert.IsType<NotFoundResult>(result.Result);
    }

    #endregion

    #region ROLLBACK

    [Fact]
    public async Task Rollback_PeriodHasNotDiffWithGivenId_ReturnsNotFound()
    {
        // Arrange

        var context = new Mock<AppDbContext>();

        var period = new Period()
        {
            Id = Guid.NewGuid(),
            Name = "Test1",
            RoutineId = Guid.NewGuid(),
            DayOfWeek = Domain.DayOfWeek.Monday,
            TimePeriod = new TimePeriod()
        };

        var otherPeriodDiff = new Diff()
        {
            Id = Guid.NewGuid(),
            ObjectId = Guid.NewGuid(),
            ObjectType = typeof(Period).Name,
            ChangeTime = DateTime.UtcNow,
            PropName = "Name",
            From = "PreviousNameOfOtherPeriod",
            To = "CurrentNameOfOtherPeriod"
        };

        context.Setup(x => x.Set<Period>()).ReturnsDbSet(new[] { period });
        context.Setup(x => x.Set<Diff>()).ReturnsDbSet(new[] { otherPeriodDiff });

        var controller = new PeriodsController(context.Object);

        // Act

        var result = await controller.Rollback(period.Id, Guid.NewGuid());

        // Assert
        Assert.IsType<NotFoundResult>(result);

    }

    [Fact]
    public async Task Rollback_PeriodIsNotExist_ReturnsBadRequest()
    {
        // Arrange

        var context = new Mock<AppDbContext>();

        var period = new Period()
        {
            Id = Guid.NewGuid(),
            Name = "Test1",
            RoutineId = Guid.NewGuid(),
            DayOfWeek = Domain.DayOfWeek.Monday,
            TimePeriod = new TimePeriod()
        };

        var otherPeriodDiff = new Diff()
        {
            Id = Guid.NewGuid(),
            ObjectId = Guid.NewGuid(),
            ObjectType = typeof(Period).Name,
            ChangeTime = DateTime.UtcNow,
            PropName = "Name",
            From = "PreviousNameOfOtherPeriod",
            To = "CurrentNameOfOtherPeriod"
        };

        context.Setup(x => x.Set<Period>()).ReturnsDbSet(new[] { period });
        context.Setup(x => x.Set<Diff>()).ReturnsDbSet(new[] { otherPeriodDiff });

        var controller = new PeriodsController(context.Object);

        // Act

        var result = await controller.Rollback(Guid.NewGuid(), otherPeriodDiff.Id);

        // Assert

        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task Rollback_InvalidPropertyName_ReturnsBadRequestResult()
    {
        // Arrange

        var context = new Mock<AppDbContext>();

        var period = new Period()
        {
            Id = Guid.NewGuid(),
            Name = "Test1",
            RoutineId = Guid.NewGuid(),
            DayOfWeek = Domain.DayOfWeek.Monday,
            TimePeriod = new TimePeriod()
        };

        var periodDiff = new Diff()
        {
            Id = Guid.NewGuid(),
            ObjectId = period.Id,
            ObjectType = typeof(Period).Name,
            ChangeTime = DateTime.UtcNow,
            PropName = "InvalidPropertyName",
            From = "PreviousNameOfOtherPeriod",
            To = "CurrentNameOfOtherPeriod"
        };

        context.Setup(x => x.Set<Period>()).ReturnsDbSet(new[] { period });
        context.Setup(x => x.Set<Diff>()).ReturnsDbSet(new[] { periodDiff });

        var controller = new PeriodsController(context.Object);

        // Act

        var result = await controller.Rollback(period.Id, periodDiff.Id);

        // Assert

        Assert.IsType<BadRequestObjectResult>(result);

    }

    [Fact]
    public async Task Rollback_NewerDiffsIsNotExist_SetsIsTrackedFalseAndRemovesDiff()
    {
        // Arrange

        var context = new Mock<AppDbContext>();

        var period = new Period()
        {
            Id = Guid.NewGuid(),
            Name = "Test1",
            RoutineId = Guid.NewGuid(),
            DayOfWeek = Domain.DayOfWeek.Monday,
            TimePeriod = new TimePeriod()
        };

        var periodDiff = new Diff()
        {
            Id = Guid.NewGuid(),
            ObjectId = period.Id,
            ObjectType = typeof(Period).Name,
            ChangeTime = DateTime.UtcNow,
            PropName = "Name",
            From = "PreviousNameOfOtherPeriod",
            To = "CurrentNameOfOtherPeriod"
        };

        context.Setup(x => x.Set<Period>()).ReturnsDbSet(new[] { period });
        context.Setup(x => x.Set<Diff>()).ReturnsDbSet(new[] { periodDiff });

        var controller = new PeriodsController(context.Object);

        // Act

        var result = await controller.Rollback(period.Id, periodDiff.Id);

        // Assert

        Assert.False(period.IsTracked);
        context.Verify(x => x.Set<Diff>().Remove(periodDiff), Times.Once());
        context.Verify(x => x.Set<Period>().Update(period), Times.Once());
        context.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());

        Assert.IsType<CreatedAtActionResult>(result);

    }

    [Fact]
    public async Task Rollback_NewerDiffsExist_SetsIsTrackedTrue()
    {
        // Arrange

        var context = new Mock<AppDbContext>();

        var period = new Period()
        {
            Id = Guid.NewGuid(),
            Name = "Test1",
            RoutineId = Guid.NewGuid(),
            DayOfWeek = Domain.DayOfWeek.Monday,
            TimePeriod = new TimePeriod()
        };

        var diffs = new List<Diff>()
        {
            new ()
            {
                Id = Guid.NewGuid(),
                ObjectId = period.Id,
                ObjectType = typeof(Period).Name,
                ChangeTime = DateTime.MinValue,
                PropName = "Name",
                From = "1",
                To = "2"
            },
            new ()
            {
                Id = Guid.NewGuid(),
                ObjectId = period.Id,
                ObjectType = typeof(Period).Name,
                ChangeTime = DateTime.MaxValue,
                PropName = "Name",
                From = "PreviousNameOfOtherRoutine",
                To = "CurrentNameOfOtherRoutine"
            },
        };

        context.Setup(x => x.Set<Period>()).ReturnsDbSet(new[] { period });
        context.Setup(x => x.Set<Diff>()).ReturnsDbSet(diffs);
        
        var controller = new PeriodsController(context.Object);

        // Act

        var result = await controller.Rollback(period.Id, diffs[0].Id);

        // Assert

        Assert.True(period.IsTracked);
        context.Verify(x => x.Set<Period>().Update(period), Times.Once());
        context.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());

        Assert.IsType<CreatedAtActionResult>(result);
    }

    #endregion
}
