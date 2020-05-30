using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColour : MonoBehaviour
{
    public Color normalColour;
    public Color parallelColour;

    public void NormalToParallel()
    {
        GetComponent<Renderer>().material.SetColor("_Color", parallelColour);
    }

    public void ParallelToNormal()
    {
        GetComponent<Renderer>().material.SetColor("_Color", normalColour);
    }
}
