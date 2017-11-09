using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePopup : MonoBehaviour {
    private Vector3 target;
    private Vector3 screenCoord;
    public int damageValue;    

    public float contentWidth = 100;
    public float contentHeight = 50;
    public GUIStyle style;

    private Vector2 point;

    public float lifeTime = 1.5f;

	// Use this for initialization
	void Start () {
        style.fontSize = 50;
        style.richText = true;
        style.normal.textColor = Color.red;
        InitCoord();
        StartCoroutine(Free());
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.up * 5f * Time.deltaTime);
        InitCoord();
    }

    private void OnGUI()
    {
        if (screenCoord.z > 0)
        {
            GUI.Label(new Rect(point.x, point.y, contentWidth, contentHeight), damageValue.ToString(),style);
        }
    }

    void InitCoord()
    {
        target = transform.position;
        screenCoord = Camera.main.WorldToScreenPoint(target);
        point = new Vector2(screenCoord.x, Screen.height - screenCoord.y);

        contentWidth = Screen.width / 5;
        contentHeight = Screen.height / 5;
    }

    IEnumerator Free()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(this.gameObject);
    }
}
