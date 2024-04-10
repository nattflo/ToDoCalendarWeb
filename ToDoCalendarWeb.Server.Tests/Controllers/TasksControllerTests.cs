using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.EntityFrameworkCore;
using ToDoCalendarWeb.Controllers;
using ToDoCalendarWeb.Domain;
using Task = System.Threading.Tasks.Task;

namespace ToDoCalendarWeb.Server.Tests.Controllers;

public class TasksControllerTests
{
    #region GET_ALL

    [Fact]
    public async Task GetAll_ReturnsAllTasks()
    {
        // Arrange
        var context = new Mock<AppDbContext>();

        var tasks = new List<Domain.Task>()
        {
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Test1",
                PeriodId = Guid.NewGuid()
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Test2",
                PeriodId = Guid.NewGuid()
            }
        };

        context.Setup(x => x.Set<Domain.Task>()).ReturnsDbSet(tasks);

        var controller = new TasksController(context.Object);

        // Act
        var result = await controller.GetAll();

        // Assert
        Assert.NotNull(result.Value);
        Assert.Equal(tasks, result.Value);
    }

    #endregion

    #region GET

    [Fact]
    public async Task Get_GetNotExistingTask_ReturnsNotFound()
    {
        // Arrange

        var context = new Mock<AppDbContext>();

        var tasks = new List<Domain.Task>()
        {
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Test1",
                PeriodId = Guid.NewGuid()
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Test2",
                PeriodId = Guid.NewGuid()
            }
        };

        context.Setup(x => x.Set<Domain.Task>()).ReturnsDbSet(tasks);

        var controller = new TasksController(context.Object);

        // Act
        var result = await controller.Get(Guid.NewGuid());

        // Assert
        Assert.Null(result.Value);
    }

    [Fact]
    public async Task Get_GetExistingTask_ReturnsTask()
    {
        // Arrange

        var context = new Mock<AppDbContext>();

        var tasks = new List<Domain.Task>()
        {
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Test1",
                PeriodId = Guid.NewGuid()
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Test2",
                PeriodId = Guid.NewGuid()
            }
        };

        context.Setup(x => x.Set<Domain.Task>()).ReturnsDbSet(tasks);

        var controller = new TasksController(context.Object);

        // Act
        var result = await controller.Get(tasks[0].Id);

        // Assert
        Assert.NotNull(result.Value);
        Assert.Equal(tasks[0], result.Value);
    }

    #endregion

    #region PUT

    [Fact]
    public async Task Put_UpdateExistingTasksButIdIsNotExisting_ReturnsBadRequest()
    {
        // Arrange
        var context = new Mock<AppDbContext>();

        var task = new Domain.Task() { Id = Guid.NewGuid(), Name = "Test1", PeriodId = Guid.NewGuid() };
        context.Setup(x => x.Set<Domain.Task>()).ReturnsDbSet(new[] { task });

        var controller = new TasksController(context.Object);

        // Act
        var result = await controller.Put(Guid.NewGuid(), task);

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task Put_UpdateExistingTask_ReturnsOK()
    {
        // Arrange

        var context = new Mock<AppDbContext>();

        var task = new Domain.Task() { Id = Guid.NewGuid(), Name = "Test1", PeriodId = Guid.NewGuid() };
        context.Setup(x => x.Set<Domain.Task>()).ReturnsDbSet(new[] { task });
        context.Setup(x => x.Set<Diff>()).ReturnsDbSet(new List<Diff>());

        var controller = new TasksController(context.Object);

        // Act

        var result = await controller.Put(task.Id, task);

        // Assert

        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task Put_UpdateTaskWithEntityChanges_AddRangeCalledOnceForDiffs()
    {
        // Arrange
        var context = new Mock<AppDbContext>();

        var task = new Domain.Task() { Id = Guid.NewGuid(), Name = "Test1", PeriodId = Guid.NewGuid() };
        var changedTask = new Domain.Task() { Id = task.Id, Name = "Test2", PeriodId = task.PeriodId };

        context.Setup(x => x.Set<Domain.Task>()).ReturnsDbSet(new[] { task });
        context.Setup(x => x.Set<Diff>()).ReturnsDbSet(new List<Diff>());

        var controller = new TasksController(context.Object);

        // Act
        await controller.Put(task.Id, changedTask);

        // Assert
        context.Verify(x => x.Set<Diff>().AddRange(It.IsAny<IEnumerable<Diff>>()), Times.Once());
    }

    [Fact]
    public async Task Put_UpdateTaskWithoutEntityChanges_AddRangeCalledOnceForDiffsWithEmptyList()
    {
        // Arrange
        var context = new Mock<AppDbContext>();

        var task = new Domain.Task() { Id = Guid.NewGuid(), Name = "Test1", PeriodId = Guid.NewGuid() };
        var changedTask = new Domain.Task() { Id = task.Id, Name = "Test1", PeriodId = task.PeriodId };

        context.Setup(x => x.Set<Domain.Task>()).ReturnsDbSet(new[] { task });
        context.Setup(x => x.Set<Diff>()).ReturnsDbSet(new List<Diff>());
        var controller = new TasksController(context.Object);

        // Act
        await controller.Put(task.Id, changedTask);

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

        var task = new Domain.Task() { Id = Guid.NewGuid(), Name = "Test1", PeriodId = Guid.NewGuid() };

        context.Setup(x => x.Set<Domain.Task>()).ReturnsDbSet(new[] { task });

        var controller = new TasksController(context.Object);

        // Act
        await controller.Post(task);

        // Assert
        context.Verify(x => x.Set<Domain.Task>().Add(task), Times.Once);
    }


    #endregion

    #region DELETE

    [Fact]
    public async Task Delete_DeletingExistedTask_RemoveMethodCalledOnce()
    {
        // Arrange
        var context = new Mock<AppDbContext>();

        var task = new Domain.Task()
        {
            Id = Guid.NewGuid(),
            Name = "Test1",
            PeriodId = Guid.NewGuid()
        };

        context.Setup(x => x.Set<Domain.Task>()).ReturnsDbSet(new[] { task });

        var controller = new TasksController(context.Object);

        // Act

        await controller.Delete(task.Id);

        // Assert
        context.Verify(x => x.Set<Domain.Task>().Remove(task), Times.Once());
    }

    [Fact]
    public async Task Delete_DeletingNotExistedTask_ReturnsNotFound()
    {
        // Arrange
        var context = new Mock<AppDbContext>();

        context.Setup(x => x.Set<Domain.Task>()).ReturnsDbSet(new List<Domain.Task>());

        var controller = new TasksController(context.Object);

        // Act
        var result = await controller.Delete(Guid.Empty);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    #endregion

    #region GET_DIFFS

    [Fact]
    public async Task GetDiffs_TaskHasDiffs_ReturnsDiffsForThisTask()
    {
        // Arrange

        var context = new Mock<AppDbContext>();

        var task = new Domain.Task()
        {
            Id = Guid.NewGuid(),
            Name = "Test1",
            PeriodId = Guid.NewGuid()
        };

        var thisTaskDiff = new Diff()
        {
            Id = Guid.NewGuid(),
            ObjectId = task.Id,
            ObjectType = typeof(Domain.Task).Name,
            ChangeTime = DateTime.UtcNow,
            PropName = "Name",
            From = "PreviousName",
            To = "CurrentName"
        };
        var otherTaskDiff = new Diff()
        {
            Id = Guid.NewGuid(),
            ObjectId = Guid.NewGuid(),
            ObjectType = typeof(Domain.Task).Name,
            ChangeTime = DateTime.UtcNow,
            PropName = "Name",
            From = "PreviousNameOfOtherTask",
            To = "CurrentNameOfOtherTask"
        };

        context.Setup(x => x.Set<Domain.Task>()).ReturnsDbSet(new[] { task });
        context.Setup(x => x.Set<Diff>()).ReturnsDbSet(new[] { thisTaskDiff, otherTaskDiff });

        var controller = new TasksController(context.Object);

        // Act

        var result = await controller.GetDiffs(task.Id);

        // Assert

        Assert.NotNull(result);
        Assert.True(result.Value.Count() == 1);
        Assert.Equal(thisTaskDiff, result.Value.First());
    }

    [Fact]
    public async Task GetDiffs_TaskHasNotDiffs_ReturnsNotFoundResult()
    {
        // Arrange

        var context = new Mock<AppDbContext>();

        var task = new Domain.Task()
        {
            Id = Guid.NewGuid(),
            Name = "Test1",
            PeriodId = Guid.NewGuid()
        };

        var otherTaskDiff = new Diff()
        {
            Id = Guid.NewGuid(),
            ObjectId = Guid.NewGuid(),
            ObjectType = typeof(Domain.Task).Name,
            ChangeTime = DateTime.UtcNow,
            PropName = "Name",
            From = "PreviousNameOfOtherTask",
            To = "CurrentNameOfOtherTask"
        };

        context.Setup(x => x.Set<Domain.Task>()).ReturnsDbSet(new[] { task });
        context.Setup(x => x.Set<Diff>()).ReturnsDbSet(new[] { otherTaskDiff });

        var controller = new TasksController(context.Object);

        // Act

        var result = await controller.GetDiffs(task.Id);

        // Assert

        Assert.NotNull(result);
        Assert.IsType<NotFoundResult>(result.Result);
    }

    #endregion

    #region GET_DIFF

    [Fact]
    public async Task GetDiff_TaskHasDiff_ReturnsDiffForThisTaskById()
    {
        // Arrange
        var context = new Mock<AppDbContext>();

        var task = new Domain.Task()
        {
            Id = Guid.NewGuid(),
            Name = "Test1",
            PeriodId = Guid.NewGuid()
        };

        var thisTaskDiff = new Diff()
        {
            Id = Guid.NewGuid(),
            ObjectId = task.Id,
            ObjectType = typeof(Domain.Task).Name,
            ChangeTime = DateTime.UtcNow,
            PropName = "Name",
            From = "PreviousName",
            To = "CurrentName"
        };

        context.Setup(x => x.Set<Domain.Task>()).ReturnsDbSet(new[] { task });
        context.Setup(x => x.Set<Diff>()).ReturnsDbSet(new[] { thisTaskDiff });

        var controller = new TasksController(context.Object);

        // Act

        var result = await controller.GetDiff(task.Id, thisTaskDiff.Id);

        // Assert

        Assert.NotNull(result);
        Assert.Equal(thisTaskDiff, result.Value);


    }

    [Fact]
    public async Task GetDiff_TaskHasNotDiff_ReturnsNotFoundResult()
    {
        // Arrange

        var context = new Mock<AppDbContext>();

        var task = new Domain.Task()
        {
            Id = Guid.NewGuid(),
            Name = "Test1",
            PeriodId = Guid.NewGuid()
        };

        var otherTaskDiff = new Diff()
        {
            Id = Guid.NewGuid(),
            ObjectId = Guid.NewGuid(),
            ObjectType = typeof(Domain.Task).Name,
            ChangeTime = DateTime.UtcNow,
            PropName = "Name",
            From = "PreviousNameOfOtherTask",
            To = "CurrentNameOfOtherTask"
        };

        context.Setup(x => x.Set<Domain.Task>()).ReturnsDbSet(new[] { task });
        context.Setup(x => x.Set<Diff>()).ReturnsDbSet(new[] { otherTaskDiff });

        var controller = new TasksController(context.Object);

        // Act

        var result = await controller.GetDiff(task.Id, otherTaskDiff.Id);

        // Assert

        Assert.IsType<NotFoundResult>(result.Result);
    }

    #endregion

    #region ROLLBACK

    [Fact]
    public async Task Rollback_TaskHasNotDiffWithGivenId_ReturnsNotFound()
    {
        // Arrange

        var context = new Mock<AppDbContext>();

        var task = new Domain.Task()
        {
            Id = Guid.NewGuid(),
            Name = "Test1",
            PeriodId = Guid.NewGuid()
        };

        var otherTaskDiff = new Diff()
        {
            Id = Guid.NewGuid(),
            ObjectId = Guid.NewGuid(),
            ObjectType = typeof(Domain.Task).Name,
            ChangeTime = DateTime.UtcNow,
            PropName = "Name",
            From = "PreviousNameOfOtherTask",
            To = "CurrentNameOfOtherTask"
        };

        context.Setup(x => x.Set<Domain.Task>()).ReturnsDbSet(new[] { task });
        context.Setup(x => x.Set<Diff>()).ReturnsDbSet(new[] { otherTaskDiff });

        var controller = new TasksController(context.Object);

        // Act

        var result = await controller.Rollback(task.Id, otherTaskDiff.Id);

        // Assert
        Assert.IsType<NotFoundResult>(result);

    }

    [Fact]
    public async Task Rollback_TaskIsNotExist_ReturnsBadRequest()
    {
        // Arrange

        var context = new Mock<AppDbContext>();

        var task = new Domain.Task()
        {
            Id = Guid.NewGuid(),
            Name = "Test1",
            PeriodId = Guid.NewGuid()
        };

        var otherTaskDiff = new Diff()
        {
            Id = Guid.NewGuid(),
            ObjectId = Guid.NewGuid(),
            ObjectType = typeof(Domain.Task).Name,
            ChangeTime = DateTime.UtcNow,
            PropName = "Name",
            From = "PreviousNameOfOtherTask",
            To = "CurrentNameOfOtherTask"
        };

        context.Setup(x => x.Set<Domain.Task>()).ReturnsDbSet(new[] { task });
        context.Setup(x => x.Set<Diff>()).ReturnsDbSet(new[] { otherTaskDiff });

        var controller = new TasksController(context.Object);

        // Act

        var result = await controller.Rollback(Guid.NewGuid(), otherTaskDiff.Id);

        // Assert

        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task Rollback_InvalidPropertyName_ReturnsBadRequestResult()
    {
        // Arrange

        var context = new Mock<AppDbContext>();

        var task = new Domain.Task()
        {
            Id = Guid.NewGuid(),
            Name = "Test1",
            PeriodId = Guid.NewGuid()
        };

        var tasksDiff = new Diff()
        {
            Id = Guid.NewGuid(),
            ObjectId = task.Id,
            ObjectType = typeof(Domain.Task).Name,
            ChangeTime = DateTime.UtcNow,
            PropName = "InvalidPropertyName",
            From = "PreviousName",
            To = "CurrentName"
        };

        context.Setup(x => x.Set<Domain.Task>()).ReturnsDbSet(new[] { task });
        context.Setup(x => x.Set<Diff>()).ReturnsDbSet(new[] { tasksDiff });

        var controller = new TasksController(context.Object);

        // Act

        var result = await controller.Rollback(task.Id, tasksDiff.Id);

        // Assert

        Assert.IsType<BadRequestObjectResult>(result);

    }

    [Fact]
    public async Task Rollback_NewerDiffsIsNotExist_SetsIsTrackedFalseAndRemovesDiff()
    {
        // Arrange

        var context = new Mock<AppDbContext>();

        var task = new Domain.Task()
        {
            Id = Guid.NewGuid(),
            Name = "Test1",
            PeriodId = Guid.NewGuid()
        };

        var tasksDiff = new Diff()
        {
            Id = Guid.NewGuid(),
            ObjectId = task.Id,
            ObjectType = typeof(Domain.Task).Name,
            ChangeTime = DateTime.UtcNow,
            PropName = "Name",
            From = "PreviousName",
            To = "CurrentName"
        };

        context.Setup(x => x.Set<Domain.Task>()).ReturnsDbSet(new[] { task });
        context.Setup(x => x.Set<Diff>()).ReturnsDbSet(new[] { tasksDiff });

        var controller = new TasksController(context.Object);

        // Act

        var result = await controller.Rollback(task.Id, tasksDiff.Id);

        // Assert

        Assert.False(task.IsTracked);
        context.Verify(x => x.Set<Diff>().Remove(tasksDiff), Times.Once());
        context.Verify(x => x.Set<Domain.Task>().Update(task), Times.Once());
        context.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());

        Assert.IsType<CreatedAtActionResult>(result);

    }

    [Fact]
    public async Task Rollback_NewerDiffsExist_SetsIsTrackedTrue()
    {
        // Arrange

        var context = new Mock<AppDbContext>();

        var task = new Domain.Task()
        {
            Id = Guid.NewGuid(),
            Name = "Test1",
            PeriodId = Guid.NewGuid()
        };

        var diffs = new List<Diff>()
        {
            new ()
            {
                Id = Guid.NewGuid(),
                ObjectId = task.Id,
                ObjectType = typeof(Domain.Task).Name,
                ChangeTime = DateTime.MinValue,
                PropName = "Name",
                From = "1",
                To = "2"
            },
            new ()
            {
                Id = Guid.NewGuid(),
                ObjectId = task.Id,
                ObjectType = typeof(Domain.Task).Name,
                ChangeTime = DateTime.MaxValue,
                PropName = "Name",
                From = "PreviousName",
                To = "CurrentName"
            }
        };

        context.Setup(x => x.Set<Domain.Task>()).ReturnsDbSet(new[] { task });
        context.Setup(x => x.Set<Diff>()).ReturnsDbSet(diffs);

        var controller = new TasksController(context.Object);

        // Act

        var result = await controller.Rollback(task.Id, diffs[0].Id);

        // Assert

        Assert.True(task.IsTracked);
        context.Verify(x => x.Set<Domain.Task>().Update(task), Times.Once());
        context.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());

        Assert.IsType<CreatedAtActionResult>(result);
    }

    #endregion
}
