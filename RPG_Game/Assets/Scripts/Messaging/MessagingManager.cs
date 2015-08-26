using UnityEngine;
using System.Collections.Generic;
using System;

public class MessagingManager : Singleton<MessagingManager> {

	protected MessagingManager() {}

	private List<Action> subscribers = new List<Action>();

	private List<Action<bool>> uiEventSubscribers = new List<Action<bool>>();

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

	public void SubscribeUIEvent(Action<bool> subscriber)
	{
		uiEventSubscribers.Add(subscriber);
	}

	public void BroadcastUIEvent(bool uIVisible)
	{
		foreach(var subscriber in uiEventSubscribers.ToArray())
		{
			subscriber(uIVisible);
		}
	}

	public void UnsubscribeUIEvnet(Action<bool> subscriber)
	{
		uiEventSubscribers.Remove(subscriber);
	}

	public void ClearAllUIEventSubscribers()
	{
		uiEventSubscribers.Clear();
	}
}
