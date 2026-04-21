using System;
using System.Collections.Generic;
using System.Text;

using TaskManager.Domain.Entities;

namespace TaskManager.Application.DTOs.Tasks;

public class UpdateTaskStatusDto
{
    public Domain.Entities.TaskStatus Status { get; set; }
}