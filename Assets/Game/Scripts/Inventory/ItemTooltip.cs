using System;
using TMPro;
using UnityEngine;

[Serializable]
public class ItemTooltipDataStruct
{
    public TextMeshProUGUI name;
    public TextMeshProUGUI description;
}

public class ItemTooltip : FloatingWindow
{

    [SerializeField] private ItemTooltipDataStruct itemTooltipDataStruct;

    public override void Show(Vector3 position, EquipmentItems data)
    {
        base.Show(position, data);
        
        itemTooltipDataStruct.name.text = data.Name;
        itemTooltipDataStruct.description.text = data.Size.ToString();
    }

    public void ClearFields()
    {
        itemTooltipDataStruct.name.text = "";
        itemTooltipDataStruct.description.text = "";
    }
}
