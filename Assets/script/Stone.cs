using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    GameObject _gotStone;
    Vector3 _centerOfGravity;
    Vector3 _stoneNormal;
    List<Voxel> _voxels;
    float _longestLength;
    List<NormalGroup> _stoneNormals;
    float _weight;

    List<Stone> neighbours;
    List<SpringJoint> joints;

}
