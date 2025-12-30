using UnityEngine;

[CreateAssetMenu(menuName = "BuffsDebuffs/Buff/HealthUp")]
public class HealthUpBuff : Buff
{
    /*public override void Add()
    {
        buffdebuffController.Characteristics.Current.health += increaseValue;
    }

    public override void Sub()
    {
        buffdebuffController.Characteristics.Current.health -= increaseValue;
    }*/

    public override void Add(BuffDebuffController controller)
    {
        base.Add(controller);
        //controller.Characteristics.Current.health += increaseValue;
    }

    public override void Remove(BuffDebuffController controller)
    {
        base.Remove(controller);
        //controller.Characteristics.Current.health -= increaseValue;
    }
}
