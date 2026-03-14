using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Health _health;
    private Atack _atack;
    private Mover _mover;
    private Rotator _rotator;
    private Patroller _patroller;
    private AlarmSistem _alarmSistem;
    private Chaser _chaser;

    private float _direction;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _atack = GetComponent<Atack>();
        _mover = GetComponent<Mover>();
        _rotator = GetComponent<Rotator>();
        _patroller = GetComponent<Patroller>();
        _alarmSistem = GetComponent<AlarmSistem>();
        _chaser = GetComponent<Chaser>();
    }

    private void Start()
    {
        _direction = _patroller.Direction;
    }

    private void OnEnable()
    {
        _alarmSistem.PlayerSpotted += StartChase;
        _alarmSistem.PlayerLost += StartPatroling;

    }

    private void OnDisable()
    {
        _alarmSistem.PlayerSpotted -= StartChase;
        _alarmSistem.PlayerLost -= StartPatroling;
    }

    private void FixedUpdate()
    {
        _rotator.Rotate(_direction);
        _mover.MoveOnX(_direction);
    }

    private void StartChase(Player player)
    {
        _chaser.GetPlayerPosition(player);
        _direction = _chaser.Direction;
    }

    private void StartPatroling()
    {
        _direction = _patroller.Direction;
        Debug.Log("Patroling");
    }
}
