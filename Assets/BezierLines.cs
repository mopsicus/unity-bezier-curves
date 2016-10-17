using UnityEngine;
using System.Collections.Generic;

public class BezierLines : MonoBehaviour {

	[SerializeField]
	private int segmentsCount = 100;
	private List<GameObject> _curves;

	void Awake () {
		_curves = new List<GameObject> ();
	}

	public void AddCurve (Vector3[] points, Color? startColor = null, Color? endColor = null, float startWidth = 0.2f, float endWidth = 0.02f) {
		Color beginColor = startColor ?? Color.red;
		Color finishColor = endColor ?? Color.red;		
		GameObject line = new GameObject ("Line-"+_curves.Count);
		line.transform.position = Vector3.zero;
		line.transform.SetParent(transform);
		line.transform.localPosition = Vector3.zero;
		line.transform.localScale = Vector3.one;
		line.AddComponent<RectTransform>();
		line.AddComponent<LineRenderer> ();
		LineRenderer render = line.GetComponent<LineRenderer> ();
		render.useWorldSpace = false;
		render.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
		render.SetColors (beginColor, finishColor);
		render.SetWidth (startWidth, endWidth);
		render.SetVertexCount (segmentsCount);
    	for (int i = 0; i < segmentsCount; i++) {
      		float t = (float)i / (float)(segmentsCount - 1);
      		Vector3 point = CalculateBezierPoint (t, points[0], points[1], points[2], points[3]);
			render.SetPosition (i, point);
    	}		
    	_curves.Add (line);
	}

	public void RemoveCurves () {
		_curves.Clear();
		int childs = transform.childCount;
		for (int i = childs - 1; i >= 0; i--)
			Destroy (transform.GetChild (i).gameObject);
	}

	Vector3 CalculateBezierPoint (float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3) {
		float u = 1 - t;
		float tt = t * t;
		float uu = u * u;
		float uuu = uu * u;
		float ttt = tt * t;
		Vector3 p = uuu * p0;
		p += 3 * uu * t * p1;
		p += 3 * u * tt * p2;
		p += ttt * p3;
		return p;
	}	

}
