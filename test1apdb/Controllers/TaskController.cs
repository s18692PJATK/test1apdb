using Microsoft.AspNetCore.Mvc;
using test1apdb.Models;
using test1apdb.Services;

namespace test1apdb.Controllers
{
    [Route("api/tasks")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private ITaskService _service;

        public TaskController(ITaskService service)
        {
            _service = service;
        }


        [HttpGet("getAll")]
        public IActionResult GetTasks(int IdProject)
        {
            var list = _service.GetTasks(IdProject);
            if (list.Count == 0)
                return BadRequest("No data was queried, check your parameter");
            else return Ok(list);
        }

        [HttpPost("add")]
        public IActionResult AddTask(MyTask task)
        {
            int Id = _service.AddTask(task);
            if (Id == -1)
                return BadRequest("an error occured, check your task object");
            else return Ok(Id);
            
        }

    }
}