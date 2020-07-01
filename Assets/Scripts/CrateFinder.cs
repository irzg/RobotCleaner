using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateFinder : MonoBehaviour
{
    [SerializeField]
    GameObject _searchObject = null;

    void Start()
    {
    }    

    public int CratesLeft
    {
        get
        {
            return CrateCollection.Instance.Count;
        }
    }   

    public Crate NearestCrate
    {
        get
        {
            if (CratesLeft > 0)
            {

                Crate nearest = CrateCollection.Instance[0];
                 Crate highest = CrateCollection.Instance[0];

                float lowestDistance = float.MaxValue;
                float biggestHeight = float.MinValue;

                foreach (Crate crate in CrateCollection.Instance)
                {
                    float distance = Vector2.Distance(_searchObject.gameObject.transform.position, crate.transform.position);

                    if (distance < lowestDistance)
                    {
                        lowestDistance = distance;
                        nearest = crate;
                    }

                    if (crate.transform.position.y > biggestHeight)
                    {
                        biggestHeight = crate.transform.position.y;
                        highest = crate;
                    }
                }

                return nearest;
            }
            else
            {
                return null;
            }
        }
    }

    

}
