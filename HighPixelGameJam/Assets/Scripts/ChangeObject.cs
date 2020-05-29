using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeObject : MonoBehaviour
{
    public GameObject normalAsset;
    public GameObject parallelAsset;

    public void NormalToParallel()
    {
        GameObject newObj = Instantiate(parallelAsset, this.transform.position, this.transform.rotation);
        ChangeObject script = newObj.AddComponent<ChangeObject>();
        script.parallelAsset = this.parallelAsset;
        script.normalAsset = this.normalAsset;
        newObj.transform.SetParent(this.transform.parent);
        Destroy(this.gameObject);
    }

    public void ParallelToNormal()
    {
        GameObject newObj = Instantiate(normalAsset, this.transform.position, this.transform.rotation);
        ChangeObject script = newObj.AddComponent<ChangeObject>();
        script.parallelAsset = this.parallelAsset;
        script.normalAsset = this.normalAsset;
        newObj.transform.SetParent(this.transform.parent);
        Destroy(this.gameObject);
    }
}
