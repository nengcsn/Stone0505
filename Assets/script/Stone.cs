using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Stone
{
    GameObject _goStone;
    Vector3 _centerOfGravity;
    Vector3 _stoneNormal;
    //List<Voxel> _voxels;
    float _longestLength;
    List<NormalGroup> _stoneNormals;
    float _weight;
    float _voxelSize = 2f;
    private GameObject[,,] _voxels;
    public Vector3 Normal;


    List<Stone> neighbours;
    List<SpringJoint> joints;

    public Stone(GameObject goStone)
    {
        _goStone = goStone;
    }
    public void VoxeliseMesh()
    {
        //Get bounds of the mesh
        //divide bounds into voxelsize
        //create voxelgrid in the bounds of the mesh
        //Check which voxels are inside the mesh
        //set the voxels active

        //Make a variable that store stoneMesh.bounds
        Mesh stoneMesh = _goStone.GetComponent<MeshFilter>().mesh;
        Vector3 centerPoint = stoneMesh.bounds.center;
        Vector3 extents = stoneMesh.bounds.extents;
        Debug.Log(extents);

        int gridX = Mathf.CeilToInt(stoneMesh.bounds.size.x / _voxelSize);
        int gridY = Mathf.CeilToInt(stoneMesh.bounds.size.y / _voxelSize);
        int gridZ = Mathf.CeilToInt(stoneMesh.bounds.size.z / _voxelSize);

        //float r = Mathf.Min(extents.x, extents.y, extents.z) / 10;
        _voxels = new GameObject[gridX, gridY, gridZ];
        for (int x = 0; x < gridX; x++)
        {
            for (int y = 0; y < gridY; y++)
            {
                for (int z = 0; z < gridZ; z++)
                {
                    Vector3 localPosition = stoneMesh.bounds.min + (new Vector3(x, y, z) * _voxelSize);
                    if (Util.IsPointInCollider(_goStone.GetComponent<MeshCollider>(), _goStone.transform.position + _goStone.transform.TransformVector(localPosition)))
                    {
                        GameObject voxel = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        voxel.transform.SetParent(_goStone.transform);
                        voxel.transform.localEulerAngles = Vector3.zero;
                        voxel.transform.localPosition = localPosition;
                        voxel.transform.localScale = Vector3.one * _voxelSize;
                        GameObject.Destroy(voxel.GetComponent<Collider>());

                        _voxels[x, y, z] = voxel;
                    }
                }
            }
        }
        _goStone.GetComponent<MeshRenderer>().enabled = false;


        GetStoneNormal();
    }

    public void GetStoneNormal()
    {
        List<GameObject> voxelList = new List<GameObject>();

        for (int x = 0; x < _voxels.GetLength(0); x++)
            for (int y = 0; y < _voxels.GetLength(1); y++)
                for (int z = 0; z < _voxels.GetLength(2); z++)
                {
                    if (_voxels[x, y, z] != null) voxelList.Add(_voxels[x, y, z]);
                }



        Vector3 longestLine = Vector3.zero;
        for (int i = 0; i < voxelList.Count; i++)
        {
            for (int j = 0; j < voxelList.Count; j++)
            {
                Vector3 line = voxelList[i].transform.position - voxelList[j].transform.position;
                if (line.magnitude > longestLine.magnitude)
                {
                    longestLine = line;
                }
            }
        }

        Debug.Log($"{longestLine} is the stones longest line");
        RotateStoneToX(longestLine);
    }

    public void RotateStoneToX(Vector3 normal)
    {
        _goStone.transform.rotation = Quaternion.FromToRotation(normal.normalized, Vector3.right);
    }

    public Vector3 GetCenterOfGravity(List<Voxel> stoneVoxel)
    {
        //add the center position of each voxel
        //divide result by the amount of voxel

        return Vector3.zero;
    }

}
