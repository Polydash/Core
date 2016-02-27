using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerAtomsManager : MonoBehaviour
{
    private List<GameObject> _AtomsList;

    public GameObject _AtomPrefab;
    public int _AtomsNum;
    public float _AtomSpawnRange;

    private void Awake()
    {
        _AtomsList = new List<GameObject>();
        for(int i = 0; i < _AtomsNum; ++i)
        {
            GameObject atom = GameObject.Instantiate<GameObject>(_AtomPrefab);
            atom.transform.position = transform.position + new Vector3(Random.Range(-_AtomSpawnRange, _AtomSpawnRange), Random.Range(-_AtomSpawnRange, _AtomSpawnRange));
            _AtomsList.Add(atom);
        }
    }
}
