using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class BasicBullet : MonoBehaviour {

    [SerializeField]
    private float time = 1;

    private LineRenderer myRenderer;
    private Material material;

    void Start()
    {
        myRenderer = GetComponent<LineRenderer>();
    }

    //draw the line from the linerenderer
    public void DrawLine(Vector3 _endPoint)
    {

    }

    void Update()
    {
        time = time - Time.deltaTime;
        Color _color = myRenderer.material.color;
        _color.a = time;
        myRenderer.material.color = _color;
        if (time <= 0)
        {
            Destroy(gameObject);
        }
    }

}
