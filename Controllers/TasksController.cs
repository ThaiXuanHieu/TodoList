using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoList.Api.ViewModels;

namespace TodoList.Api.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class TasksController : ControllerBase
    {
        private readonly TodoListDbContext _context;
        public TasksController(TodoListDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskVm>>> GetTasks()
        {
            return await _context.Tasks.Select(x => new TaskVm
            {
                Id = x.Id,
                Title = x.Title,
                DueDate = x.DueDate,
                IsComplete = x.IsComplete
            }).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskVm>> GetTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
                return BadRequest(new {message = "Task không tồn tại"});
            var taskVm = new TaskVm
            {
                Id = task.Id,
                Title = task.Title,
                DueDate = task.DueDate,
                IsComplete = task.IsComplete
            };

            return taskVm;
        }

        [HttpPost]
        public async Task<ActionResult<TaskVm>> PostTask(CreateTaskRequest request)
        {
            var task = new Models.Task
            {
                Title = request.Title,
                CreatedDate = DateTime.Now,
                DueDate = request.DueDate,
                IsComplete = false
            };

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetTask", new { id = task.Id }, new TaskVm
            {
                Id = task.Id,
                Title = task.Title,
                DueDate = task.DueDate,
                IsComplete = task.IsComplete
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTask(int id, CreateTaskRequest request)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
                return BadRequest(new {message = "Task không tồn tại"});
            task.Title = request.Title;
            task.DueDate = request.DueDate;
            task.IsComplete = request.IsComplete;

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTask", new { id = task.Id }, new TaskVm
            {
                Id = task.Id,
                Title = task.Title,
                DueDate = task.DueDate,
                IsComplete = task.IsComplete
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
                return BadRequest(new { message = "Task không tồn tại" });
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPatch("{id}/{isComplete}")]
        public async Task<IActionResult> UpdateStatus(int id, bool isComplete)
        {
            var task = await _context.Tasks.FindAsync(id);
            if(task == null)
            {
                return BadRequest(new {message = "Task không tồn tại"});
            }

            task.IsComplete = isComplete;
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetTask", new { id = task.Id }, new TaskVm
            {
                Id = task.Id,
                Title = task.Title,
                DueDate = task.DueDate,
                IsComplete = task.IsComplete
            });
        }
    }
}