using System;
using System.Collections.Generic;
using System.Text;

using TaskManager.Application.DTOs.Tasks;

namespace TaskManager.Application.Interfaces;

public interface ITaskService
{
    Task<List<TaskResponseDto>> GetByProjectAsync(int projectId, int userId);
    Task<TaskResponseDto> CreateAsync(CreateTaskDto dto, int userId);
    Task<TaskResponseDto> UpdateStatusAsync(int taskId, UpdateTaskStatusDto dto, int userId);
    Task DeleteAsync(int taskId, int userId);
}