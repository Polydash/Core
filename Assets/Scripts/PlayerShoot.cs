using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(PlayerAtomsManager))]
[RequireComponent(typeof(PlayerMovement))]
public class PlayerShoot : MonoBehaviour
{
    private PlayerMovement _PlayerMovement;
    private PlayerAtomsManager _AtomsManager;
    private List<AtomMovement> _AtomsList;

    private void Awake()
    {
        _PlayerMovement = GetComponent<PlayerMovement>();
        _AtomsManager = GetComponent<PlayerAtomsManager>();
    }

    private void Start()
    {
        _AtomsList = _AtomsManager.GetAtomsList();
    }

    private void Update()
    {
        if(Input.GetButtonDown("Fire2"))
        {
            int atomIndex = 0;
            _AtomsList[atomIndex].Shoot(_PlayerMovement.GetMoveDirection());
        }
    }
}
