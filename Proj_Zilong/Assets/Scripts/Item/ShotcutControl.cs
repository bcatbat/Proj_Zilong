using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShotcutControl : MonoBehaviour {
    private Grid shortcutItem;

	// Use this for initialization
	void Start () {
        UIEventListener listener = gameObject.AddComponent<UIEventListener>();
        listener.OnMouseDrop += Listener_OnMouseDrop;        
	}

    private void Listener_OnMouseDrop(GameObject gb)
    {
        // gb是拖拽的物体
        if(gb.GetComponent<Grid>() != null)
        {
            Debug.Log(gb);
           // shortcutItem = gameObject.AddComponent<Item>();
            //shortcutItem = gb.GetComponent<Item>();
            //
            
        }
    }
    
}
