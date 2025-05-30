using Domain.Entities.Enums;
using Domain.Entities.Exceptions;
using Domain.Entities.Interfaces;

namespace Domain.Entities;

public class Order : IAggregateRoot
{
    private string? _id;
    private string? _customerId;
    private string? _customerName;
    private IEnumerable<OrderItem> _items = [];
    private OrderStatus _status;
    private PaymentMethod _paymentMethod;
    private decimal _totalPrice;

    public string Id
    {
        get => _id!;
        set
        {
            OrderException.ThrowIfNullOrWhiteSpace(value, nameof(Id));

            _id = value;
        }
    }

    public string? CustomerId
    {
        get => _customerId;
        set => _customerId = value;
    }

    public string? CustomerName
    {
        get => _customerName;
        set => _customerName = value;
    }

    public OrderStatus Status
    {
        get => _status;
        set
        {
            ValidateStatus(value);

            _status = value;
        }
    }

    public PaymentMethod PaymentMethod
    {
        get => _paymentMethod;
        set => _paymentMethod = value;
    }

    public decimal TotalPrice
    {
        get => _totalPrice;
        set
        {
            OrderException.ThrowIfIsEqualOrLowerThanZero(value, nameof(TotalPrice));

            _totalPrice = value;
        }
    }

    public IEnumerable<OrderItem> Items
    {
        get => _items;
        set => _items = value;
    }

    internal Order(
        string id,
        string? customerId,
        string? customerName,
        IEnumerable<OrderItem> items,
        OrderStatus status,
        PaymentMethod paymentMethod,
        decimal totalPrice)
    {
        Id = id;
        CustomerId = customerId;
        CustomerName = customerName;
        Items = items;
        Status = status;
        PaymentMethod = paymentMethod;
        TotalPrice = totalPrice;
    }

    internal Order(
        string? customerId,
        string? customerName,
        IEnumerable<OrderItem> items)
    {
        CustomerId = customerId;
        CustomerName = customerName;
        Items = items;
        TotalPrice = SumItems(items);
        Status = OrderStatus.Pending;
        PaymentMethod = PaymentMethod.None;
    }

    private static void ValidateStatus(OrderStatus status)
    {
        var isInvalidCategory = !Enum.IsDefined(typeof(OrderStatus), status) || status == OrderStatus.None;

        if (isInvalidCategory)
        {
            throw new OrderItemException(nameof(OrderStatus));
        }
    }

    private static decimal SumItems(IEnumerable<OrderItem> items)
    {
        decimal total = 0;

        foreach (var item in items)
        {
            total += item.GetTotalPrice();
        }

        return total;
    }
}
