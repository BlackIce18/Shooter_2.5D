using UnityEngine;

[CreateAssetMenu(menuName = "BuffsDebuffs/Buff/HealthUp")]
public class HealthUpBuff : Buff
{
    public float increaseValue;
    public override void Add()
    {
        buffdebuffController.Characteristics.Current.health += increaseValue;
    }

    public override void Sub()
    {
        buffdebuffController.Characteristics.Current.health -= increaseValue;
    }
}
