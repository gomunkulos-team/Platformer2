using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Health _health;
    private Damage _atack;
    private Patroller _patroller;
    private AlarmSistem _alarmSistem;
    private Chaser _chaser;
    private Player _player;
    private Fight _fight;

    private bool _patrol = true;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _atack = GetComponent<Damage>();
        _patroller = GetComponent<Patroller>();
        _alarmSistem = GetComponent<AlarmSistem>();
        _chaser = GetComponent<Chaser>();
        _fight = GetComponent<Fight>();
    }

    private void OnEnable()
    {
        _alarmSistem.PlayerSpotted += StartChase;
        _alarmSistem.PlayerLost += StartPatroling;
        _fight.TargetPlayer += AttackPlayer;
    }

    private void OnDisable()
    {
        _alarmSistem.PlayerSpotted -= StartChase;
        _alarmSistem.PlayerLost -= StartPatroling;
        _fight.TargetPlayer -= AttackPlayer;
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

    private void AttackPlayer(Player player)
    {
        player.TakeDamage(_atack.GetValue());
    }

    public void TakeDamage(float damage)
    {
        _health.Decrease(damage);
    }
}