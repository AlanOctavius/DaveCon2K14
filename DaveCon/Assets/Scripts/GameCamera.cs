using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour {
	
	private Transform target;
	public float trackAccel = 1;
	public bool Alive = false;
	private Vector3 pos;
	private float x = 0;
	private float y = 10;
	private float z = -20;
	private Vector3 newPos;
	private float speed = 5;
	public void SetPosition (Transform t)
	{
		target = t;
	}

	public void SetAlive (bool t)
	{
		Alive = t;
	}
	
	void Start()
	{
		pos.x = x;
		pos.y = y;
		pos.z = -20;
		newPos = pos;
		transform.position = pos;
	}
	void LateUpdate()
	{
		if (Alive)
		{
			float x = IncrementTowards(transform.position.x,target.position.x,trackAccel);
			float y = IncrementTowards(transform.position.y,target.position.y,trackAccel);
			transform.position = new Vector3(x,y,-20);
		}
		else
		{
			float x = IncrementTowards(transform.position.x,newPos.x,trackAccel);
			float y = IncrementTowards(transform.position.y,newPos.y,trackAccel);
			transform.position = new Vector3(x,y,-20);
		}
	}

	void Update()
	{
		newPos = transform.position;
		newPos = new Vector3(newPos.x + speed*Time.deltaTime*Input.GetAxis("Horizontal"),newPos.y + speed*Time.deltaTime*Input.GetAxis("Vertical") , -20);
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
