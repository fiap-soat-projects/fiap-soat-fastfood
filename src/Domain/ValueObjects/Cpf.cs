using Business.ValueObjects.Exceptions;
using System.Text.RegularExpressions;

namespace Domain.ValueObjects;

public readonly partial struct Cpf
{
    const short CPF_LENGTH = 11;

    public string Number { get; }

    public static implicit operator Cpf(string value) => new(value);
    public static implicit operator string(Cpf cpf) => cpf.Number;

    [GeneratedRegex(@"[^\d]")]
    private static partial Regex CpfReplaceRegex();

    [GeneratedRegex(@"^\d{11}$")]
    private static partial Regex CpfRegex();

    public Cpf(string number)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace("Cpf cannot be null or white space");

        number = CpfReplaceRegex().Replace(number, string.Empty);

        if (number.Length != CPF_LENGTH || CpfRegex().IsMatch(number) is false)
        {
            throw new InvalidCpfException(number);
        }

        Number = number;
    }

    public override string ToString()
    {
        return Number;
    }
}
