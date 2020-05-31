using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeObject : MonoBehaviour
{
    public GameObject normalAsset;
    public GameObject parallelAsset;
    public Vector3 normalAssetScale;
    public Vector3 parallelAssetScale;
    public bool isParallelAlready = false;

    Vector3 defaultPosition;
    bool isFirst = true;

    private void Start()
    {
        if (isFirst)
        {
            defaultPosition = transform.position;
        }
            
        if (normalAssetScale == Vector3.zero)
            normalAssetScale = this.transform.localScale;
        if (isParallelAlready)
            ParallelToNormal();
    }

    public void NormalToParallel()
    {
        GameObject newObj = Instantiate(parallelAsset, this.transform.position, this.transform.rotation);

        //Move/scale/rotate new object
        if (parallelAssetScale != Vector3.zero)
            newObj.transform.localScale = parallelAssetScale;

        //Set script variables
        ChangeObject script = newObj.AddComponent<ChangeObject>();
        script.parallelAsset = this.parallelAsset;
        script.normalAsset = this.normalAsset;
        script.normalAssetScale = this.normalAssetScale;
        script.parallelAssetScale = this.parallelAssetScale;
        script.isFirst = false;

        newObj.transform.SetParent(this.transform.parent);
        Destroy(this.gameObject);
    }

    public void ParallelToNormal()
    {
        GameObject newObj = Instantiate(normalAsset, this.transform.position, this.transform.rotation);

        //Move/scale new object
        if (normalAssetScale != Vector3.zero)
            newObj.transform.localScale = normalAssetScale;

        //Set script variables
        ChangeObject script = newObj.AddComponent<ChangeObject>();
        script.parallelAsset = this.parallelAsset;
        script.normalAsset = this.normalAsset;
        script.normalAssetScale = this.normalAssetScale;
        script.parallelAssetScale = this.parallelAssetScale;
        script.defaultPosition = this.defaultPosition;
        script.isFirst = false;

        newObj.transform.SetParent(this.transform.parent);
        Destroy(this.gameObject);
    }
}
