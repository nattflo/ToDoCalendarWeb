using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.EntityFrameworkCore;
using ToDoCalendarWeb.Controllers;
using ToDoCalendarWeb.Domain;
using Task = System.Threading.Tasks.Task;

namespace ToDoCalendarWeb.Server.Tests.Controllers;

public class RoutinesControllerTest
{
    #region GET_PERIODS

    [Fact]
    public async Task GetPeriods_WhenPeriodsExist_ReturnsListOfPeriods()
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

        var controller = new RoutinesController(context.Object);

        // Act

        var result = await controller.GetPeriods(routine.Id);

        // Assert

        Assert.NotNull(result.Value);
        Assert.Equal(periods, result.Value);
    }

    [Fact]
    public async Task GetPeriods_WhenPeriodsIsNotExist_ReturnsNoContent()
    {
        // Arrange
        var context = new Mock<AppDbContext>();

        var routine = new Routine() { Name = "Test1", Id = Guid.NewGuid() };
        context.Setup(x => x.Set<Routine>()).ReturnsDbSet(new[] { routine });
        context.Setup(x => x.Set<Period>()).ReturnsDbSet(new List<Period>());

        var controller = new RoutinesController(context.Object);

        // Act

        var result = await controller.GetPeriods(routine.Id);

        // Assert

        Assert.IsType<NoContentResult>(result.Result);
    }

    #endregion

    #region GET_ALL

    [Fact]
    public async Task GetAll_ReturnsAllRoutines()
    {
        // Arrange
        var context = new Mock<AppDbContext>();

        var routines = new List<Routine>()
        {
            new ()
            {
                Id = Guid.NewGuid(),
                Name = "Test1"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Test2"
            }
        };
        context.Setup(x => x.Set<Routine>()).ReturnsDbSet(routines);

        var controller = new RoutinesController(context.Object);

        // Act
        var result = await controller.GetAll();

        // Assert
        Assert.NotNull(result.Value);
        Assert.Equal(routines, result.Value);
    }

    #endregion

    #region GET

    [Fact]
    public async Task Get_GetNotExistingRoutine_ReturnsNull()
    {
        // Arrange

        var context = new Mock<AppDbContext>();

        var routines = new List<Routine>()
        {
            new ()
            {
                Id = Guid.NewGuid(),
                Name = "Test1"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Test2"
            }
        };
        context.Setup(x => x.Set<Routine>()).ReturnsDbSet(routines);

        var controller = new RoutinesController(context.Object);

        // Act
        var result = await controller.Get(Guid.NewGuid());

        // Assert
        Assert.Null(result.Value);
    }

    [Fact]
    public async Task Get_GetExistingRoutine_ReturnsRoutine()
    {
        // Arrange

        var context = new Mock<AppDbContext>();

        var routines = new List<Routine>()
        {
            new ()
            {
                Id = Guid.NewGuid(),
                Name = "Test1"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Test2"
            }
        };
        context.Setup(x => x.Set<Routine>()).ReturnsDbSet(routines);

        var controller = new RoutinesController(context.Object);

        // Act
        var result = await controller.Get(routines[0].Id);

        // Assert
        Assert.NotNull(result.Value);
        Assert.Equal(routines[0], result.Value);
    }

    #endregion

    #region PUT

    [Fact]
    public async Task Put_UpdateExistingRoutineButIdIsNotExisting_ReturnsBadRequest()
    {
        // Arrange
        var context = new Mock<AppDbContext>();

        var routines = new List<Routine>()
        {
            new ()
            {
                Id = Guid.NewGuid(),
                Name = "Test1"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Test2"
            }
        };
        context.Setup(x => x.Set<Routine>()).ReturnsDbSet(routines);

        var controller = new RoutinesController(context.Object);

        // Act
        var result = await controller.Put(Guid.NewGuid(), routines[0]);

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task Put_UpdateExistingRoutine_ReturnsOK()
    {
        // Arrange
        var context = new Mock<AppDbContext>();

        var routines = new List<Routine>()
        {
            new ()
            {
                Id = Guid.NewGuid(),
                Name = "Test1"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Test2"
            }
        };
        context.Setup(x => x.Set<Routine>()).ReturnsDbSet(routines);
        context.Setup(x => x.Set<Diff>()).ReturnsDbSet(new List<Diff>());

        var controller = new RoutinesController(context.Object);

        // Act
        var result = await controller.Put(routines[0].Id, routines[0]);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task Put_UpdateRoutineWithEntityChanges_AddRangeCalledOnceForDiffs()
    {
        // Arrange
        var context = new Mock<AppDbContext>();

        var routine = new Routine()
        {
            Id = Guid.NewGuid(),
            Name = "Test1"
        };
        var changedRoutine = new Routine()
        {
            Id = routine.Id,
            Name = "Test2"
        };
        context.Setup(x => x.Set<Routine>()).ReturnsDbSet(new[] { routine });
        context.Setup(x => x.Set<Diff>()).ReturnsDbSet(new List<Diff>());

        var controller = new RoutinesController(context.Object);

        // Act
        await controller.Put(routine.Id, changedRoutine);

        // Assert
        context.Verify(x => x.Set<Diff>().AddRange(It.IsAny<IEnumerable<Diff>>()), Times.Once());
    }

    [Fact]
    public async Task Put_UpdateRoutineWithoutEntityChanges_AddRangeCalledOnceForDiffsWithEmptyList()
    {
        // Arrange
        var context = new Mock<AppDbContext>();

        var routine = new Routine()
        {
            Id = Guid.NewGuid(),
            Name = "Test1"
        };
        var changedRoutine = new Routine()
        {
            Id = routine.Id,
            Name = "Test1"
        };
        context.Setup(x => x.Set<Routine>()).ReturnsDbSet(new[] { routine });
        context.Setup(x => x.Set<Diff>()).ReturnsDbSet(new List<Diff>());

        var controller = new RoutinesController(context.Object);

        // Act
        await controller.Put(routine.Id, changedRoutine);

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

        var routine = new Routine()
        {
            Name = "Test1"
        };
        context.Setup(x => x.Set<Routine>()).ReturnsDbSet(new List<Routine>());

        var controller = new RoutinesController(context.Object);

        // Act
        await controller.Post(routine);

        // Assert
        context.Verify(x => x.Set<Routine>().Add(routine), Times.Once);
    }


    #endregion

    #region DELETE

    [Fact]
    public async Task Delete_DeletingExistedRoutine_RemoveMethodCalledOnce()
    {
        // Arrange
        var context = new Mock<AppDbContext>();

        var routine = new Routine()
        {
            Id = Guid.NewGuid(),
            Name = "Test1"
        };

        context.Setup(x => x.Set<Routine>()).ReturnsDbSet(new[] {routine});

        var controller = new RoutinesController(context.Object);

        // Act
        var result = await controller.Delete(routine.Id);

        // Assert
        context.Verify(x => x.Set<Routine>().Remove(routine), Times.Once());
    }

    [Fact]
    public async Task Delete_DeletingNotExistedRoutine_ReturnsNotFound()
    {
        // Arrange
        var context = new Mock<AppDbContext>();

        var routine = new Routine()
        {
            Id = Guid.NewGuid(),
            Name = "Test1"
        };

        context.Setup(x => x.Set<Routine>()).ReturnsDbSet(new[] { routine });

        var controller = new RoutinesController(context.Object);

        // Act
        var result = await controller.Delete(Guid.Empty);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    #endregion

    #region GET_DIFFS

    [Fact]
    public async Task GetDiffs_RoutineHasDiffs_ReturnsDiffsForThisRoutine()
    {
        // Arrange

        var context = new Mock<AppDbContext>();
        var routine = new Routine()
        {
            Name = "CurrentName",
            Id = Guid.NewGuid(),
        };

        var thisRoutinesDiff = new Diff()
        {
            Id = Guid.NewGuid(),
            ObjectId = routine.Id,
            ObjectType = typeof(Routine).Name,
            ChangeTime = DateTime.UtcNow,
            PropName = "Name",
            From = "PreviousName",
            To = "CurrentName"
        };
        var otherRoutinesDiff = new Diff()
        {
            Id = Guid.NewGuid(),
            ObjectId = Guid.NewGuid(),
            ObjectType = typeof(Routine).Name,
            ChangeTime = DateTime.UtcNow,
            PropName = "Name",
            From = "PreviousNameOfOtherRoutine",
            To = "CurrentNameOfOtherRoutine"
        };

        context.Setup(x => x.Set<Routine>()).ReturnsDbSet(new[] { routine });
        context.Setup(x => x.Set<Diff>()).ReturnsDbSet(new[] { thisRoutinesDiff, otherRoutinesDiff });

        var controller = new RoutinesController(context.Object);

        // Act

        var result = await controller.GetDiffs(routine.Id);

        // Assert

        Assert.NotNull(result);
        Assert.True(result.Value.Count() == 1);
        Assert.Equal(thisRoutinesDiff, result.Value.First());
    }

    [Fact]
    public async Task GetDiffs_RoutineHasNotDiffs_ReturnsNotFoundResult()
    {
        // Arrange

        var context = new Mock<AppDbContext>();
        var routine = new Routine()
        {
            Name = "CurrentName",
            Id = Guid.NewGuid(),
        };

        var otherRoutinesDiff = new Diff()
        {
            Id = Guid.NewGuid(),
            ObjectId = Guid.NewGuid(),
            ObjectType = routine.GetType().ToString(),
            ChangeTime = DateTime.UtcNow,
            PropName = "Name",
            From = "PreviousNameOfOtherRoutine",
            To = "CurrentNameOfOtherRoutine"
        };

        context.Setup(x => x.Set<Routine>()).ReturnsDbSet(new[] { routine });
        context.Setup(x => x.Set<Diff>()).ReturnsDbSet(new[] { otherRoutinesDiff });

        var controller = new RoutinesController(context.Object);

        // Act

        var result = await controller.GetDiffs(routine.Id);

        // Assert

        Assert.NotNull(result);
        Assert.IsType<NotFoundResult>(result.Result);
    }

    #endregion

    #region GET_DIFF

    [Fact]
    public async Task GetDiff_RoutineHasDiff_ReturnsDiffForThisRoutineById()
    {
        // Arrange
        var context = new Mock<AppDbContext>();

        var routine = new Routine()
        {
            Name = "CurrentName",
            Id = Guid.NewGuid(),
        };

        var thisRoutinesDiff = new Diff()
        {
            Id = Guid.NewGuid(),
            ObjectId = routine.Id,
            ObjectType = typeof(Routine).Name,
            ChangeTime = DateTime.UtcNow,
            PropName = "Name",
            From = "PreviousName",
            To = "CurrentName"
        };

        context.Setup(x => x.Set<Routine>()).ReturnsDbSet(new[] { routine });
        context.Setup(x => x.Set<Diff>()).ReturnsDbSet(new[] { thisRoutinesDiff });

        var controller = new RoutinesController(context.Object);

        // Act

        var result = await controller.GetDiff(routine.Id, thisRoutinesDiff.Id);

        // Assert

        Assert.NotNull(result);
        Assert.Equal(thisRoutinesDiff, result.Value);


    }

    [Fact]
    public async Task GetDiff_RoutineHasNotDiff_ReturnsNotFoundResult()
    {
        // Arrange

        var context = new Mock<AppDbContext>();

        var routine = new Routine() { Name = "CurrentName", Id = Guid.NewGuid()};

        var otherRoutinesDiff = new Diff()
        {
            Id = Guid.NewGuid(),
            ObjectId = Guid.Empty,
            ObjectType = routine.GetType().ToString(),
            ChangeTime = DateTime.UtcNow,
            PropName = "Name",
            From = "PreviousNameOfOtherRoutine",
            To = "CurrentNameOfOtherRoutine"
        };

        context.Setup(x => x.Set<Routine>()).ReturnsDbSet(new[] { routine });
        context.Setup(x => x.Set<Diff>()).ReturnsDbSet(new[] { otherRoutinesDiff });

        var controller = new RoutinesController(context.Object);

        // Act

        var result = await controller.GetDiff(routine.Id, otherRoutinesDiff.Id);

        // Assert
        
        Assert.IsType<NotFoundResult>(result.Result);
    }

    #endregion

    #region ROLLBACK

    [Fact]
    public async Task Rollback_RoutineHasNotDiffWithGivenId_ReturnsNotFound()
    {
        // Arrange

        var context = new Mock<AppDbContext>();

        var routine = new Routine() { Name = "CurrentName", Id = Guid.NewGuid() };

        var otherRoutinesDiff = new Diff()
        {
            Id = Guid.Empty,
            ObjectId = Guid.Empty,
            ObjectType = routine.GetType().Name,
            ChangeTime = DateTime.UtcNow,
            PropName = "Name",
            From = "PreviousNameOfOtherRoutine",
            To = "CurrentNameOfOtherRoutine"
        };

        context.Setup(x => x.Set<Routine>()).ReturnsDbSet(new[] { routine });
        context.Setup(x => x.Set<Diff>()).ReturnsDbSet(new[] { otherRoutinesDiff });

        var controller = new RoutinesController(context.Object);

        // Act

        var result = await controller.Rollback(routine.Id, otherRoutinesDiff.Id);

        // Assert
        Assert.IsType<NotFoundResult>(result);

    }

    [Fact]
    public async Task Rollback_RoutineIsNotExist_ReturnsBadRequest()
    {
        // Arrange

        var context = new Mock<AppDbContext>();
        var routine = new Routine() { Name = "CurrentName", Id = Guid.NewGuid() };

        var otherRoutinesDiff = new Diff()
        {
            Id = Guid.Empty,
            ObjectId = Guid.Empty,
            ObjectType = typeof(Routine).Name,
            ChangeTime = DateTime.UtcNow,
            PropName = "Name",
            From = "PreviousNameOfOtherRoutine",
            To = "CurrentNameOfOtherRoutine"
        };

        context.Setup(x => x.Set<Routine>()).ReturnsDbSet(new[] { routine });
        context.Setup(x => x.Set<Diff>()).ReturnsDbSet(new[] { otherRoutinesDiff });

        var controller = new RoutinesController(context.Object);

        // Act

        var result = await controller.Rollback(Guid.Empty, otherRoutinesDiff.Id);

        // Assert

        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task Rollback_InvalidPropertyName_ReturnsBadRequestResult()
    {
        // Arrange

        var context = new Mock<AppDbContext>();
        var routine = new Routine() { Name = "CurrentName", Id = Guid.NewGuid() };

        var routinesDiff = new Diff()
        {
            Id = Guid.Empty,
            ObjectId = routine.Id,
            ObjectType = typeof(Routine).Name,
            ChangeTime = DateTime.UtcNow,
            PropName = "InvalidPropertyName",
            From = "PreviousNameOfOtherRoutine",
            To = "CurrentNameOfOtherRoutine"
        };

        context.Setup(x => x.Set<Routine>()).ReturnsDbSet(new[] { routine });
        context.Setup(x => x.Set<Diff>()).ReturnsDbSet(new[] { routinesDiff });

        var controller = new RoutinesController(context.Object);

        // Act

        var result = await controller.Rollback(routine.Id, routinesDiff.Id);

        // Assert

        Assert.IsType<BadRequestObjectResult>(result);

    }

    [Fact]
    public async Task Rollback_NewerDiffsIsNotExist_SetsIsTrackedFalseAndRemovesDiff()
    {
        // Arrange

        var context = new Mock<AppDbContext>();
        var routine = new Routine() { Name = "CurrentName", Id = Guid.NewGuid() };

        var routinesDiff = new Diff()
        {
            Id = Guid.Empty,
            ObjectId = routine.Id,
            ObjectType = typeof(Routine).Name,
            ChangeTime = DateTime.UtcNow,
            PropName = "Name",
            From = "PreviousNameOfOtherRoutine",
            To = "CurrentNameOfOtherRoutine"
        };


        context.Setup(x => x.Set<Routine>()).ReturnsDbSet(new[] { routine });
        context.Setup(x => x.Set<Diff>()).ReturnsDbSet(new[] { routinesDiff });

       var controller = new RoutinesController(context.Object);

        // Act

        var result = await controller.Rollback(routine.Id, routinesDiff.Id);

        // Assert

        Assert.False(routine.IsTracked);
        context.Verify(x => x.Set<Diff>().Remove(routinesDiff), Times.Once());
        context.Verify(x => x.Set<Routine>().Update(routine), Times.Once());
        context.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());

        Assert.IsType<CreatedAtActionResult>(result);

    }

    [Fact]
    public async Task Rollback_NewerDiffsExist_SetsIsTrackedTrue()
    {
        // Arrange

        var context = new Mock<AppDbContext>();
        var routine = new Routine() { Name = "CurrentName", Id = Guid.NewGuid() };

        var diffs = new List<Diff>()
        {
            new Diff()
            {
            Id = Guid.NewGuid(),
            ObjectId = routine.Id,
            ObjectType = typeof(Routine).Name,
            ChangeTime = DateTime.MinValue,
            PropName = "Name",
            From = "1",
            To = "2"
            },
            new Diff()
            {
            Id = Guid.NewGuid(),
            ObjectId = routine.Id,
            ObjectType = typeof(Routine).Name,
            ChangeTime = DateTime.MaxValue,
            PropName = "Name",
            From = "PreviousNameOfOtherRoutine",
            To = "CurrentNameOfOtherRoutine"
            },

        };


        context.Setup(x => x.Set<Routine>()).ReturnsDbSet(new[] { routine });
        context.Setup(x => x.Set<Diff>()).ReturnsDbSet(diffs);

        var controller = new RoutinesController(context.Object);

        // Act

        var result = await controller.Rollback(routine.Id, diffs[0].Id);

        // Assert

        Assert.True(routine.IsTracked);
        context.Verify(x => x.Set<Routine>().Update(routine), Times.Once());
        context.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());

        Assert.IsType<CreatedAtActionResult>(result);
    }

    #endregion
}
