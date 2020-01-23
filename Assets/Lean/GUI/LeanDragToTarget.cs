using System;
using System.Collections;
using System.Collections.Generic;
using Lean.Gui;
using UnityEngine;

public class LeanDragToTarget : MonoBehaviour
{

    public float x, y;

    public bool isOccupied;

    public bool canOnlyHoldOne;
    
    /*private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("DragBox"))
        {
            other.gameObject.GetComponent<LeanDrag>().hoverOverObject = gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("DragBox"))
        {
            var hoverOverObject = other.gameObject.GetComponent<LeanDrag>().hoverOverObject;
            if(hoverOverObject != null && hoverOverObject == gameObject) other.gameObject.GetComponent<LeanDrag>().hoverOverObject = null;
        }
    }
*/
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
