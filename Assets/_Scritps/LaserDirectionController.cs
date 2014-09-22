using UnityEngine;
using System.Collections;

public class LaserDirectionController : MonoBehaviour
{
    public float speed = 5f;

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector2.up * Time.deltaTime * speed);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.transform.rotation = Quaternion.Euler(0, 0, 90f + this.transform.rotation.eulerAngles.z);
        }
        Debug.Log(this.transform.rotation.eulerAngles.z);
    }
}
