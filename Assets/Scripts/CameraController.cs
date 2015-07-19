using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour 
{
	public Transform player;
	public Vector2 margin;
	public Vector2 smoothing;
	public BoxCollider2D boundsChecking;

	private Vector3 _min, _max;

	public bool isFollowing;

	// Use this for initialization
	void Start () 
	{
		_min = boundsChecking.bounds.min;
		_max = boundsChecking.bounds.max;
		isFollowing = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		var x = transform.position.x;
		var y = transform.position.y;

		if (isFollowing)
		{
			if(Mathf.Abs(x - player.position.x) > margin.x)
			{
				x = Mathf.Lerp (x, player.position.x, smoothing.x * Time.deltaTime);
			}
			if(Mathf.Abs(y - player.position.y) > margin.y)
			{
				y = Mathf.Lerp (y, player.position.y, smoothing.y * Time.deltaTime);
			}
		}

		var cameraHalfWidth = (GetComponent<Camera>().orthographicSize) * ((float)Screen.width/Screen.height);

		x = Mathf.Clamp (x, _min.x + cameraHalfWidth, _max.x - cameraHalfWidth);
		y = Mathf.Clamp (y, _min.y + (GetComponent<Camera>().orthographicSize), _max.y - (GetComponent<Camera>().orthographicSize));

		transform.position = new Vector3 (x, y, transform.position.z);

	}
}
