using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DrawLine : MonoBehaviour
{
    public Vector3 PrevMousePos;
    public Vector3 WorldPosition;
    public float MouseDelta = 5;
    public bool Pressed;
    [Space]
    public LineRenderer Line0;

    private void Update()
    {
        Pressed = Input.GetMouseButton(0);

        if (Pressed)
        {
            WorldPosition = TapPos();// Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));

            if (Vector2.Distance(PrevMousePos, WorldPosition) > MouseDelta)
            {
                Get3dMousePoint();
            }
        }
    }

    void Get3dMousePoint()
    {
        WorldPosition = TapPos();// Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
        PrevMousePos = WorldPosition;

        if (WorldPosition == Vector3.zero)
            return;

        Line0.positionCount++;
        int t = Line0.positionCount - 1;
        Line0.SetPosition(t, WorldPosition);

        print(WorldPosition);
    }

    Vector3 TapPos()
    {
        Vector3 tapPos = new Vector3();

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            tapPos = hit.point;
            tapPos.y = 0.7f;
        }

        return tapPos;
    }
}