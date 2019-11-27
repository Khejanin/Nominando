using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : Item
{

    public enum ConsumableEffect
    {
        Heal,
        Strength,
        Endurance,
        Intelligence,
        Speed,
        Arcana
    }

    ConsumableEffect effect;
    int effectiveness;

    public Consumable(string itemName, int value, ConsumableEffect effect) : base(itemName, value)
    {
        this.effect = effect;
    }
   
    public override void use(Entity useOn)
    {
        if(effect == ConsumableEffect.Heal)
        {
            useOn.hp -= effectiveness;
        }
        else if(effect == ConsumableEffect.Strength)
        {
            useOn.stats.Strength += effectiveness;
        }
        else if (effect == ConsumableEffect.Speed)
        {
            useOn.stats.Speed += effectiveness;
        }
        else if (effect == ConsumableEffect.Endurance)
        {
            useOn.stats.Endurance += effectiveness;
        }
        else if (effect == ConsumableEffect.Intelligence)
        {
            useOn.stats.Intelligence += effectiveness;
        }
        else if (effect == ConsumableEffect.Arcana)
        {
            useOn.stats.Arcana += effectiveness;
        }
    }

}
