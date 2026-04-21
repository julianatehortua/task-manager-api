using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Application.DTOs.Common;

public class TokenResponseDto
{
    public string Token { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}