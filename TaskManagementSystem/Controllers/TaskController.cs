using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskManagementSystem.DTOs.TaskDTOs;
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


            return Ok(result);
        }

        [HttpPut("{taskId}")]
        [Authorize]
        public async Task<IActionResult> UpdateTask(Guid taskId, TaskDto updateTaskDto)
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

            if(appUser.Id != task.UserId)
            {
                return BadRequest("You can`t update this task");
            }

            var taskUpdated = updateTaskDto.ToUpdateTask(task.TaskId, appUser.Id);
            


            var result = await _taskRepository.UpdateTask(taskId, taskUpdated);

            if (result is null) return BadRequest("Something went wrong during updating");

            return Ok(result.ToTaskResultDto());
        }

    }
}
