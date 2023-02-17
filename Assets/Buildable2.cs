using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buildable2 : MonoBehaviour
{
    private Renderer renderer;
    public float heightOffset = 0.5f;
    private void Awake()
    {
        renderer = GetComponent<Renderer>();
    }
    private IEnumerator BuildColorChange()
    {
        renderer.material.color = Color.yellow;
        yield return new WaitForSeconds(1f);
        renderer.material.color = Color.white;
    }
    public void Build()
    {
        StartCoroutine(BuildColorChange());
        Debug.Log("This Object is Built");
    }
}
