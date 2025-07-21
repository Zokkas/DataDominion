using System;
using System.Collections.Generic;

[Serializable]
public class LocalizationData
{
    public LocalizedItem[] items;
}

[Serializable]
public class LocalizedItem
{
    public string key;
    public string value;
}
