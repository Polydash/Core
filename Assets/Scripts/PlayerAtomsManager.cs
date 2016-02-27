using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerAtomsManager : MonoBehaviour
{
    private List<AtomMovement> _AtomsList;
    private PlayerMovement _PlayerMovement;

    public AtomMovement _AtomPrefab;
    public int _AtomsNum;
    public float _AtomSpawnRange;

    private void Awake()
    {
        _PlayerMovement = GetComponent<PlayerMovement>();

        _AtomsList = new List<AtomMovement>();
        for(int i = 0; i < _AtomsNum; ++i)
        {
            AtomMovement atom = GameObject.Instantiate<AtomMovement>(_AtomPrefab);
            atom.transform.position = transform.position + new Vector3(Random.Range(-_AtomSpawnRange, _AtomSpawnRange), Random.Range(-_AtomSpawnRange, _AtomSpawnRange));
            atom.Init(_PlayerMovement, _AtomsList);
            _AtomsList.Add(atom);
        }
    }

    public List<AtomMovement> GetAtomsList()
    {
        return _AtomsList;
    }
}
