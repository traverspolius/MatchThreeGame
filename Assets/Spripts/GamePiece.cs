using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePiece : MonoBehaviour
{
	public int xIndex;
	public int yIndex;

	bool m_isMoving = false;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			Move((int)transform.position.x + 1, (int)transform.position.y, 0.5f);

		}

		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			Move((int)transform.position.x - 1, (int)transform.position.y, 0.5f);

		}
	}

	public void SetCoord(int x, int y)
	{
		xIndex = x;
		yIndex = y;
	}

	public void Move(int destX, int destY, float timeToMove)
	{

		if (!m_isMoving)
		{

			StartCoroutine(MoveRoutine(new Vector3(destX, destY, 0), timeToMove));
		}
	}


	IEnumerator MoveRoutine(Vector3 destination, float timeToMove)
	{
		Vector3 startPosition = transform.position;

		bool reachedDestination = false;

		float elapsedTime = 0f;

		m_isMoving = true;

		while (!reachedDestination)
		{
			// if we are close enough to destination
			if (Vector3.Distance(transform.position, destination) < 0.01f)
			{
				reachedDestination = true;
				transform.position = destination;
				SetCoord((int)destination.x, (int)destination.y);

			}

			// track the total running time
			elapsedTime += Time.deltaTime;

			// calculate the Lerp value
			float t = Mathf.Clamp(elapsedTime / timeToMove, 0f, 1f);

			// move the game piece
			transform.position = Vector3.Lerp(startPosition, destination, t);

			// wait until next frame
			yield return null;
		}

		m_isMoving = false;


	}
}
