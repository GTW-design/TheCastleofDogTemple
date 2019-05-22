using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour {
	public UnityEvent m_OnActivate;
	public UnityEvent m_OnDeactivate;

	public bool m_isSingleUse = false;

	// Use this for initialization
	void Start ()
	{
		
	}

	private void OnTriggerEnter(Collider other)
	{
		if (m_OnActivate == null)
			return;

		if (other.tag != "Player")
			return;

		m_OnActivate.Invoke();
	}

	private void OnTriggerExit(Collider other)
	{
		if (m_isSingleUse)
			return;

		if (m_OnDeactivate == null)
			return;

		if (other.tag != "Player")
			return;

		m_OnDeactivate.Invoke();
	}
}
