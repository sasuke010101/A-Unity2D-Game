using UnityEngine;
using System.Collections.Generic;
using System;

public class MessagingManager : MonoBehaviour {

	public static MessagingManager Instance { get; private set;}

	private List<Action> subscribers = new List<Action>();

	public ScriptingObjects MyWaypoints;

	void Awake()
	{
		Debug.Log("Messaging Manager Started");

		if(Instance != null && Instance != this)
		{
			Destroy(gameObject);
		}

		Instance = this;

		DontDestroyOnLoad(gameObject);
	}

	public void Subscribe(Action subscriber)
	{
		Debug.Log("Subscriber registered");
		subscribers.Add(subscriber);
	}

	public void Unsubscribe(Action subscriber)
	{
		Debug.Log("Subscriber Unregistered");
		subscribers.Remove(subscriber);
	}

	public void ClearAllSubscribers()
	{
		subscribers.Clear();
	}

	public void Broadcast()
	{
		Debug.Log("Broadcast requesed, No of Subscribers = " + subscribers.Count);
		foreach(var subscriber in subscribers)
		{
			subscriber();
		}
	}
}
