using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;
using TaskManager.Application.DTOs.Tasks;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Entities;
using TaskManager.Infrastructure.Persistence;

namespace TaskManager.Infrastructure.Services;

public class TaskService : ITaskService
{
    private readonly AppDbContext _context;

    public TaskService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<TaskResponseDto>> GetByProjectAsync(int projectId, int userId)
    {
        var project = await _context.Projects
            .FirstOrDefaultAsync(p => p.Id == projectId && p.OwnerId == userId)
            ?? throw new Exception("Proyecto no encontrado.");

        return await _context.Tasks
            .Where(t => t.ProjectId == projectId)
            .Include(t => t.AssignedUser)
            .Select(t => new TaskResponseDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Status = t.Status,
                CreatedAt = t.CreatedAt,
                ProjectId = t.ProjectId,
                AssignedUserName = t.AssignedUser != null ? t.AssignedUser.Name : null
            })
            .ToListAsync();
    }

    public async Task<TaskResponseDto> CreateAsync(CreateTaskDto dto, int userId)
    {
        var project = await _context.Projects
            .FirstOrDefaultAsync(p => p.Id == dto.ProjectId && p.OwnerId == userId)
            ?? throw new Exception("Proyecto no encontrado.");

        var task = new TaskItem
        {
            Title = dto.Title,
            Description = dto.Description,
            ProjectId = dto.ProjectId,
            AssignedUserId = dto.AssignedUserId,
            Status = Domain.Entities.TaskStatus.Pending
        };

        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();

        var assignedUser = dto.AssignedUserId.HasValue
            ? await _context.Users.FindAsync(dto.AssignedUserId.Value)
            : null;

        return new TaskResponseDto
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            Status = task.Status,
            CreatedAt = task.CreatedAt,
            ProjectId = task.ProjectId,
            AssignedUserName = assignedUser?.Name
        };
    }
    public async Task<TaskResponseDto> UpdateAsync(int taskId, UpdateTaskDto dto, int userId)
    {
        var task = await _context.Tasks
            .Include(t => t.Project)
            .Include(t => t.AssignedUser)
            .FirstOrDefaultAsync(t => t.Id == taskId && t.Project.OwnerId == userId)
            ?? throw new Exception("Tarea no encontrada.");

        task.Title = dto.Title;
        task.Description = dto.Description;
        task.Status = dto.Status;
        await _context.SaveChangesAsync();

        return new TaskResponseDto
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            Status = task.Status,
            CreatedAt = task.CreatedAt,
            ProjectId = task.ProjectId,
            AssignedUserName = task.AssignedUser?.Name
        };
    }   

    public async Task DeleteAsync(int taskId, int userId)
    {
        var task = await _context.Tasks
            .Include(t => t.Project)
            .FirstOrDefaultAsync(t => t.Id == taskId && t.Project.OwnerId == userId)
            ?? throw new Exception("Tarea no encontrada.");

        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();
    }
}