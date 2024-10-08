using System.Diagnostics.CodeAnalysis;

namespace OrderManagement.Business.Validations.Constants;

public static class Expressions
{
    [StringSyntax("Regex")] 
    public const string UkrainianPhoneNumber = @"^\+?3?8?(0\d{9})$";
}