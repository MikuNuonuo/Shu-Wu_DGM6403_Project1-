using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floweradd : MonoBehaviour
{
    public GameManager ui;
    public Transform explosion;

    void Start()
    {
        ui = GameObject.FindWithTag("ui").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ui.IncrementScore();
            Destroy(gameObject);
        }
    }

}