using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Controllers;
using TaskManagementSystem.DTOs.TaskDTOs;
using TaskManagementSystem.Interfaces.ITaskRepo;
using TaskManagementSystem.Mapper.TaskMap;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Test.Controller
{
    public class TaskControllerTests
    {
        private readonly ITaskRepository _taskRepository;
        private readonly UserManager<AppUser> _userManager;

        public TaskControllerTests()
        {
            _taskRepository = A.Fake<ITaskRepository>();
            _userManager = A.Fake<UserManager<AppUser>>();
        }

        [Fact]
        public async Task TaskController_CreateTask_ReturnOK()
        {
            // Arrange
            var taskDto = new TaskDto
            {
                Title = "Test Task",
                DueDate = DateTime.Today.AddDays(1) // Валідна дата
            };

            var user = new AppUser { UserName = "test" };
            var task = taskDto.ToTask(user.Id);

            A.CallTo(() => _userManager.FindByNameAsync(A<string>.Ignored)).Returns(Task.FromResult(user));
            A.CallTo(() => _taskRepository.CreateTask(A<TaskEntity>.Ignored)).Returns(Task.FromResult(task));

            var controller = new TaskController(_taskRepository, _userManager)
            {
                // Імітуємо аутентифікованого користувача
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.Name, user.UserName) }))
                    }
                }
            };

            // Act
            var result = await controller.CreateTask(taskDto);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }
    }
}
