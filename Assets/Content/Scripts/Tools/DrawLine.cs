using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DrawLine : MonoBehaviour
{
    public Vector3 PrevMousePos;
    public Vector3 WorldPosition;
    public float MouseDelta = 5;
    public bool Pressed;
    public bool CanDrawLine = false;
    private bool CanTapPlayer = true;
    [Space]
    public Player Player;
    public LineRenderer Line;
    public List<Vector3> LinePoses;

    private void Update()
    {
        if (!CanDrawLine && CanTapPlayer)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.Equals(Player.gameObject))
                {
                    CanDrawLine = true;
                    CanTapPlayer = false;
                }
            }
        }

        if (CanDrawLine)
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

            if (Input.GetMouseButtonUp(0))
            {
                if (CanDrawLine)
                {
                    OnEndDrawLine();
                }
                CanDrawLine = false;
            }
        }
    }

    void OnEndDrawLine()
    {
        Player.Move(LinePoses);
    }

    void Get3dMousePoint()
    {
        WorldPosition = TapPos();// Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
        PrevMousePos = WorldPosition;

        if (WorldPosition == Vector3.zero)
            return;

        Line.positionCount++;
        int t = Line.positionCount - 1;
        Line.SetPosition(t, WorldPosition);
        LinePoses.Add(WorldPosition);

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