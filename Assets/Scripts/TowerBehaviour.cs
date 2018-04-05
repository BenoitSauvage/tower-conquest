using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBehaviour : MonoBehaviour {

    void Update()
    {
        Vector3 pos = transform.position;
        Vector3 scale = transform.localScale;

        if (pos.y < -10 || scale.y < 0.1)
        {
            Destroy(this.gameObject);
        }
    }

    void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MakeCubes();
        }
    }

    void MakeCubes()
    {

        Vector3 scale = transform.localScale;
        Vector3 pos = GetComponent<Renderer>().bounds.center;
        Vector3 lowPoint = GetComponent<Renderer>().bounds.min;
        Vector3 highPoint = GetComponent<Renderer>().bounds.max;

        float startPos = (highPoint.y - lowPoint.y);

        Vector3 explosionCenter = new Vector3(pos.x, pos.y, pos.z);
        Vector3[] posArray = new Vector3[4];

        posArray[0] = new Vector3(startPos, 1f, -startPos);
        posArray[1] = new Vector3(-startPos, 1f, startPos);
        posArray[2] = new Vector3(startPos, 1f, startPos);
        posArray[3] = new Vector3(-startPos, 1f, -startPos);

        for (int i = 0; i < 4; i++)
        {

            GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);

            obj.transform.localScale = (scale * .5f);
            obj.transform.localPosition = (pos + posArray[i]);

            float forceScale = (Random.Range(100.0f, 1000.0f));
            float forceRadius = (Random.Range(100.0f, 300.0f));

            Rigidbody temp = obj.AddComponent<Rigidbody>();
            temp.AddExplosionForce(forceScale, explosionCenter, forceRadius);
            temp.SetDensity(obj.transform.localScale.x);
            obj.AddComponent<TowerBehaviour>();
        }
        Destroy(this.gameObject);
    }
}
