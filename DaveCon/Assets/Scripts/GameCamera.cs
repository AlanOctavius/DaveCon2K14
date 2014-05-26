using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour {
	
	public Transform target;
	public float trackAccel = 20;
	private Transform targetPos;
	private Vector3 pos;
	private float x = 0;
	private float y = 10;
	private float z = -20;
	private bool Alive = false;
	public float minY = 6;
	public float minX = -10;
	public float maxY = 9;
	public float maxX = 10;
	public float box = 1;


	public void SetPosition (Transform t)
	{
			targetPos = t;
	}

	public void SetAlive (bool t)
	{
		Alive = t;
	}

	void Start()
	{
		pos.x = x;
		pos.y = y;
		pos.z = z;

	}

	
	void LateUpdate()
	{
		if(Alive)
		{
			pos.x = IncrementTowards(transform.position.x,targetPos.position.x,trackAccel,box);
			pos.y = IncrementTowards(transform.position.y,targetPos.position.y,trackAccel,box);
			/*if(pos.y < minY)
			{
				pos.y = minY;
			}
			if(pos.x < minX)
			{
				pos.x = minX;
			}
			if(pos.y > maxY)
			{
				pos.y = maxY;
			}
			if(pos.x > maxX)
			{
				pos.x = maxX;
			}*/
			transform.position = pos;
		}
	}
	
	
	private float IncrementTowards(float n, float target, float accel,float box)
	{
		if (n==target)
		{
			return n;
		}
		else
		{
			float dir = Mathf.Sign (target-n); // must n be increased or decreased to get closer to target
			n += accel * Time.deltaTime * dir;
			return (dir == Mathf.Sign(target-n))? n: target; // if n has passed target then return target, otherwise return n
		}
	}
}
