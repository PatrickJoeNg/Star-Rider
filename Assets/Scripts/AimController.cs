using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimController : MonoBehaviour
{

    [SerializeField] RectTransform outerCrosshair;
    [SerializeField] RectTransform innerCrosshair;

    [SerializeField] Transform playerTarget;

    [SerializeField] Camera cam;

    RectTransform canvasRect;

    // Start is called before the first frame update
    void Start()
    {
        canvasRect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {

        ProcessCrosshair();
    }

    private void ProcessCrosshair()
    {
        Vector2 viewportPoint = cam.WorldToViewportPoint(playerTarget.transform.position);

        Vector2 screenPoint = cam.WorldToScreenPoint(playerTarget.transform.position);

        canvasRect.position = screenPoint;
    }
}
