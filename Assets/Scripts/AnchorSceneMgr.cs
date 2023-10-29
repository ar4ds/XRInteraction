using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class AnchorSceneMgr
{
    static XRInteractionManager xriMgr;
    // ´´½¨Ãªµã
    public static void CreateAnchor(IXRSelectInteractor selectItor, Color color)
    {
        var tmpGo = GameObject.CreatePrimitive(PrimitiveType.Cube);
        tmpGo.GetComponent<MeshRenderer>().material.color = color;
        var grabObj = tmpGo.AddComponent<XRGrabInteractable>();
        grabObj.throwOnDetach = false;
        if (xriMgr)
            xriMgr = Object.FindObjectOfType<XRInteractionManager>();
        xriMgr.SelectEnter(selectItor, grabObj);
        var ray = (XRRayInteractor)selectItor;
        ray.attachTransform.position = ray.transform.position + ray.transform.forward.normalized;
    }
}
