using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.PolySpatial.InputDevices;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using UnityEngine.InputSystem.LowLevel;


public class ColorChangeOnInput : MonoBehaviour
{
    private GameObject selectedObject;
    private Vector3 lastPosition;

    [SerializeField]
    Transform m_InputAxisTransform;
    
    void Start()
    {
        EnhancedTouchSupport.Enable();
        TouchSimulation.Enable();
    }
    void OnEnable()
    {
        EnhancedTouchSupport.Enable();
        TouchSimulation.Enable();
    }


    void Update()
    {
        var activeTouches = Touch.activeTouches;
        print(activeTouches.Count);
        if(activeTouches.Count > 0)
        {
            foreach (var touch in activeTouches)
            {
                SpatialPointerState touchData = EnhancedSpatialPointerSupport.GetPointerState(touch);
                if (touchData.targetObject != null )//&& touchData.Kind != SpatialPointerKind.Touch
                {
                    ChangeObjectColor(touchData.targetObject);
                    if(touch.phase == UnityEngine.InputSystem.TouchPhase.Began)
                    {
                        selectedObject = touchData.targetObject;
                        lastPosition = touchData.interactionPosition;
                    }
                    else if (touch.phase == UnityEngine.InputSystem.TouchPhase.Moved && selectedObject != null)
                    {
                        Vector3 deltaPosition = touchData. interactionPosition - lastPosition;
                        selectedObject.transform.position += deltaPosition;
                        lastPosition = touchData.interactionPosition;
                    }
                }

                    m_InputAxisTransform.SetPositionAndRotation(touchData.interactionPosition, touchData.inputDeviceRotation);
            }
        }
        if(Touch.activeTouches.Count == 0)
        {	
            selectedObject = null;
        }
    }
    void ChangeObjectColor(GameObject obj)
    {
        Renderer objRenderer=obj.GetComponent<Renderer>();
        if(objRenderer != null)
        {
            objRenderer.material.color =new Color(Random.value,Random.value,Random.value);
        }
    }
}
