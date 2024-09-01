using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using TaskManagementSystem.DTOs.TaskDTOs;
using TaskManagementSystem.DTOs.TaskFilterDTOs;
using TaskManagementSystem.Extensions;
using TaskManagementSystem.Interfaces.ITaskRepo;
using TaskManagementSystem.Mapper.TaskMap;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Controllers
{
    [Route("api/tasks")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;
        private readonly UserManager<AppUser> _userManager;

        public TaskController(ITaskRepository taskRepository, UserManager<AppUser> userManager)
        {
            _taskRepository = taskRepository;
            _userManager = userManager;
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateTask([FromForm] TaskDto taskDto)
        {
            if (taskDto.DueDate < DateTime.Today)
            {
                return BadRequest("Due date is incorrect");
            }
            var userName = User.GetUsername();
            if (userName is null)
            {
                return Unauthorized();
            }
            var appUser = await _userManager.FindByNameAsync(userName);
            if (appUser is null)
            {
                return NotFound("User wasn`t found");
            }


            var task = taskDto.ToTask(appUser.Id);

            var result = await _taskRepository.CreateTask(task);

            if (result is null)
            {
                return BadRequest("Something went wrong during creating");
            }


            return Ok(result.ToTaskResultDto());
        }

        [HttpPut("{taskId}")]
        [Authorize]
        public async Task<IActionResult> UpdateTask(Guid taskId, [FromForm] TaskDto updateTaskDto)
        {
            if (updateTaskDto.DueDate < DateTime.Today)
            {
                return BadRequest("Due date is incorrect");
            }
            var userName = User.GetUsername();
            if (userName is null)
            {
                return Unauthorized("You need to be authorize");
            }
            var appUser = await _userManager.FindByNameAsync(userName);
            if (appUser is null)
            {
                return NotFound("User wasn`t found");
            }

            var task = await _taskRepository.GetEntityById(taskId);
            if (task is null) return NotFound("Task wasn`t found");

            if (appUser.Id != task.UserId)
            {
                return BadRequest("You can`t update this task");
            }

            var taskUpdated = updateTaskDto.ToUpdateTask(task.TaskId, appUser.Id);



            var result = await _taskRepository.UpdateTask(taskId, taskUpdated);

            if (result is null) return BadRequest("Something went wrong during updating");

            return Ok(result.ToTaskResultDto());
        }


        [HttpDelete("{taskId}")]
        [Authorize]
        public async Task<IActionResult> DeleteTask(Guid taskId)
        {
            var userName = User.GetUsername();
            if (userName is null)
            {
                return Unauthorized("You need to be authorize");
            }
            var appUser = await _userManager.FindByNameAsync(userName);
            if (appUser is null)
            {
                return NotFound("User wasn`t found");
            }

            var task = await _taskRepository.GetEntityById(taskId);
            if (task is null) return NotFound("Task wasn`t found");

            if (appUser.Id != task.UserId)
            {
                return BadRequest("You can`t delete this task");
            }

            var result = await _taskRepository.DeleteTask(task);

            if (result)
            {
                return Ok("Deleted");
            }
            else
            {
                return BadRequest("Something went during deleting");
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetTasks([FromQuery] TaskFilterDto filter, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var userName = User.GetUsername();
            if (userName is null)
            {
                return Unauthorized("You need to be authorize");
            }
            var appUser = await _userManager.FindByNameAsync(userName);
            if (appUser is null)
            {
                return NotFound("User wasn`t found");
            }
            var result = await _taskRepository.GetTasks(filter, pageNumber, pageSize, appUser.Id);

            return Ok(result.ToTaskResultDtoList());
        }

        [HttpGet("{taskId}")]
        [Authorize]
        public async Task<IActionResult> GetTaskById(Guid taskId)
        {
            var userName = User.GetUsername();
            if (userName is null)
            {
                return Unauthorized("You need to be authorize");
            }
            var appUser = await _userManager.FindByNameAsync(userName);
            if (appUser is null)
            {
                return NotFound("User wasn`t found");
            }

            var task = await _taskRepository.GetEntityById(taskId);
            if (task is null) return NotFound("Task wasn`t found");

            if (appUser.Id != task.UserId)
            {
                return BadRequest("You can`t delete this task");
            }

            return Ok(task.ToTaskResultDto());
        }

    }
}
