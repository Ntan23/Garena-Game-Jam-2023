using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollower : MonoBehaviour
{
    private enum Type {
        robot , enemy
    }

    [SerializeField] private float speed;
    [SerializeField] private Type type;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update() 
    {
        if(type == Type.robot) FollowMouse();
        if(type == Type.enemy) EnemyFollowPlayer();
    }
    
    private void FollowMouse()
    {
        Vector3 newBladePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        newBladePosition.z = 0f;

        Vector3 bladeDirection = newBladePosition - transform.position;

        float velocity = bladeDirection.magnitude / Time.deltaTime;

        transform.position = newBladePosition;
    }

    private void EnemyFollowPlayer()
    {
        if(player != null)
        {
            Vector3 playerPosition = player.transform.position;

            transform.position = playerPosition;
        }
    }
}
