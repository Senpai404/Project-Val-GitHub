using UnityEngine;

public class EnemyUI : MonoBehaviour {

    // Work around Unity 5.5's lack of nested prefabs
    [Tooltip("The UI canvas prefab")]
    [SerializeField]
    GameObject enemyCanvasPrefab = null;

    Camera cameraToLookAt;

    void Start()
    {
        cameraToLookAt = Camera.main;
        Instantiate(enemyCanvasPrefab, transform.position, Quaternion.identity, transform);
    }


    void LateUpdate()
    {
        transform.LookAt(cameraToLookAt.transform);
    }
}