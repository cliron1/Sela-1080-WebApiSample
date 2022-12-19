using System.ComponentModel.DataAnnotations;

public class LoginModel {
    [Required, MinLength(2)]
    public string Username { get; set; }

    [Required, MinLength(4)]
    public string Pwd { get; set; }
}