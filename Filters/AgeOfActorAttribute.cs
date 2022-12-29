using System;
using AutoMapper;

namespace MoviesApp.Filters;
using System.ComponentModel.DataAnnotations;


public class AgeOfActorAttribute : ValidationAttribute
{
    public AgeOfActorAttribute(int minAge,int maxAge)
    {
        MinAge = minAge;
        MaxAge = maxAge;
    }
    public int MinAge { get; }
    public int MaxAge { get; }

    public string GetErrorMessage() =>
        $"Actors must have birth date between {DateTime.Now.AddYears(-MaxAge).ToShortDateString()} and {DateTime.Now.AddYears(-MinAge).ToShortDateString()}.";

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var birthYear = (DateTime) value;
        //Console.WriteLine("YearYearYear"+birthYear);
        if (birthYear < DateTime.Now.AddYears(-MaxAge).Date || birthYear > DateTime.Now.AddYears(-MinAge).Date)
        {
            return new ValidationResult(GetErrorMessage());
        }
        return ValidationResult.Success;
    }
}