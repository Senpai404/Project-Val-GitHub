using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Player : MonoBehaviour, IDamageable {

    [SerializeField]const int walkableLayerNr = 8;
    [SerializeField]const int EnemyLayerNr = 9;

    [SerializeField] float DamagePerHit = 10f;
    [SerializeField] float minTimePerHit = 0.5f;
    [SerializeField] float maxHealthPoints = 100f;
    [SerializeField] float maxAttackRange = 1.5f;
    [SerializeField] Weapons weaponInUse ;


    float currentHealthPoints;
    CameraRaycaster cameraRaycaster;
    float lastHitTime = 0f;

public float healthAsPercentage { get { return currentHealthPoints / maxHealthPoints; } }

    void Start()
    {
        currentHealthPoints = maxHealthPoints;
        RegisterForMouseClick();
        PutWeaponInHand();
    }

    private void PutWeaponInHand()
    {
        var weaponPreFab = weaponInUse.GetWeaponPreFab();
        GameObject dominanHnad = RequestDominantHand();
        var weapon = Instantiate(weaponPreFab, dominanHnad.transform);
        weapon.transform.localPosition = weaponInUse.gripTransform.localPosition;
        weapon.transform.localRotation = weaponInUse.gripTransform.localRotation;


    }

    private GameObject RequestDominantHand()
    {
        var dominantHand = GetComponentsInChildren<DominantHand>();
        int numberOfDominentHand = dominantHand.Length;
        Assert.AreNotEqual(numberOfDominentHand, 0, "No Dominant Hand Found on player");
        Assert.IsFalse(numberOfDominentHand > 1, "multiple domiant hands");
        return dominantHand[0].gameObject; 
    }

    // TODO Check on Runtime if weapon Has Changed Apply new weapon
    private void RegisterForMouseClick()
    {
        cameraRaycaster = FindObjectOfType<CameraRaycaster>();
        cameraRaycaster.notifyMouseClickObservers += OnMouseClick;
    }

    // TODO refactor to reduse lines;
    void OnMouseClick(RaycastHit raycastHit, int layerHit)
    {
        if(layerHit == EnemyLayerNr)
        {
            var enemy = raycastHit.collider.gameObject;

            if((enemy.transform.position - transform.position).magnitude > maxAttackRange)
            {
                return;
            }
            var enemyComponent = enemy.GetComponent<Enemy>();
            if (Time.time - lastHitTime > minTimePerHit)
            {
                enemyComponent.TakeDamage(DamagePerHit);
                lastHitTime = Time.time;
            }
        }

    }

     public void TakeDamage(float damage)
    {
        currentHealthPoints = Mathf.Clamp(currentHealthPoints - damage, 0f, maxHealthPoints);
    }
}
