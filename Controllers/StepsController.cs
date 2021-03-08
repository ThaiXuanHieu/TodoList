using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoList.Api.Models;
using TodoList.Api.ViewModels;

namespace TodoList.Api.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class StepsController : ControllerBase
    {

        private readonly TodoListDbContext _context;
        public StepsController(TodoListDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StepVm>>> GetSteps()
        {
            return await _context.Steps.Select(x => new StepVm
            {
                Id = x.Id,
                Title = x.Title
            }).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StepVm>> GetStep(int id)
        {
            var step = await _context.Steps.FindAsync(id);
            if (step == null)
                return NotFound();

            var stepVm = new StepVm
            {
                Id = step.Id,
                Title = step.Title
            };

            return stepVm;
        }

        [HttpPost]
        public async Task<ActionResult<StepVm>> PostStep(CreateStepRequest request)
        {
            var step = new Step
            {
                Title = request.Title,
                TaskId = request.TaskId,
                IsComplete = false
            };

            _context.Steps.Add(step);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStep", new { id = step.Id }, new StepVm
            {
                Id = step.Id,
                Title = step.Title
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutStep(int id, CreateStepRequest request)
        {
            var step = await _context.Steps.FindAsync(id);
            if (step == null)
                return NotFound();
            step.Title = request.Title;
            step.TaskId = id;
            step.IsComplete = request.IsComplete;
            
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStep", new { id = step.Id }, new StepVm
            {
                Id = step.Id,
                Title = step.Title
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStep(int id)
        {
            var step = await _context.Steps.FindAsync(id);
            if (step == null)
                return NotFound();
            _context.Steps.Remove(step);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}