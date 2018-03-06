﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CastleRoom_Minions : CastleRoom 
{

    [Header("Minion Room Setup")]
    public GameObject minionToSpawn;
    public Transform minionSpawnPoint;

    [Header("Minion Room Stats")]
    public Stat spawnCost;
    public Stat amountToSpawnPerBuy;
    public float spawnInterval;
    public float spawnPointOffsetRandomizer;

    private bool canSpawn = true;
    private int currentAmountToSpawn;

    [Header("UI")]
    public TextMeshProUGUI roomTypeText;
    public TextMeshProUGUI roomMinionCostText;

    public override void Update()
    {
        base.Update();

        if (currentAmountToSpawn > 0)
        {
            if (canSpawn)
            {
                StartCoroutine(SpawnMinions());
            }
        }
    }

    public override void SetupUI()
    {
        base.SetupUI();

        roomTypeText.text = "Type: <color=green>" + roomType + "</color>";
        roomMinionCostText.text = "Spawn Cost: " + spawnCost.currentValue;

        upgradeStatsText.text = "Spawn cost: " + spawnCost.currentValue + " (<color=green>" + CastleUpgradeManager.instance.CheckPositiveOrNegative(spawnCost.increaseValue) + "</color>)" + "\n" +
                                "Spawns per buy: " + amountToSpawnPerBuy .currentValue + " (<color=green>" + CastleUpgradeManager.instance.CheckPositiveOrNegative(amountToSpawnPerBuy.increaseValue) + "</color>)";
    }

    public override void UseRoom()
    {
        base.UseRoom();

        if (ResourceManager.instance.goldPrefabsInScene.Count >= spawnCost.currentValue)
        {
            currentAmountToSpawn += (int)amountToSpawnPerBuy.currentValue;

            ResourceManager.instance.RemoveGold((int)spawnCost.currentValue);
        }
    }

    public IEnumerator SpawnMinions()
    {
        canSpawn = false;

        Vector3 spawnOffset = new Vector3(Random.Range(minionSpawnPoint.position.x - spawnPointOffsetRandomizer, minionSpawnPoint.position.x + spawnPointOffsetRandomizer),
                                          minionSpawnPoint.position.y,
                                          Random.Range(minionSpawnPoint.position.z - spawnPointOffsetRandomizer, minionSpawnPoint.position.z + spawnPointOffsetRandomizer));

        GameObject newMinion = ObjectPooler.instance.GrabFromPool("Ally Knight", Vector3.zero + spawnOffset,Quaternion.Euler(Vector3.zero));
        newMinion.GetComponent<AudioSource>().pitch = Random.Range(0.75f, 1.25f);
        newMinion.GetComponent<AudioSource>().volume = Random.Range(0.01f, 0.08f);
        newMinion.transform.SetParent(null);

        WaveManager.instance.alliesInScene.Add(newMinion.GetComponent<Allie>());

        currentAmountToSpawn--;

        yield return new WaitForSeconds(spawnInterval);

        canSpawn = true;
    }

    public override void Upgrade()
    {
        if (ResourceManager.instance.goldPrefabsInScene.Count < upgradeCost.currentValue || roomLevel >= maxRoomLevel)
        {
            return;
        }

        base.Upgrade();

        spawnCost.currentValue += spawnCost.increaseValue;
        amountToSpawnPerBuy.currentValue += amountToSpawnPerBuy.increaseValue;

        SetupUI();
    }
}
