using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.UI;

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

    private void CalHand(ref InputDevice hand, XRRayInteractor rayIt, XRNode node)
    {
        if (!hand.isValid)
            hand = InputDevices.GetDeviceAtXRNode(node);
        else
            GetInputAction(hand, rayIt);
        rayIt.enabled = hand.isValid;
    }

    private void GetInputAction(InputDevice hand, XRRayInteractor rayIt)
    {
        if (hand.TryGetFeatureValue(CommonUsages.grip, out float gv) && gv > .5f
            && rayIt.TryGetUIModel(out TrackedDeviceModel model))
            if (model.currentRaycast.isValid && model.currentRaycast.gameObject.transform.parent.TryGetComponent<AnchorItem>(out var item))
                if (!((IXRSelectInteractor)rayIt).hasSelection)
                    AnchorSceneMgr.CreateAnchor(rayIt, item.color);
    }

    // Update is called once per frame
    void Update()
    {
        CalHand(ref leftHand, leftRay, XRNode.LeftHand);
        CalHand(ref rightHand, rightRay, XRNode.RightHand);
    }
}
