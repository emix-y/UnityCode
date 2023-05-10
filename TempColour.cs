using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempColour : MonoBehaviour
{

    public FirestoreGet FireData;

    private Renderer rndr; 
    // Start is called before the first frame update
    void Start()
    {
        rndr.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float humid = FireData.Humid;
        
    }
}
