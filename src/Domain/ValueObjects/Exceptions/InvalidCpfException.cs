using Business.Exceptions;
using System.Diagnostics.CodeAnalysis;

namespace Business.ValueObjects.Exceptions;

[ExcludeFromCodeCoverage]
public class InvalidCpfException : DomainException
{
    const string INVALID_CPF_MESSAGE_TEMPLATE = "The CPF {0} is invalid";

    public InvalidCpfException(string cpf) : base(string.Format(INVALID_CPF_MESSAGE_TEMPLATE, cpf))
    {

    }
}
