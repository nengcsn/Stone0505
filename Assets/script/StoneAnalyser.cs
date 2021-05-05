using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StoneAnalyser : MonoBehaviour
{
    //Voxelise the mesh
    Stone[] Stones;


    void Start()
    {
        //Find all the stones in my project and create stone objects
        Stones = GameObject.FindGameObjectsWithTag("Stone").Select(s => new Stone(s)).ToArray();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Rotate stones according to the longest directions and voxelise them
            for (int i = 0; i < Stones.Length; i++)
            {
                //Stones[i].PlaceStoneByLongestDirection();
                Stones[i].VoxeliseMesh();
            }
        }
    }
}
