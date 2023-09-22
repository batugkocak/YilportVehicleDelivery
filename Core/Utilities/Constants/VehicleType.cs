// ReSharper disable IdentifierTypo
// ReSharper disable InconsistentNaming
namespace Business.Constants;
//TODO: Kapalı Kasa Kamyonet/ Açık Kasa Kamyonet / İtfaiye Arazöz should be added.
public enum VehicleType
{
    None,
    Otomobil,
    Ticari,
    Transit,
    Ambulans,
    Traktör,
    İtfaiye,
    Kamyonet
}
// public enum VehicleType
// {
//     [Description("None")]
//     None,
//     [Description("Ticari")]
//     Commercial,
//     [Description("Otomobil")]
//     Car,
//     [Description("Transit")]
//     Transit,
//     [Description("Ambulans")]
//     Ambulance,
// }