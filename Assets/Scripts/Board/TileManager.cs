using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{

    private Material _greenMaterial;
    private Material _redMaterial;
    private Material _transparentMaterial;
    
    // Start is called before the first frame update
    void Start()
    {
        _greenMaterial = Resources.Load("Materials/Selected", typeof(Material)) as Material;
        _redMaterial = Resources.Load("Materials/Error", typeof(Material)) as Material;      
        _transparentMaterial = Resources.Load("Materials/InvisibleFace", typeof(Material)) as Material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseOver()
    {
        gameObject.GetComponent<MeshRenderer>().material = _greenMaterial;
    }
    
    private void OnMouseExit()
    {
        //gameObject.GetComponent<MeshRenderer>().material = _transparentMaterial;
    }
}
