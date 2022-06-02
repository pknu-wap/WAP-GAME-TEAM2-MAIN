using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    MeshRenderer mesh;
    public bool buildable;
    [SerializeField] Material buildable_Cell;
    [SerializeField] Material notBuildable_Cell;
    [SerializeField] Material default_Cell;
    private void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        buildable = true;
    }
   /* private void OnTriggerEnter(Collider other)
    {
        if (!BuildManager.Instance.IsTouch) return;

        if(other.tag == "Turret")
        {
            Color color = mesh.material.color;
            color = Color.yellow;
            color.a = 0.3f;
            mesh.material.color = color;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (!BuildManager.Instance.IsTouch) return;

        if (other.tag == "Turret")
        {
            Color color = mesh.material.color;
            color.a = 0f;
            mesh.material.color = color;
        }
    }*/
}
