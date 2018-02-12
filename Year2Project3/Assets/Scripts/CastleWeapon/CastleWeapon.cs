﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleWeapon : MonoBehaviour 
{

    public enum WeaponType
    {
        Ballista,
        Catapult,
        Canon
    }
    public WeaponType weaponType;

    public enum Side
    {
        Left,
        Right
    }
    public Side side;

    public string weaponName;
    public int weaponLevel;
    public int buildCost;
    public Stat upgradeCost;

    protected bool usingWeapon;
    protected bool shooting;

    public Transform rotatableWeapon;
    public GameObject projectile;
    public List<Transform> projectileSpawns = new List<Transform>();
    public int amountOfProjectiles = 1;
    protected Transform mouseObject;

    public GameObject useUI;

    [Header("Properties")]
    public Stat damage;
    public Stat force;
    [Space(10)]
    public Stat coolDown;
    private float nextTimeToFire;

    [Header("Rotation")]
    public float maxXRotation;
    public float minXRotation;
    [Space(10)]
    public LayerMask mouseLayerMask;

    public virtual void Awake()
    {
        mouseObject = GameObject.FindWithTag("MouseObject").transform;
    }

    public virtual void Update()
    {
        if (usingWeapon)
        {
            //if (Input.GetMouseButton(0) && Time.time >= nextTimeToFire)
            //{
            //    nextTimeToFire = Time.time + coolDown.currentValue;
            //    Shoot();
            //}

            if (shooting)
            {
                if (Time.time >= nextTimeToFire)
                {
                    nextTimeToFire = Time.time + coolDown.currentValue;
                    Shoot();
                }
            }

            if (Input.GetButtonDown("Cancel"))
            {
                StopUsing();
            }
        }
    }

    public virtual void Shoot()
    {

    }

    public void Upgrade()
    {

    }

    public void ToggleAutoShoot()
    {
        shooting = !shooting;
    }

    public virtual void StartUsing()
    {
        usingWeapon = true;
        GameManager.instance.StartUsingWeapon(this);

        useUI.SetActive(true);
    }

    public virtual void StopUsing()
    {
        usingWeapon = false;
        GameManager.instance.StopUsingWeapon();

        shooting = false;

        useUI.SetActive(false);
    }
}
