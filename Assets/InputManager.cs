using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.Interaction.Toolkit.AR;

public class InputManager : ARBaseGestureInteractable
{
    //[SerializeField] private GameObject arObj;
    [SerializeField] private Camera arCam;
    [SerializeField] ARRaycastManager raycastManager;
    [SerializeField] private GameObject crosshair; 

    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private Touch touch;
    private Pose pose;
    // Start is called before the first frame update

    void Start()
    {

    }

    protected override bool CanStartManipulationForGesture(TapGesture gesture)
    {
        if (gesture.targetObject == null)
            return true;
        return false;
    }
    protected override void OnEndManipulation(TapGesture gesture)
    {
        if (gesture.isCanceled)
            return;
        if (gesture.targetObject != null && IsPointerOverUI(gesture))
        {
            return;
        }
        if (GestureTransformationUtility.Raycast(gesture.startPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            GameObject placedObj = Instantiate(DataHandler.Instance.GetFurniture(), pose.position, pose.rotation);

            var anchorObj = new GameObject("PlacementAnchor");
            anchorObj.transform.position = pose.position;
            anchorObj.transform.rotation = pose.rotation;
            placedObj.transform.parent = anchorObj.transform;
        }
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        CrosshairCalculation();

        //Instantiate(DataHandler.Instance.GetFurniture(), pose.position, pose.rotation);

        //Ray ray = arCam.ScreenPointToRay(Input.mousePosition);
        //Instantiate(arObj, pose.position, pose.rotation);
    }
    bool IsPointerOverUI(TapGesture touch) 
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = new Vector2(touch.startPosition.x, touch.startPosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results.Count > 0;
    }

    void CrosshairCalculation()
    {
        Vector3 origin = arCam.ViewportToScreenPoint(new Vector3(0.5f, 0.5f, 0));


        if (GestureTransformationUtility.Raycast(origin, hits,TrackableType.PlaneWithinPolygon))
        {
            pose = hits[0].pose;
            crosshair.transform.position = pose.position;
            crosshair.transform.eulerAngles = new Vector3(90, 0, 0);
        }

    }




}
