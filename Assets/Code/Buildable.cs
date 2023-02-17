using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buildable : MonoBehaviour
{
    Color unbuilt = Color.red;
    public float heightOffset = 0.5f;
    public bool canBuild = true;
    public bool built;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Buildable buildable))
        {
            if (buildable.canBuild)
            {
                buildable.canBuild = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Buildable buildable))
        {
            if (!buildable.canBuild)
            {
                buildable.canBuild = true;
            }
        }
    }
    private IEnumerator BuildAnimation()
    {
        GetComponent<Renderer>().material.color = Color.yellow;
        yield return new WaitForSeconds(1.0f);
        GetComponent<Renderer>().material.color = Color.white;
        Debug.Log("Object is built!");
    }
    public void Build()
    {
        if (!canBuild)
        {
            Destroy(gameObject);
        }
        else
        {
            built = true;
            StartCoroutine(BuildAnimation());
        }
    }

    private void Update()
    {
        if (built) return;
        GetComponent<Renderer>().material.color = !canBuild ? Color.red : Color.green;
    }
}
