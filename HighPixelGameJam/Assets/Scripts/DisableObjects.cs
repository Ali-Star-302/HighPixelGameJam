using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableObjects : MonoBehaviour
{
    [Tooltip("0 = appear in normal, 1 = appear in parallel")]
    public int world;

    public void NormalToParallel()
    {
        if (world == 0)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }

    public void ParallelToNormal()
    {
        if (world == 0)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
