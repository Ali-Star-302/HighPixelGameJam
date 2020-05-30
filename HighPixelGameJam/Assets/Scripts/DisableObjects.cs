using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableObjects : MonoBehaviour
{
    [Tooltip("0 = appear in normal, 1 = appear in parallel")]
    public int world;

    List<MeshRenderer> mrList = new List<MeshRenderer>();
    List<SkinnedMeshRenderer> smrList = new List<SkinnedMeshRenderer>();
    List<Collider> cList = new List<Collider>();

    private void Start()
    {
        if (GetComponent<MeshRenderer>())
        {
            mrList.Add(GetComponent<MeshRenderer>());
        }
        else
        {
            foreach(Transform child in this.transform)
            {
                if (child.GetComponent<MeshRenderer>())
                {
                    mrList.Add(child.GetComponent<MeshRenderer>());
                }
                else if (child.GetComponent<SkinnedMeshRenderer>())
                {
                    smrList.Add(child.GetComponent<SkinnedMeshRenderer>());
                }
            }
        }

        if (GetComponent<Collider>())
        {
            cList.Add(GetComponent<Collider>());
        }
        else
        {
            foreach (Transform child in this.transform)
            {
                if (child.GetComponent<Collider>())
                {
                    cList.Add(child.GetComponent<Collider>());
                }
            }
        }

        if (world == 1)
        {
            ParallelToNormal();
        }
    }

    public void NormalToParallel()
    {
        if (world == 0)
        {
            foreach(MeshRenderer m in mrList)
            {
                m.enabled = false;
            }
            foreach (SkinnedMeshRenderer sm in smrList)
            {
                sm.enabled = false;
            }
            foreach (Collider c in cList)
            {
                c.enabled = false;
            }
        }
        else
        {
            foreach (MeshRenderer m in mrList)
            {
                m.enabled = true;
            }
            foreach (SkinnedMeshRenderer sm in smrList)
            {
                sm.enabled = true;
            }
            foreach (Collider c in cList)
            {
                c.enabled = true;
            }
        }
    }

    public void ParallelToNormal()
    {
        if (world == 0)
        {
            foreach (MeshRenderer m in mrList)
            {
                m.enabled = true;
            }
            foreach (SkinnedMeshRenderer sm in smrList)
            {
                sm.enabled = true;
            }
            foreach (Collider c in cList)
            {
                c.enabled = true;
            }
        }
        else
        {
            foreach (MeshRenderer m in mrList)
            {
                m.enabled = false;
            }
            foreach (SkinnedMeshRenderer sm in smrList)
            {
                sm.enabled = false;
            }
            foreach (Collider c in cList)
            {
                c.enabled = false;
            }
        }
    }
}
