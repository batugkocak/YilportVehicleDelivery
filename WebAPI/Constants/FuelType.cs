using System.ComponentModel;

namespace Business.Constants;

public enum FuelType
{
    [Description("None")]
    None,
    [Description("Benzin")]
    Gasoline,
    [Description("Dizel")]
    Diesel,
    [Description("LPG")]
    Lpg,
    [Description("Elektrikli")]
    Electric,
    [Description("Hibrit")]
    Hybrid,
}  
    