using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable {

    [SerializeField]const int walkableLayerNr = 8;
    [SerializeField]const int EnemyLayerNr = 9;

    [SerializeField] float DamagePerHit = 10f;
    [SerializeField] float minTimePerHit = 0.5f;
    [SerializeField] float maxHealthPoints = 100f;
    [SerializeField] float maxAttackRange = 1.5f;

    GameObject currentTarget;
    float currentHealthPoints;
    CameraRaycaster cameraRaycaster;
    float lastHitTime = 0f;

public float healthAsPercentage { get { return currentHealthPoints / maxHealthPoints; } }

    void Start()
    {
        currentHealthPoints = maxHealthPoints;
        cameraRaycaster = FindObjectOfType<CameraRaycaster>();
        cameraRaycaster.notifyMouseClickObservers += OnMouseClick;
    }

    void OnMouseClick(RaycastHit raycastHit, int layerHit)
    {
        if(layerHit == EnemyLayerNr)
        {
            var enemy = raycastHit.collider.gameObject;

            if((enemy.transform.position - transform.position).magnitude > maxAttackRange)
            {
                return;
            }
            currentTarget = enemy;
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
