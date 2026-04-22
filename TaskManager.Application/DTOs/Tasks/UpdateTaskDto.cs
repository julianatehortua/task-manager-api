using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Application.DTOs.Tasks;

public class UpdateTaskDto
{
    public Domain.Entities.TaskStatus Status { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}