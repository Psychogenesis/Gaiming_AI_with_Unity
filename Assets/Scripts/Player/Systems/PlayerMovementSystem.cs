using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;


public class PlayerMovementSystem : ComponentSystem {

    private struct PlayerEnties
    {
        public PlayerInputComponent playerInputComponent;
        public Transform transform;
        public Camera playerCamera;
    }

    private readonly float time = Time.deltaTime;


    protected override void OnUpdate()
    {
        foreach( var entity in GetEntities<PlayerEnties>())
        {            
            entity.playerInputComponent.transform.position = MoveCamera(entity);
            entity.playerInputComponent.transform.rotation = RotateCamera(entity);
        }
    }

    private Vector3 MoveCamera(PlayerEnties entity)
    {
        Vector3 pos = entity.playerInputComponent.transform.position;

        if ((Input.GetKey(KeyCode.W) || Input.mousePosition.y >= Screen.height - entity.playerInputComponent.panBorderThicness) && !PauseMenuScript.isPaused)
        {
            pos.z += time * entity.playerInputComponent.panSpeed;
        }
        if ((Input.GetKey(KeyCode.S) || Input.mousePosition.y <= entity.playerInputComponent.panBorderThicness) && !PauseMenuScript.isPaused)
        {
            pos.z -= entity.playerInputComponent.panSpeed * time;
        }
        if ((Input.GetKey(KeyCode.A) || Input.mousePosition.x <= entity.playerInputComponent.panBorderThicness) && !PauseMenuScript.isPaused)
        {
            pos.x -= entity.playerInputComponent.panSpeed * time;
        }
        if ((Input.GetKey(KeyCode.D) || Input.mousePosition.x >= Screen.width - entity.playerInputComponent.panBorderThicness) && !PauseMenuScript.isPaused)
        {
            pos.x += entity.playerInputComponent.panSpeed * time;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
         pos.y -= scroll * time * 40 * entity.playerInputComponent.zoomSpeed;
 
         pos.x = Mathf.Clamp(pos.x, -entity.playerInputComponent.panLimit.x, entity.playerInputComponent.panLimit.x);
         pos.z = Mathf.Clamp(pos.z, -entity.playerInputComponent.panLimit.y, entity.playerInputComponent.panLimit.y);
         pos.y = Mathf.Clamp(pos.y, entity.playerInputComponent.zoomDistanceMin, entity.playerInputComponent.zoomDistanceMax);

        return pos;
    }

    private Quaternion RotateCamera(PlayerEnties entity)
    {
        Quaternion rotation = entity.playerInputComponent.transform.rotation;

        if (Input.GetKey(KeyCode.Q) && !PauseMenuScript.isPaused)
            rotation.y -= entity.playerInputComponent.rotationSpeed* time;
        if (Input.GetKey(KeyCode.E) && !PauseMenuScript.isPaused)
            rotation.y += entity.playerInputComponent.rotationSpeed* time;

        return rotation;
    }

}
