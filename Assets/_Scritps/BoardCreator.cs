using UnityEngine;
using System.Collections;

public class BoardCreator : MonoBehaviour
{
    public int width = 1;
    public int height = 1;
    public Transform boxPrefab;
    private Transform box;

    // Use this for initialization
    void Start()
    {
        int i, j;
        for (i = 0; i < height; i++)
        {
            for (j = 0; j < width; j++)
            {
                box = Instantiate(boxPrefab, new Vector3(j * 0.64f, i * 0.64f, 0.5f), Quaternion.identity) as Transform;
                box.parent = this.transform;
            }
        }
    }
}
