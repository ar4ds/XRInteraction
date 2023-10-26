using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class SceneMgr : MonoBehaviour
{
    InputDevice leftHand, rightHand;
    XRRayInteractor leftRay, rightRay;
    public XRGrabInteractable GrabObj;
    // Start is called before the first frame update
    void Start()
    {
        leftHand = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        rightHand = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        leftRay = GameObject.Find("XR Origin/Camera Offset/LeftHand Controller").GetComponent<XRRayInteractor>();
        rightRay = GameObject.Find("XR Origin/Camera Offset/RightHand Controller").GetComponent<XRRayInteractor>();
    }

    // Update is called once per frame
    void Update()
    {
        if (leftHand.isValid)
        {
            bool gb = leftHand.TryGetFeatureValue(CommonUsages.gripButton, out bool gvalue);
            Debug.Log($"gripButton # {gb}#{gvalue}");
            leftRay.interactionManager.SelectEnter((IXRSelectInteractor)leftRay, GrabObj);
            bool tb = leftHand.TryGetFeatureValue(CommonUsages.triggerButton, out bool tvalue);
            Debug.Log($"triggerButton # {tb}#{tvalue}");
        }
    }
}
