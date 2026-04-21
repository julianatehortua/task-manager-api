using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Application.DTOs.Tasks;

public class CreateTaskDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int ProjectId { get; set; }
    public int? AssignedUserId { get; set; }
}