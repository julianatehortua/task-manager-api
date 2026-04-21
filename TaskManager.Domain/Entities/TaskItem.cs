using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Domain.Entities;

public enum TaskStatus { Pending, InProgress, Completed }

public class TaskItem
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public TaskStatus Status { get; set; } = TaskStatus.Pending;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public int ProjectId { get; set; }
    public Project Project { get; set; } = null!;

    public int? AssignedUserId { get; set; }
    public User? AssignedUser { get; set; }
}
