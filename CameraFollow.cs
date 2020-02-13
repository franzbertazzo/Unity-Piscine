using UnityEngine;

public class CameraFollow : MonoBehaviour
{

	[SerializeField] GameObject gm;
	public Transform target;
	public float damping = 1;
	private float offsetZ;
	private Vector3 lastTargetPosition;
	private Vector3 currentVelocity;
	private Vector3 lookAheadPos;

	private void Start()
	{
		lastTargetPosition = target.position;
		offsetZ = (transform.position - target.position).z;
		transform.parent = null;
	}

	private void FixedUpdate()
	{
		target = gm.GetComponent<GameMaster>().target;

		Vector3 newPos = Vector3.SmoothDamp(transform.position, target.position, ref currentVelocity, damping);

		transform.position = newPos;

		lastTargetPosition = target.position;
	}
}
