using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
	// Start is called before the first frame update
	//void DrawLine(Vector3 start, Vector3 end, Color color, float duration = 0.2f)
	//{
	//	GameObject myLine = new GameObject();
	//	myLine.transform.position = start;
	//	myLine.AddComponent<LineRenderer>();
	//	LineRenderer lr = myLine.GetComponent<LineRenderer>();
	//	lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
	//	lr.SetColors(color, color);
	//	lr.SetWidth(0.1f, 0.1f);
	//	lr.SetPosition(0, start);
	//	lr.SetPosition(1, end);
	//	GameObject.Destroy(myLine, duration);
	//}

	// Update is called once per frame
	void Update()
    {
		Debug.DrawLine(Vector3.zero, new Vector3(5, 0, 0), Color.white);
		Debug.DrawLine(Vector3.zero, new Vector3(5, 5, 0), Color.white);
        //Debug.DrawLine(new Vector3(1, 0, 0), new Vector3(1, 1, 0, Color.white);
    }
}
