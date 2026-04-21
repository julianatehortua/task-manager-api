using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Application.DTOs.Auth;
using TaskManager.Application.DTOs.Common;

namespace TaskManager.Application.Interfaces;

public interface IAuthService
{
    Task<TokenResponseDto> RegisterAsync(RegisterDto dto);
    Task<TokenResponseDto> LoginAsync(LoginDto dto);
}