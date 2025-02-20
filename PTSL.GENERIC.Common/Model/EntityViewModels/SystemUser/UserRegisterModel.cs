﻿using System.ComponentModel.DataAnnotations;

namespace PTSL.SQTC.Common.Model.EntityViewModels.SystemUser;

public class UserRegisterModel
{
    [Required(ErrorMessage = "Email is Required")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Username is Required")]
    public string? UserName { get; set; }

    [Required(ErrorMessage = "Password is Required")]
    public string? Password { get; set; }
}