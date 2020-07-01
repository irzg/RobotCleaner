using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CrateSpawner))]
public class GameManager : MonoBehaviour
{
    CrateSpawner _createSpawner = null;

    void Start()
    {
        _createSpawner = GetComponent<CrateSpawner>();
        _createSpawner.SpawnCrates();
    }

    void Update()
    {
        
    }
}
