using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helpers
{
    /// <summary>
    /// Gets the mouse position in 3D space by mapping it onto the given plane.
    /// </summary>
    /// <param name="plane">The plane to map the mouse position to</param>
    /// <returns>The position on the plane that the mouse corresponds to. Vector3.zero if no position can be found.</returns>
    public static Vector3 GetMousePosition3D(Plane plane)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float point = 0f;
        if (plane.Raycast(ray, out point))
        {
            return ray.GetPoint(point);
        }
        return Vector3.zero;
    }
}
