using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneState : BattleState
{
    ConversationController conversationController;
    ConversationData data;

    protected override void Awake(){
        base.Awake();
        Debug.Log("here");
        conversationController = owner.GetComponentInChildren<ConversationController>();
        Debug.Log(conversationController);
        data = Resources.Load<ConversationData>("Conversations/test");
    }

    protected override void OnDestroy(){
        base.OnDestroy();
        if(data)
            Resources.UnloadAsset(data);
    }

    public override void Enter(){
        base.Enter();
        conversationController.Show(data);
    }

    protected override void AddListeners(){
        base.AddListeners();
        ConversationController.completeEvent += OnCompleteConversation;
    }
    
     protected override void RemoveListeners ()
    {
        base.RemoveListeners ();
        ConversationController.completeEvent -= OnCompleteConversation;
    }

    protected override void OnFire (object sender, InfoEventArgs<int> e)
    {
        base.OnFire (sender, e);
        conversationController.Next();
    }
    
    void OnCompleteConversation (object sender, System.EventArgs e)
    {
        owner.ChangeState<SelectUnitState>();
    }
}
