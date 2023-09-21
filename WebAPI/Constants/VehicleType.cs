using System.ComponentModel;

namespace Business.Constants;

//TODO: Düzeltilecek.
public enum VehicleType
{
    [Description("None")]
    None,
    [Description("Ticari")]
    Commercial,
    [Description("Otomobil")]
    Car,
    [Description("Transit")]
    Transit,
    [Description("Ambulans")]
    Ambulance,
}