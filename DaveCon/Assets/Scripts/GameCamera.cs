using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour {
	
	public Transform target;
	public float trackAccel = 1;
	public void SetTarget (Transform t)
	{
		target = t;
	}
	
	
	void LateUpdate()
	{
		if(target)
		{
			float x = IncrementTowards(transform.position.x,target.position.x,trackAccel);
			float y = IncrementTowards(transform.position.y,target.position.y,trackAccel);
			transform.position = new Vector3(x,y,transform.position.z);
		}
	}
	
	
	private float IncrementTowards(float n, float target, float accel)
	{
		if (n == target)
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
