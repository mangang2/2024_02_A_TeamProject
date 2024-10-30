using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject Player;
    public GameObject ButtonTeleport;
    public GameObject TopTeleport;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("ButtonTeleport"))
        {
            Player.transform.position = ButtonTeleport.transform.position;

        }

        if (collision.gameObject.CompareTag("TopTeleport"))
        {
            Player.transform.position = TopTeleport.transform.position;

        }
    }
}
