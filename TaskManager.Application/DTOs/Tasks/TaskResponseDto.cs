using System;
using System.Collections.Generic;
using System.Text;

using TaskManager.Domain.Entities;

namespace TaskManager.Application.DTOs.Tasks;

public class TaskResponseDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Domain.Entities.TaskStatus Status { get; set; }
    public string StatusName => Status.ToString();
    public DateTime CreatedAt { get; set; }
    public int ProjectId { get; set; }
    public string? AssignedUserName { get; set; }
}