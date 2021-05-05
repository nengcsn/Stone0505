using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public float Speed = 0.01f;
    public List<VoxelStone> VoxelStoneList = new List<VoxelStone>();

    [Space]
   /// public Text VoxelStone1CountTxt; 
   /// public Text VoxelStone2CountTxt; 
   /// public Text VoxelStone3CountTxt; 
   /// public Text VoxelStone4CountTxt; 
    

    private int m_CurrentIndex = 0; //current index of show stone
    private int m_CubeCount = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            ShowVoxelStone(); //start show voxel stone
        }
    }

    /// <summary>
    /// show voxel stone step by step
    /// </summary>
    private void ShowVoxelStone()
    {
        if (m_CurrentIndex >= 0 && m_CurrentIndex < VoxelStoneList.Count) //check index valid or not
        {
            VoxelStone currentStone = VoxelStoneList[m_CurrentIndex]; //find voxel stone by index
            if (currentStone.IsShowingCubes()) 
            {
                return;
            }

            m_CubeCount = 0; 
            currentStone.OnShowCubeOnce.AddListener(OnShowCubeOnce);
            currentStone.OnFinishShow.AddListener(OnFinishShow); //add listener of finish show event to show next
            currentStone.StartShowCubes(Speed); 
        }
    }

    /// <summary>
    /// when show one cube successfully
    /// </summary>
    private void OnShowCubeOnce()
    {
        m_CubeCount++;
        switch (m_CurrentIndex)
        {
            case 0:
                {
              ///      VoxelStone1CountTxt.text = "VoxelStone1: " + m_CubeCount.ToString();
                }
                break;
            case 1:
                {
             ///       VoxelStone2CountTxt.text = "VoxelStone2: " + m_CubeCount.ToString();
                }
                break;
            case 2:
                {
              ///      VoxelStone3CountTxt.text = "VoxelStone3: " + m_CubeCount.ToString();
                }
                break;
            case 3:
                {
              ///      VoxelStone4CountTxt.text = "VoxelStone4: " + m_CubeCount.ToString();
                }
                break;
         
            
          
        }
    }

    /// <summary>
    /// when a voxel stone finish show
    /// </summary>
    /// <param name="voxelStone"></param>
    private void OnFinishShow(VoxelStone voxelStone)
    {
        if (voxelStone == null)
        {
            return;
        }

        int index = VoxelStoneList.FindIndex(stone => stone.GetInstanceID() == voxelStone.GetInstanceID()); //find the index of this finished voxel stone
        if (index >= 0 && index < VoxelStoneList.Count) //check index valid or not
        {
            m_CurrentIndex++; //move index to next           
            ShowVoxelStone(); //show next voxel stone
        }
    }
}