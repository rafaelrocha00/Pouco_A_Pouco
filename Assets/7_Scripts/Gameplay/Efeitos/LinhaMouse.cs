using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinhaMouse : MonoBehaviour
{
    LineRenderer lr;
    Queue<Vector3> pontos = new Queue<Vector3>();
    Camera c;
    Vector3 p;
    int pontosQuantidade;
    private void Start()
    {
        lr = GetComponent<LineRenderer>();
        c = GameManager.instancia.GetComponent<C_Camera>().GetCamera();
        pontosQuantidade = lr.positionCount;
    }

    private void Update()
    {
        SetPoints();
    }

    public void SetPoints()
    {
        p = c.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, c.nearClipPlane + 0.1f));
        transform.position = p;
        pontos.Enqueue(p);
        if (pontos.Count > pontosQuantidade)
        {
           pontos.Dequeue();
        }
        lr.SetPositions(pontos.ToArray());
    }
}
