using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.Interaction.Toolkit.AR;

public class ARPlacementInteractableSingle : ARBaseGestureInteractable
{
    [SerializeField]
    [Tooltip("A GameObject to place when a raycast from a user touch hits a plane.")]
    private GameObject placementPrefab;

    [SerializeField]
    [Tooltip("Callback event executed after object is placed.")]
    private ARObjectPlacementEvent onObjectPlaced;

    private GameObject placementObject;

    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private static GameObject trackablesObject;

    protected override bool CanStartManipulationForGesture(TapGesture gesture)
    {
        
        if (gesture.TargetObject == null || gesture.TargetObject.layer == 9)
            return true;

        return false;
    }

    protected override void OnEndManipulation(TapGesture gesture)
    {
        if (gesture.WasCancelled)
            return;

       
        if (gesture.TargetObject != null && gesture.TargetObject.layer != 9)
            return;

        
        if (GestureTransformationUtility.Raycast(gesture.StartPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            var hit = hits[0];

           
            if (Vector3.Dot(Camera.main.transform.position - hit.pose.position, hit.pose.rotation * Vector3.up) < 0)
                return;

            if (placementObject == null)
            {
                placementObject = Instantiate(placementPrefab, hit.pose.position, hit.pose.rotation);

              
                var anchorObject = new GameObject("PlacementAnchor");
                anchorObject.transform.position = hit.pose.position;
                anchorObject.transform.rotation = hit.pose.rotation;
                placementObject.transform.parent = anchorObject.transform;

               
                if (trackablesObject == null)
                    trackablesObject = GameObject.Find("Trackables");
                if (trackablesObject != null)
                    anchorObject.transform.parent = trackablesObject.transform;

                onObjectPlaced?.Invoke(this, placementObject);
            }
        }
    }
}