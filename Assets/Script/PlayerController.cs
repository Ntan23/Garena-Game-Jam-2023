using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    private float horizontalInput;
    private float verticalInput;
    private Vector3 moveVector;
    private Vector3 mousePosition;
    private Rigidbody rb;
    private GameManager gm;

    void Start() 
    {
        gm = GameManager.instance;

        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        
        moveVector = new Vector3(-horizontalInput, verticalInput, 0f);

        // transform.Translate(moveVector * speed * Time.deltaTime);
        rb.MovePosition(transform.position + moveVector * speed * Time.deltaTime);
    }

    public void UpdateSpeed()
    {
        speed++;

        gm.CloseLevelUpUI();
    } 
}
