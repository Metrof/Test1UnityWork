using System.Collections.Generic;

public static class Constants 
{
    public const string MoneyFieldName = "Money";

    public static Dictionary<BildingTypes, string> ButtonKeys = new Dictionary<BildingTypes, string>()
    {
        {BildingTypes.HomeBilding, "Home" },
        {BildingTypes.WorkBilding, "Work" },
        {BildingTypes.ShopBilding, "Shop" }
    };
}
