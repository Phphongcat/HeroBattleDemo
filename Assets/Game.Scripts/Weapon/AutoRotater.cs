using UnityEngine;

public class AutoRotater : MonoBehaviour
{
    public float rotSpeed;

	private void Update()
	{
		transform.Rotate(Vector3.forward * rotSpeed * Time.deltaTime);
	}
}
