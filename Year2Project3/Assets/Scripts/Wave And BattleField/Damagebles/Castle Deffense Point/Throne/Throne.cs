﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Throne : CastleDeffensePoint
{
    public override void TakeDamage(float damage)
    {
        int goldToSteal = (Mathf.RoundToInt(damage) / ResourceManager.goldPerPhysicalCoin);

        if (ResourceManager.instance.goldPrefabsInScene.Count >= goldToSteal)
        {
            ResourceManager.instance.RemoveGold(Mathf.RoundToInt(damage));
        }
        else if(ResourceManager.instance.goldPrefabsInScene.Count > 0)
        {
            damage = Mathf.Abs(ResourceManager.instance.goldPrefabsInScene.Count - Mathf.RoundToInt(damage));
            ResourceManager.instance.RemoveGold(ResourceManager.instance.goldPrefabsInScene.Count);
            myStats.health.currentValue -= damage;
        }
        else
        {
            myStats.health.currentValue -= damage;

            if (myStats.health.currentValue <= 0)
            {
                StartCoroutine(UIManager.instance.GameOver());
            }
        }

        if (healthbarFill != null)
        {
            healthbarFill.fillAmount = (myStats.health.currentValue / myStats.health.baseValue);
        }
    }
}
