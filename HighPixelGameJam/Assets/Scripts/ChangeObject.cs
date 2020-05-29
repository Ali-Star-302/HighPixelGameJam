using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeObject : MonoBehaviour
{
    public GameObject normalAsset;
    public GameObject parallelAsset;

    public void NormalToParallel()
    {
        Instantiate(parallelAsset, normalAsset.transform.position, normalAsset.transform.rotation);
        Destroy(normalAsset);
    }

    public void ParallelToNormal()
    {
        Instantiate(normalAsset, parallelAsset.transform.position, parallelAsset.transform.rotation);
        Destroy(parallelAsset);
    }
}
