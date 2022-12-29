using System;
using System.ComponentModel.DataAnnotations;
using MoviesApp.Filters;

namespace MoviesApp.ViewModels;

public class InputActorViewModel
{
    [Required]
    [MinLength(4,ErrorMessage = "Введено менее 4 символов")]
    public string FirstName { get; set; }
    [Required]
    [MinLength(4,ErrorMessage = "Введено менее 4 символов")]
    public string LastName { get; set; }
    [Required]
    [DataType(DataType.Date)]
    [AgeOfActor(4,99)]
    public DateTime BirthDate { get; set; }
}