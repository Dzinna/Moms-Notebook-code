using System.ComponentModel;

namespace MomsNotebook.Models.Enums
{
    public enum ToothNumber
    {
        [Description("Pirmas")]
        First = 1,
        [Description("Antras")]
        Second,
        [Description("Trečias")]
        Third,
        [Description("Ketvirtas")]
        Fourth,
        [Description("Penktas")]
        Fifth,
        [Description("Šeštas")]
        Sixth,
        [Description("Septintas")]
        Seventh,
        [Description("Aštuntas")]
        Eighth
    }
}
