using Microsoft.AspNetCore.Mvc;
using ToDoCalendarWeb.Domain;

namespace ToDoCalendarWeb.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoutinesController(AppDbContext context) : AbstractControllerWithTracking<Routine>(context);