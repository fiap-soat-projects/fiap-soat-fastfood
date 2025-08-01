using System.ComponentModel;

namespace Business.Entities.Enums;
public enum PaymentStatus
{
    [Description("None")]
    None = 0,
    [Description("Pending")]
    Pending = 1,
    [Description("Authorized")]
    Authorized = 2,
    [Description("Refused")]
    Refused = 3,
}
