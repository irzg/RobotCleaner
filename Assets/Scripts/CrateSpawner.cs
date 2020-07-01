using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateSpawner : MonoBehaviour
{
    [SerializeField]
    private Crate _cratePrefab = null;

    [SerializeField]
    private int _cratesToSpawn = 3;


    public void SpawnCrates()
    {
        SpawnRandomCrates();
    }

    private void SpawnRandomCrates()
    {
        for (int i = 0; i < _cratesToSpawn; i++)
        {
            float x = Random.value;

            if (i % 2 == 0)
            {
                x = Random.Range(2, 5);
            }

            else
            {
                x = Random.Range(-2, -5);
            }

            float seedColor = Random.value;


            Vector3 position = new Vector3(x, 0, 6);
            Crate crate = Instantiate(_cratePrefab, position, Quaternion.identity);

            CrateColor cc = seedColor < 0.5f ? CrateColor.Blue : CrateColor.Red;
            crate.Color = cc;

            CrateCollection.Instance.Add(crate);
        }
    }
}
