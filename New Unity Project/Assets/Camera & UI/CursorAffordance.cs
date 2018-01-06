using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CameraRaycaster))]
public class CursorAffordance: MonoBehaviour {

    [SerializeField] Vector2 cursourHotspot = new Vector2(2f,2f);
    [SerializeField] Texture2D WalkCoursor = null;
    [SerializeField] Texture2D Enemy = null;
    [SerializeField] Texture2D CantDoAnything = null;

    [SerializeField] const int walkableLayerNr = 8;
    [SerializeField] const int EnemyLayerNr = 9;

    CameraRaycaster cameraRaycaster;



	// Use this for initialization
	void Start () {

        cameraRaycaster = GetComponent<CameraRaycaster>();
        cameraRaycaster.notifyLayerChangeObservers += OnLayerChange;
    }
	
    void OnLayerChange(int layerInt)
    {


        switch (layerInt)
        {
            case walkableLayerNr:
                Cursor.SetCursor(WalkCoursor, cursourHotspot, CursorMode.Auto);
                break;
            case EnemyLayerNr:
                Cursor.SetCursor(Enemy, cursourHotspot, CursorMode.Auto);
                break;
            default:
                Cursor.SetCursor(CantDoAnything, cursourHotspot, CursorMode.Auto);
                return;
        }
    }
}
