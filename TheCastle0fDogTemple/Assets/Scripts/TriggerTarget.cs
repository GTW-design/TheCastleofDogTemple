using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TriggerTarget : MonoBehaviour
{
	private bool isActive;

	public void Activate()
	{
		isActive = true;
		OnActivate();
	}

	public void Deactivate()
	{
		isActive = false;
		OnDeactivate();
	}

	protected abstract void OnActivate();
	protected abstract void OnDeactivate();
}
