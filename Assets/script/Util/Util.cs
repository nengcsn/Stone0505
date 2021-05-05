using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util 
{
    public static bool IsPointInCollider(Collider collider, Vector3 point)
    {
        var bounds = collider.bounds;

        if (!bounds.Contains(point)) //is point contains in the box
        {
            return false;
        }
        else
        {
            RaycastHit raycasthit;
            var upraycast = new Ray(point, Vector3.up);
            var downraycast = new Ray(point, Vector3.down);

            if (collider.Raycast(upraycast, out raycasthit, float.MaxValue) || collider.Raycast(downraycast, out raycasthit, float.MaxValue))
            {
                return false;
            }
            else
            {
                float updirection, downdirection;
                bounds.IntersectRay(upraycast, out updirection);
                bounds.IntersectRay(downraycast, out downdirection);

                Vector3 upoint = upraycast.GetPoint(-updirection * 1.5f);
                Vector3 dpoint = downraycast.GetPoint(-downdirection * 1.5f);

                if (collider.Raycast(new Ray(upoint, Vector3.down), out raycasthit, float.MaxValue)
                    || collider.Raycast(new Ray(dpoint, Vector3.up), out raycasthit, float.MaxValue))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

    }
}
