using UnityEngine;
using System.Collections;

public class MapMovement : MonoBehaviour {

	Vector3 StartLocation;
	Vector3 TargetLocation;
	float timer = 0;
	bool inputActive = true;
	bool inputReady = true;
	bool startedTravelling = false;

	int EncounterChance = 100;
	float EncounterDistance = 0;

	public AnimationCurve MovementCureve;


	void Awake()
	{
		this.collider2D.enabled = false;
		var lastPosition = GameState.GetLastScenePosition(Application.loadedLevelName);
		if(lastPosition != Vector3.zero)
		{
			transform.position = lastPosition;
		}
	}

	void Start()
	{
		MessagingManager.Instance.SubscribeUIEvent(UpdateInputAction);
	}

	private void UpdateInputAction(bool uiVisible)
	{
		inputReady = !uiVisible;
	}

	void Update()
	{
		if(inputActive && Input.GetMouseButtonUp(0))
		{
			StartLocation = transform.position.ToVector3_2D();
			timer = 0;
			TargetLocation = WorldExtensions.GetScreenPositionFor2D(Input.mousePosition);
			startedTravelling = true;

			var EncounterProbability = Random.Range(1, 100);
			if(EncounterProbability < EncounterChance && !GameState.PlayerReturningHome)
			{
				EncounterDistance = (Vector3.Distance(StartLocation, TargetLocation) / 100) * Random.Range(10, 100);
			}else
			{
				EncounterDistance = 0;
			}

		}else if(inputActive && Input.touchCount == 1)
		{
			StartLocation = transform.position.ToVector3_2D();
			timer = 0;
			TargetLocation = WorldExtensions.GetScreenPositionFor2D(Input.GetTouch(0).position);
			startedTravelling = true;

			var EncounterProbability = Random.Range(1, 100);
			if(EncounterProbability < EncounterChance && !GameState.PlayerReturningHome)
			{
				EncounterDistance = (Vector3.Distance(StartLocation, TargetLocation) / 100) * Random.Range(10, 100);
			}else
			{
				EncounterDistance = 0;
			}
		}

		if(TargetLocation != Vector3.zero && TargetLocation != transform.position && TargetLocation != StartLocation)
		{
			transform.position = Vector3.Lerp(StartLocation, TargetLocation, MovementCureve.Evaluate(timer));
			timer += Time.deltaTime;
		}

		if(startedTravelling && Vector3.Distance(StartLocation, transform.position.ToVector3_2D()) > 0.5)
		{
			this.collider2D.enabled = true;
			startedTravelling = false;
		}

		if(EncounterDistance > 0)
		{
			if(Vector3.Distance(StartLocation, transform.position) > EncounterDistance)
			{
				TargetLocation = Vector3.zero;
				NavigationManager.NavigateTo("Battle");
			}
		}

		if(!inputReady && inputActive)
		{
			TargetLocation = this.transform.position;
			Debug.Log("Stopping Player");
		}

		inputActive = inputReady;
	}

	void OnDestroy()
	{
		GameState.SetLastScenePosition(Application.loadedLevelName, transform.position);
	}
}
