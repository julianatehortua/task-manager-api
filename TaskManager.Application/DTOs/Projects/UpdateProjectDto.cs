using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Application.DTOs.Projects;

public class UpdateProjectDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }
}