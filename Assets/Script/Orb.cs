using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{
    private enum Type
    {
        bossHP, bossXP, normalHP, normalXP
    }

    [SerializeField] private Type orbType;
    private GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if(orbType == Type.bossHP) other.gameObject.GetComponent<PlayerHealth>().Heal(40);
            if(orbType == Type.bossXP) gm.AddXP(5);
            if(orbType == Type.normalHP) other.gameObject.GetComponent<PlayerHealth>().Heal(20);
            if(orbType == Type.normalXP) gm.AddXP(1);

            Destroy(this.gameObject);
        }    
    }
}
