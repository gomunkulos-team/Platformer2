using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Health _health;
    private Atack _atack;
    private Patroller _patroller;
    private AlarmSistem _alarmSistem;
    private Chaser _chaser;
    private Player _player;

    private bool _patrol = true;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _atack = GetComponent<Atack>();
        _patroller = GetComponent<Patroller>();
        _alarmSistem = GetComponent<AlarmSistem>();
        _chaser = GetComponent<Chaser>();
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
        if (_patrol)
            _patroller.Activate();
        else
            _chaser.Activate(_player);
    }

    private void StartChase(Player player)
    {
        _player = player;
        _patrol = false;
    }

    private void StartPatroling()
    {
        _patrol = true;
    }
}
