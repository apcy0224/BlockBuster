using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    [SerializeField] private Transform targetTr;
    [SerializeField] private float distance = 10.0f;
    [SerializeField] private float height = 2.0f;
    [SerializeField] private float dampTrace = 20.0f;
    [SerializeField] private float rotSpeed = 50.0f;
    [SerializeField] private float vRotUpLimit = 10.0f;
    [SerializeField] private float vRotDownLimit = 30.0f;

    private Transform cameraTr;

	// Use this for initialization
	void Start ()
    {
        cameraTr = GetComponent<Transform>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
	
	void LateUpdate ()
    {
        cameraTr.position = Vector3.Lerp(
            cameraTr.position,
            targetTr.position - (cameraTr.forward * distance) + (Vector3.up * height),
            Time.deltaTime * dampTrace
            );

        // 회전 값 입력
        cameraTr.Rotate(Vector3.left * rotSpeed * Time.deltaTime * Input.GetAxis("Mouse Y"));

        if (cameraTr.rotation.eulerAngles.x > 180 && cameraTr.rotation.eulerAngles.x < 360 - vRotUpLimit)
        {
            cameraTr.rotation = Quaternion.Euler(360 - vRotUpLimit,
            cameraTr.rotation.eulerAngles.y,
            cameraTr.rotation.eulerAngles.z);
        }
        else if (cameraTr.rotation.eulerAngles.x < 180 && cameraTr.rotation.eulerAngles.x > vRotDownLimit)
        {
            cameraTr.rotation = Quaternion.Euler(vRotDownLimit,
            cameraTr.rotation.eulerAngles.y,
            cameraTr.rotation.eulerAngles.z);
        }

        cameraTr.RotateAround(targetTr.position, Vector3.up, Time.deltaTime * rotSpeed * Input.GetAxis("Mouse X"));
        cameraTr.RotateAround(targetTr.position, Vector3.up, Time.deltaTime * rotSpeed * Input.GetAxis("Horizontal") * ((Input.GetAxis("Vertical") >= 0) ? 1 : -1));
    }
}
