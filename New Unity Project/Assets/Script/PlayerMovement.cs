using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(AICharacterControl))]
[RequireComponent(typeof (ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{

    [SerializeField] const int walkableLayerNr = 8;
    [SerializeField] const int EnemyLayerNr = 9;


    ThirdPersonCharacter thirdPersonCharacter = null;
    CameraRaycaster cameraRaycaster = null;
    Vector3 CurrentDestination, clickPoint;
    AICharacterControl aiCharacterControl = null;
    GameObject walkTarget = null;


    void Start()
    {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        thirdPersonCharacter = GetComponent<ThirdPersonCharacter>();
        CurrentDestination = transform.position;
        aiCharacterControl = GetComponent<AICharacterControl>();
        cameraRaycaster.notifyMouseClickObservers += ProcessMouseClick;
        walkTarget = new GameObject("walkTarget");
    }

    void ProcessMouseClick(RaycastHit raycastHit, int layerHit)
    {
       switch(layerHit)
        {
            case EnemyLayerNr:
                GameObject enemy = raycastHit.collider.gameObject;
                aiCharacterControl.SetTarget(enemy.transform);
                break;
            case walkableLayerNr:
                walkTarget.transform.position = raycastHit.point;
                aiCharacterControl.SetTarget(walkTarget.transform);
                break;
            default:
                return;

        }
    }
}

