using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;
using TaskManager.Application.DTOs.Projects;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Entities;
using TaskManager.Infrastructure.Persistence;

namespace TaskManager.Infrastructure.Services;

public class ProjectService : IProjectService
{
    private readonly AppDbContext _context;

    public ProjectService(AppDbContext context)
    {
        _context = context;
    }
      

    public async Task<List<ProjectResponseDto>> GetAllAsync(int userId)
    {
        return await _context.Projects
            .Where(p => p.OwnerId == userId)
            .Select(p => new ProjectResponseDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                CreatedAt = p.CreatedAt,
                TaskCount = p.Tasks.Count
            })
            .ToListAsync();
    }

    public async Task<ProjectResponseDto> CreateAsync(CreateProjectDto dto, int userId)
    {
        var project = new Project
        {
            Name = dto.Name,
            Description = dto.Description,
            OwnerId = userId
        };

        _context.Projects.Add(project);
        await _context.SaveChangesAsync();

        return new ProjectResponseDto
        {
            Id = project.Id,
            Name = project.Name,
            Description = project.Description,
            CreatedAt = project.CreatedAt,
            TaskCount = 0
        };
    }
    public async Task<ProjectResponseDto> UpdateAsync(int id, UpdateProjectDto dto, int userId)
    {
        var project = await _context.Projects
            .Include(p => p.Tasks)
            .FirstOrDefaultAsync(p => p.Id == id && p.OwnerId == userId)
            ?? throw new Exception("Proyecto no encontrado.");

        if (dto.Name != null) project.Name = dto.Name;
        if (dto.Description != null) project.Description = dto.Description;

        await _context.SaveChangesAsync();

        return new ProjectResponseDto
        {
            Id = project.Id,
            Name = project.Name,
            Description = project.Description,
            CreatedAt = project.CreatedAt,
            TaskCount = project.Tasks.Count
        };
    }
    public async Task DeleteAsync(int id, int userId)
    {
        var project = await _context.Projects
            .FirstOrDefaultAsync(p => p.Id == id && p.OwnerId == userId)
            ?? throw new Exception("Proyecto no encontrado.");

        _context.Projects.Remove(project);
        await _context.SaveChangesAsync();
    }
}