using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MousePointerScript : MonoBehaviour
{
    private Controls controls;

    // Start is called before the first frame update
    void Start()
    {
        // make mousepointer invisible
        //Cursor.visible = false;
        controls = new Controls();
        controls.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        var temp = controls.Gameplay.MousePosition.ReadValue<Vector2>();
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(temp.x, temp.y, 0));
    }
}
