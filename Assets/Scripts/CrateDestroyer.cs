using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateDestroyer : MonoBehaviour
{
    [SerializeField]
    private CrateColor _color;   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "crate")
        {
            Crate crate = collision.gameObject.GetComponent<Crate>();
            if (crate.Color == _color)
            {
                CrateCollection.Instance.Remove(crate);
                crate.gameObject.SetActive(false);
                //Destroy(crate.gameObject);
            }
        }
    }

}
