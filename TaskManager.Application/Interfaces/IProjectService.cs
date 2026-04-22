using System;
using System.Collections.Generic;
using System.Text;

using TaskManager.Application.DTOs.Projects;

namespace TaskManager.Application.Interfaces;

public interface IProjectService
{
    Task<List<ProjectResponseDto>> GetAllAsync(int userId);
    Task<ProjectResponseDto> CreateAsync(CreateProjectDto dto, int userId);
    Task<ProjectResponseDto> UpdateAsync(int id, UpdateProjectDto dto, int userId);
    Task DeleteAsync(int id, int userId);
}