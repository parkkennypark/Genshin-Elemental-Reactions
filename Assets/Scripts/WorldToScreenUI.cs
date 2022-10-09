using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldToScreenUI : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    private RectTransform rt;
    private CanvasGroup canvasGroup;

    private void Start()
    {
        rt = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

    }

    public void Setup(Transform target, Vector3 offset)
    {
        transform.SetParent(GameObject.Find("World UI Parent").transform);

        this.target = target;
        this.offset = offset;
    }

    void LateUpdate()
    {
        Vector3 targetPos = target.position + offset;

        float distance = Vector3.Distance(Camera.main.transform.position, targetPos);
        canvasGroup.alpha = distance < 12 ? 1 : 0;

        Vector2 pos = Camera.main.WorldToScreenPoint(targetPos);
        rt.position = pos;
    }

    public Vector3 GetWorldPositionOnPlane(Vector3 screenPosition, float z)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        Plane xy = new Plane(Vector3.forward, new Vector3(0, 0, z));
        float distance;
        xy.Raycast(ray, out distance);
        return ray.GetPoint(distance);
    }
}
