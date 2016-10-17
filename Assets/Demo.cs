using UnityEngine;
using System.Collections;

public class Demo : MonoBehaviour {

	[SerializeField]
	private BezierLines lines;
	private int _curveCount;
	private Vector3[] _curvePoints;

	void Awake () {
		_curveCount = -1;
		_curvePoints = new Vector3[4];
	}

	public void RemoveCurves () {
		lines.RemoveCurves ();
		_curveCount = -1;
	}

	void Update () {
		if (Input.GetMouseButtonDown (0)) {
     		Vector3 position = Camera.main.ScreenToWorldPoint (Input.mousePosition);
    		position.z = transform.position.z;
     		_curveCount++;
     		_curvePoints[_curveCount] = position;
     		if (_curveCount == 3) {
				_curveCount = -1;
     			lines.AddCurve (_curvePoints, Color.red, Color.blue);
     		}
     	}
	}

}
