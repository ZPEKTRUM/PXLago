using UnityEngine;

public class TwinStick_Camera : MonoBehaviour
{
#region Champs
	//INSPECTOR
	[SerializeField] Transform _target;
	[SerializeField] float _smoothTime;
	[SerializeField] Vector3 offset;
	//PRIVATES
	Vector3 _velocity;
	//PUBLICS
  
#endregion
#region Default Informations
	void Reset()
	{
		_smoothTime = 0.3f;
		_velocity = Vector3.zero;
	}
#endregion
#region Unity LifeCycle
	// Start is called before the first frame update
	// Update is called once per frame
	void Update()
	{
		if(_target != null)
		{
			Vector3 targetPosition = _target.position + offset;
			transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, _smoothTime);
		}
	}
#endregion
}