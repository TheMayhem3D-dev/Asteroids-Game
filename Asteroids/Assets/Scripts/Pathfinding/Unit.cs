using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {

	[Header("General")]
	[SerializeField] private Movement movement;
	[SerializeField] private RandomPosition randomPosition;

	[Header("Target")]
	[SerializeField] private float minRateOfChangeMoveDir;
	[SerializeField] private float maxRateOfChangeMoveDir;
	private bool rotatesToWaypoint = false;

	Vector2[] path;
	int targetIndex;

	private void Awake()
    {
		if (movement == null)
			movement = GetComponent<Movement>();
    }

	private void Start() {
		StartCoroutine(ChangeTarget());
	}

	IEnumerator ChangeTarget()
	{
		Vector2 randomVector = randomPosition.GetRandomPosition();
		PathRequestManager.RequestPath(transform.position, randomVector, OnPathFound);

		yield return new WaitForSeconds(Random.Range(minRateOfChangeMoveDir, maxRateOfChangeMoveDir));

		StartCoroutine(ChangeTarget());
	}

	public void OnPathFound(Vector2[] newPath, bool pathSuccessful) {
		if (pathSuccessful) {
			path = newPath;
			targetIndex = 0;
			StopCoroutine("FollowPath");
			StartCoroutine("FollowPath");
		}
	}

	IEnumerator FollowPath() {
		Vector2 currentWaypoint = path[0];
		while (true) {
			if ((Vector2)transform.position == currentWaypoint) {
				targetIndex ++;
				if (targetIndex >= path.Length) {
					targetIndex = 0;
					path = new Vector2[0];
					rotatesToWaypoint = false;
					yield break;
				}
				currentWaypoint = path[targetIndex];
				movement.RotateTowards(currentWaypoint);
			}
			else
            {
				if(!rotatesToWaypoint)
                {
					movement.RotateTowards(currentWaypoint);
					rotatesToWaypoint = true;
                }
			}

			movement.MoveTo(currentWaypoint);

			yield return null;

		}
	}

	public void OnDrawGizmos() {
		if (path != null) {
			for (int i = targetIndex; i < path.Length; i ++) {
				Gizmos.color = Color.black;
				Gizmos.DrawCube(path[i], Vector3.one*0.5f);

				if (i == targetIndex) {
					Gizmos.DrawLine(transform.position, path[i]);
				}
				else {
					Gizmos.DrawLine(path[i-1],path[i]);
				}
			}
		}
	}
}
