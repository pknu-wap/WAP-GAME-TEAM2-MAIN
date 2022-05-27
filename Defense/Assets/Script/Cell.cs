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
}
