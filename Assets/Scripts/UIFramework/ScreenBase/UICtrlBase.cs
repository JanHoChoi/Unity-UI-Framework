using UnityEngine;

public class UICtrlBase : MonoBehaviour
{
    [HideInInspector]
    public Canvas ctrlCanvas;

    void Awake()
    {
        ctrlCanvas = GetComponent<Canvas>();
    }
     void OnDestroy()
    {
        
    }
}