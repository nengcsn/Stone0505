using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// VoxelStone Root
/// </summary>
public class VoxelStone : MonoBehaviour
{
    [HideInInspector]
    public List<MeshRenderer> VoxelCubeList = new List<MeshRenderer>();

    [SerializeField]
    public class VoxelStoneEvent : UnityEvent<VoxelStone> { }
    public VoxelStoneEvent OnFinishShow = new VoxelStoneEvent(); //when finishing show all cubes of this voxel stone
    public UnityEvent OnShowCubeOnce = new UnityEvent(); //when finishing show one cube success

    private int m_CurrentIndex = -1; //current index of show cube
    private WaitForSeconds m_WaitForSeconds; //WaitForSeconds temp

    private void Awake()
    {
        VoxelCubeList = GetComponentsInChildren<MeshRenderer>().ToList(); //get cubes in this root        
        HideAllCubes();
    }

    /// <summary>
    /// SetActive false all cube
    /// </summary>
    private void HideAllCubes()
    {
        if (VoxelCubeList == null) //check null
        {
            return;
        }

        VoxelCubeList.ForEach(cube =>
        {
            cube.gameObject.SetActive(false); //hide all cubes
        });
        m_CurrentIndex = -1;
    }

    /// <summary>
    /// Show cube step by step
    /// </summary>
    public void StartShowCubes(float speed)
    {
        if (VoxelCubeList == null) //check null
        {
            return;
        }

        if (m_CurrentIndex >= VoxelCubeList.Count) //if already show all before
        {
            HideAllCubes(); //hide all cubes
        }

        m_WaitForSeconds = new WaitForSeconds(speed);

        StopCoroutine(nameof(ShowCube)); 
        StartCoroutine(nameof(ShowCube)); 
    }

    /// <summary>
    /// Show cube coroutine
    /// </summary>
    /// <returns></returns>
    private IEnumerator ShowCube()
    {
        m_CurrentIndex = 0;

        while (m_CurrentIndex >= 0 && m_CurrentIndex < VoxelCubeList.Count) //check index is valid or not valid and check has finish while loop
        {
            yield return m_WaitForSeconds;

            MeshRenderer currentCube = VoxelCubeList[m_CurrentIndex]; //get cube by index
            currentCube.material.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 1); //random change cube color
            currentCube.gameObject.SetActive(true); // show cube in the index
            m_CurrentIndex++; //move index to next
            OnShowCubeOnce?.Invoke();
        }

        OnFinishShow?.Invoke(this); 
    }

    /// <summary>
    /// if in the progress of showing
    /// </summary>
    /// <returns></returns>
    public bool IsShowingCubes()
    {
        if (VoxelCubeList == null)
        {
            return false;
        }

        return m_CurrentIndex >= 0 && m_CurrentIndex < VoxelCubeList.Count;
    }
}