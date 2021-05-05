using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StoneAnalyser : MonoBehaviour
{
    //Voxelise the mesh
    MeshFilter[] Stones;

    public void VoxeliseMesh(MeshFilter Stone)
    {
        //Get bounds of the mesh
        //divide bounds into voxelsize
        //create voxelgrid in the bounds of the mesh
        //Check which voxels are inside the mesh
        //set the voxels active
        Mesh stone = Stone.GetComponent<MeshFilter>().mesh;
        Vector3 centerPoint = stone.bounds.center;
        Vector3 extents = stone.bounds.extents;
        Debug.Log(extents);
        float r = Mathf.Min(extents.x, extents.y, extents.z) / 10;
        for (float x = -extents.x; x <= extents.x; x += r)
        {
            for (float y = -extents.y; y <= extents.y; y += r)
            {
                for (float z = -extents.z; z <= extents.z; z += r)
                {
                    if (IsPointInCollider(Stone.GetComponent<MeshCollider>(), Stone.transform.position + Stone.transform.TransformVector(new Vector3(x, y, z))))
                    {
                        GameObject Voxel = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        Voxel.transform.SetParent(Stone.transform);
                        Voxel.transform.localEulerAngles = Vector3.zero;
                        Voxel.transform.localPosition = centerPoint + new Vector3(x, y, z);
                        Voxel.transform.localScale = Vector3.one * r;
                        Destroy(Voxel.GetComponent<Collider>());
                    }
                }
            }
        }
        Stone.GetComponent<MeshRenderer>().enabled = false;
 
    }

    void Start()
    {
        Stones = FindObjectsOfType<MeshFilter>();
        Invoke("InvokeVoxeliseMesh", 2);
    }
    void InvokeVoxeliseMesh()
    {
        for (int i = 0; i < Stones.Length; i++)
        {
            VoxeliseMesh(Stones[i]);
        }
    }

     public Vector3 GetCenterOfGravity(List<Voxel>stoneVoxel)
    {
        //add the center position of each voxel
        //divide result by the amount of voxel

        return Vector3.zero;

    }



    //public static bool IsPointInCollider(Collider cld, Vector3 point)
    //{

    //    Vector3 direction = new Vector3(0, 1, 0);

    //    if (Physics.Raycast(point, direction, Mathf.Infinity) &&
    //        Physics.Raycast(point, -direction, Mathf.Infinity))
    //    {
    //        return true;
    //    }

    //    else return false;
    //}

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
