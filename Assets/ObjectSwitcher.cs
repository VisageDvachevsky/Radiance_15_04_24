using UnityEngine;
using UnityEngine.UI;

public class ObjectSwitcher : MonoBehaviour
{
    public GameObject[] objectsToSpawn;
    private ARPlacement _arPlacement;

    void Start()
    {
        _arPlacement = FindObjectOfType<ARPlacement>();
        if (_arPlacement == null)
        {
            Debug.LogError("ARManager not found in scene.");
            return;
        }
    }

    public void SwitchObject(GameObject obj)
    {
        _arPlacement.PlaceObject(obj);
    }
}
