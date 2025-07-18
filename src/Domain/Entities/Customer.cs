using Business.Entities.Exceptions;
using Business.Entities.Interfaces;
using Domain.ValueObjects;
using System.Diagnostics.CodeAnalysis;

namespace Business.Entities;

public class Customer : IAggregateRoot
{
    private string? _id;
    private string? _name;
    private string? _cpf;
    private string? _email;

    public string Id
    {
        get => _id!;
        set
        {
            CustomerException.ThrowIfNullOrWhiteSpace(value, nameof(Id));

            _id = value;
        }
    }

    public DateTime CreatedAt { get; private set; }

    public required string Name
    {
        get => _name!;
        set
        {
            CustomerException.ThrowIfNullOrWhiteSpace(value, nameof(Name));

            _name = value;
        }
    }

    public required Cpf Cpf
    {
        get => _cpf!;
        set
        {
            CustomerException.ThrowIfNullOrWhiteSpace(value, nameof(Cpf));

            _cpf = value;
        }
    }

    public required Email Email
    {
        get => _email!;
        set
        {
            CustomerException.ThrowIfNullOrWhiteSpace(value, nameof(Email));

            _email = value;
        }
    }

    [SetsRequiredMembers]
    public Customer(string name, Cpf cpf, Email email)
    {
        Name = name;
        Cpf = cpf;
        Email = email;
    }

    public Customer(string id, DateTime createdAt)
    {
        Id = id;
        CreatedAt = createdAt;
    }
}
