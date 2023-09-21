using System.ComponentModel;

namespace Business.Constants;

public enum VehicleStatus
{
    [Description("None")]
    None,
    [Description("Müsait")]
    Available,
    [Description("Görevde")]
    OnDuty,
}