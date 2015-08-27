using UnityEngine;
using System.Collections;

public class MessagingClientReceiver : MonoBehaviour {
	
	void Start () {
		MessagingManager.Instance.Subscribe(ThePlayerIsTryingToLeave);
	}

	void ThePlayerIsTryingToLeave()
	{
		var dialog = GetComponent<ConversationComponent>();
		if(dialog != null)
		{
			if(dialog.Conversations != null && dialog.Conversations.Length > 0)
			{
				var conversation = dialog.Conversations[0];
				if(conversation != null)
				{
					ConversationManager.Instance.StartConversation(conversation);
				}
			}
		}
	}

	void OnDestory()
	{
		if(MessagingManager.Instance != null)
		{
			MessagingManager.Instance.Unsubscribe(ThePlayerIsTryingToLeave);
		}
	}
}
