using UnityEngine;
using System.Collections;

public class StateMachine : MonoBehaviour 
{
	public virtual State CurrentState
	{
		get { return currentState; }
		set { Transition (value); }
	}
	protected State currentState;
	protected bool inTransition;

	public virtual T GetState<T> () where T : State
	{
		T target = GetComponent<T>();
		if (target == null)
			target = gameObject.AddComponent<T>();
		return target;
	}
	
	private void Update() {
		if(CurrentState != null)
			CurrentState.OnUpdate();
	}

	public virtual void ChangeState<T> () where T : State
	{
		CurrentState = GetState<T>();

		//Debug.Log("CHANGING STATE TO: " + CurrentState.GetType().Name);
	}

	protected virtual void Transition (State value)
	{
		if (currentState == value /*|| inTransition*/)
			return;

		//inTransition = true;
		
		if (currentState != null)
			currentState.Exit();
		
		currentState = value;
		
		if (currentState != null)
			currentState.Enter();
		
		inTransition = false;
	}
}