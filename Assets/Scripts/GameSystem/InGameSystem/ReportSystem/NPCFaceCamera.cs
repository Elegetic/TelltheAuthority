using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCFaceCamera : MonoBehaviour
{
    private Transform cameraTransform;

    void Start()
    {
        cameraTransform = Camera.main.transform;

        if (cameraTransform == null)
        {
            Debug.LogError("Can't find camara with tag 'MainCamera'!");
        }
    }

    void Update()
    {
        Vector3 directionToCamera = cameraTransform.position - transform.position;

        directionToCamera.y = 0;

        transform.rotation = Quaternion.LookRotation(directionToCamera);
    }
}
