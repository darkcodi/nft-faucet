using NftFaucet.Plugins.Models.Enums;

namespace NftFaucet.Plugins.Models;

public class ConfigurationItem
{
    public UiDisplayType DisplayType { get; set; }
    public string Name { get; set; }
    public string Value { get; set; }
    public string Placeholder { get; set; }
    public bool IsDisabled { get; set; }

    // for buttons only
    public Action ClickAction { get; set; }
}
