using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private GraphicRaycaster rayCaster;
    private PointerEventData pData;
    private EventSystem eventSystem;
    public Transform SelectionPoint;

    private static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIManager>();
            }
            return instance;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        rayCaster = GetComponent<GraphicRaycaster>();
        eventSystem = GetComponent<EventSystem>();
        pData = new PointerEventData(eventSystem);

        pData.position = SelectionPoint.position;
    }

  
    public bool onEntered(GameObject button)
    {
        List<RaycastResult> results = new List<RaycastResult>();
        rayCaster.Raycast(pData, results);
        foreach(RaycastResult result in results)
        {
            if(result.gameObject == button)
            {
                return true;
            }

        }
        return false;
    }
}
