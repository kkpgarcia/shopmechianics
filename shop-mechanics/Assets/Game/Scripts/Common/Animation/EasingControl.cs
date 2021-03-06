﻿using UnityEngine;
using System;
using System.Collections;

public class EasingControl : MonoBehaviour 
{
	#region Events
	public event EventHandler UpdateEvent;
	public event EventHandler StateChangeEvent;
	public event EventHandler CompletedEvent;
	public event EventHandler LoopedEvent;
	#endregion

	#region Enums
	public enum TimeType
	{
		Normal,
		Real,
		Fixed,
	};

	public enum PlayState
	{
		Stopped,
		Paused,
		Playing,
		Reversing,
	};

	public enum EndBehaviour
	{
		Constant,
		Reset,
	};

	public enum LoopType
	{
		Repeat,
		PingPong,
	};
	#endregion

	#region Properties
	public TimeType timeType = TimeType.Normal;
	public PlayState playState { get; private set; }
	public PlayState previousPlayState { get; private set; }
	public EndBehaviour endBehaviour = EndBehaviour.Constant;
	public LoopType loopType = LoopType.Repeat;
	public bool IsPlaying { get { return playState == PlayState.Playing || playState == PlayState.Reversing; }}

	public float startValue = 0.0f;
	public float endValue = 1.0f;
	public float duration = 1.0f;
	public int loopCount = 0;
	public Func<float, float, float, float> equation = EasingEquations.Linear;

	public float currentTime { get; private set; }
	public float currentValue { get; private set; }
	public float currentOffset { get; private set; }
	public int loops { get; private set; }
	
	public AnimationCurve curve;
	public bool useAnimationCurve = false;
	#endregion

	#region Queries
	public EasingControl SetLoopType(LoopType l) {
		loopType = l;
		return this;
	}

	public EasingControl SetLoopCount(int i) {
		loopCount = i;
		return this;
	}

	public EasingControl SetEquation(Func<float, float, float, float> e) {
		equation = e;
		return this;
	}

	public EasingControl SetTimeType(TimeType t) {
		timeType = t;
		return this;
	}

	public EasingControl SetOnStateChange(EventHandler e) {
		StateChangeEvent += e;
		return this;
	}

	public EasingControl SetOnUpdate(EventHandler e) {
		UpdateEvent += e;
		return this;
	}

	public EasingControl SetOnFinish(EventHandler e) {
		CompletedEvent += e;
		return this;
	}

	public EasingControl SetEndBehavior(EndBehaviour e) {
		this.endBehaviour = e;
		return this;
	}

	public EasingControl SetDuration(float duration) {
		this.duration = duration;
		return this;
	}

	#endregion

	
	#region MonoBehaviour
	void OnEnable ()
	{
		Resume();
	}
	
	void OnDisable ()
	{
		Pause();
	}
	#endregion

	#region Public
	public void Play ()
	{
		SetPlayState(PlayState.Playing);
	}
	
	public void Reverse ()
	{
		SetPlayState(PlayState.Reversing);
	}
	
	public void Pause ()
	{
		if (IsPlaying)
			SetPlayState(PlayState.Paused);
	}
	
	public void Resume ()
	{
		if (playState == PlayState.Paused)
			SetPlayState(previousPlayState);
	}
	
	public void Stop ()
	{
		SetPlayState(PlayState.Stopped);
		previousPlayState = PlayState.Stopped;
		loops = 0;
		if (endBehaviour == EndBehaviour.Reset)
			SeekToBeginning ();
	}
	
	public void SeekToTime (float time)
	{
		currentTime = Mathf.Clamp01(time / duration);
		float newValue = (endValue - startValue) * currentTime + startValue;
		currentOffset = newValue - currentValue;
		currentValue = newValue;
		OnUpdate();
	}
	
	public void SeekToBeginning ()
	{
		SeekToTime(0.0f);
	}
	
	public void SeekToEnd ()
	{
		SeekToTime(duration);
	}
	#endregion

	#region Protected
	protected virtual void OnUpdate ()
	{
		if (UpdateEvent != null)
			UpdateEvent(this, EventArgs.Empty);
	}

	protected virtual void OnLoop ()
	{
		if (LoopedEvent != null)
			LoopedEvent(this, EventArgs.Empty);
	}

	protected virtual void OnComplete ()
	{
		if (CompletedEvent != null)
			CompletedEvent(this, EventArgs.Empty);
	}

	protected virtual void OnStateChange ()
	{
		if (StateChangeEvent != null)
			StateChangeEvent(this, EventArgs.Empty);
	}
	#endregion

	#region Private
	void SetPlayState (PlayState target)
	{
		if (isActiveAndEnabled)
		{
			if (playState == target)
				return;
			
			previousPlayState = playState;
			playState = target;
			OnStateChange();
			StopCoroutine("Ticker");
			if (IsPlaying)
				StartCoroutine("Ticker");
		}
		else
		{
			previousPlayState = target;
			playState = PlayState.Paused;
		}
	}

	IEnumerator Ticker ()
	{
		while (true)
		{
			switch (timeType)
			{
			case TimeType.Normal:
				yield return new WaitForEndOfFrame();
				Tick(Time.deltaTime);
				break;
			case TimeType.Real:
				yield return new WaitForEndOfFrame();
				Tick(Time.unscaledDeltaTime);
				break;
			default: // Fixed
				yield return new WaitForFixedUpdate();
				Tick(Time.fixedDeltaTime);
				break;
			}
		}
	}

	void Tick (float time)
	{
		bool finished = false;
		if (playState == PlayState.Playing)
		{
			currentTime = Mathf.Clamp01( currentTime + (time / duration));
			finished = Mathf.Approximately(currentTime, 1.0f);
		}
		else // Reversing
		{
			currentTime = Mathf.Clamp01( currentTime - (time / duration));
			finished = Mathf.Approximately(currentTime, 0.0f);
		}

		float equationValue = useAnimationCurve ? curve.Evaluate(currentTime) : equation (0.0f, 1.0f, currentTime);

		float frameValue = (endValue - startValue) * equationValue + startValue;
		currentOffset = frameValue - currentValue;
		currentValue = frameValue;
		OnUpdate();
		
		if (finished)
		{
			++loops;
			if (loopCount < 0 || loopCount >= loops) 
			{
				if (loopType == LoopType.Repeat) 
					SeekToBeginning();
				else // PingPong
					SetPlayState( playState == PlayState.Playing ? PlayState.Reversing : PlayState.Playing );

				OnLoop();
			} 
			else
			{
				OnComplete();
				Stop ();
			}
		}
	}
	#endregion
}