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

        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Task>> GetTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            task.Steps = await _context.Steps.Where(x => x.TaskId == id).ToListAsync();
            if (task == null)
                return BadRequest(new { message = "Task không tồn tại" });

            return task;
        }

        [HttpPost]
        public async Task<ActionResult> PostTask(CreateTaskRequest request)
        {
            var task = new Models.Task
            {
                Title = request.Title,
                CreatedDate = DateTime.Now,
                IsComplete = false,
                CreatedBy = request.CreatedBy
            };

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetTask", new { id = task.Id }, task);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTask(int id, CreateTaskRequest request)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
                return BadRequest(new { message = "Task không tồn tại" });
            task.Title = request.Title;
            task.DueDate = request.DueDate;
            task.IsComplete = request.IsComplete;
            task.CreatedBy = request.CreatedBy;

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTask", new { id = task.Id }, task);
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

        [HttpGet("{userId}/tasks")]
        public async Task<ActionResult<IEnumerable<Models.Task>>> GetTasks(Guid userId)
        {
            return await _context.Tasks.Where(x => x.CreatedBy == userId).OrderByDescending(x => x.Id).ToListAsync();
        }

        [HttpGet("{userId}/tasks/{searchString}")]
        public async Task<ActionResult<IEnumerable<Models.Task>>> SearchTask(Guid userId, string searchString)
        {
            var tasks = await _context.Tasks.Where(x => x.CreatedBy == userId && x.Title.ToLower().Contains(searchString.ToLower())).ToListAsync();
            if (tasks == null)
                return BadRequest(new { message = "Danh sách trống" });
            
            return tasks;
        }
        
    }
}