using Microsoft.AspNetCore.Mvc;

namespace ToDoCalendarWeb.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TasksController(AppDbContext context) : AbstractControllerWithTracking<Domain.Task>(context);