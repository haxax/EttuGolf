using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfClubPC : GolfClub
{
    protected override void InputUpdate()
    {
        if (Input.GetMouseButton(0)) { Hold(Camera.main.ScreenToWorldPoint(Input.mousePosition)); }
        if (Input.GetMouseButtonUp(0)) { Release(Camera.main.ScreenToWorldPoint(Input.mousePosition)); }
    }
}
