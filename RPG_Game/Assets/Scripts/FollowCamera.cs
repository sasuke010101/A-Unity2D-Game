using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour {

	public float xMargin = 1.5f;
	public float yMargin = 1.5f;

	public float xSmooth = 1.5f;
	public float ySmooth = 1.5f;

	private Vector2 maxXAndY;
	private Vector2 minXAndY;

	public Transform player;

	void Awake()
	{
		var backgroundBounds = GameObject.Find("background").renderer.bounds;
		var camTopLeft = camera.ViewportToWorldPoint(new Vector3(0, 0, 0));
		var camBottomRight = camera.ViewportToWorldPoint(new Vector3(1, 1, 0));

		minXAndY.x = backgroundBounds.min.x - camTopLeft.x;
		maxXAndY.x = backgroundBounds.max.x - camBottomRight.x;

		player = GameObject.Find("Player").transform;

		if(player == null)
		{
			Debug.LogError("Player object not found");
		}
	}

	bool CheckXMargin()
	{
		return Mathf.Abs (transform.position.x - player.position.x) > xMargin;
	}

	bool CheckYMargin()
	{
		return Mathf.Abs(transform.position.y - player.position.y) > yMargin;
	}

	void FixedUpdate()
	{
		float targetX = transform.position.x;
		float targetY = transform.position.y;

		if(CheckXMargin())
			targetX = Mathf.Lerp(transform.position.x, player.position.x, xSmooth * Time.fixedDeltaTime);
		if(CheckYMargin())
			targetY = Mathf.Lerp(transform.position.y, player.position.y, ySmooth * Time.fixedDeltaTime);

		targetX = Mathf.Clamp(targetX, minXAndY.x, maxXAndY.x);
		targetY = Mathf.Clamp(targetY, minXAndY.y, maxXAndY.y);

		transform.position = new Vector3(targetX, targetY, transform.position.z);
	}
}
