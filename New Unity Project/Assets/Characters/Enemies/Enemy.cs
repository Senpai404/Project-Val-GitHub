using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] float maxHealthPoints = 100f;
    [SerializeField] float attackRaduis = 1f;
    [SerializeField] float ChaseRaduis = 5f;

    float currentHealthPoints;

    AICharacterControl aiCharacterControl = null;
    GameObject player = null;

    public float healthAsPercentage
    {get{ return currentHealthPoints / maxHealthPoints;}}

    void Start()
    {
        currentHealthPoints = maxHealthPoints;
        player = GameObject.FindGameObjectWithTag("Player");
        aiCharacterControl = GetComponent<AICharacterControl>();
    }

    public void TakeDamage(float damageToTake)
    {
        currentHealthPoints = Mathf.Clamp(currentHealthPoints - damageToTake, 0f, maxHealthPoints);
        if(currentHealthPoints <=0) { Destroy(gameObject); }
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

        if(distanceToPlayer <= ChaseRaduis)
        {
            aiCharacterControl.SetTarget(player.transform);
        }
        else
        {
          // TODO shoot /hit target
           
        }

        if (distanceToPlayer <= attackRaduis)
        {
            // not attack

        }
        else
        {
            
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRaduis);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, ChaseRaduis);

    }


}
