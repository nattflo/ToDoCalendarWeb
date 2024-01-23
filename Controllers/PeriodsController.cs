using Microsoft.AspNetCore.Mvc;
using ToDoCalendarWeb.Domain;

namespace ToDoCalendarWeb.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PeriodsController(AppDbContext context) : AbstractControllerWithTracking<Period>(context);
