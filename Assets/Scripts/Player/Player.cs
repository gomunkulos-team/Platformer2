using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Mana))]
[RequireComponent(typeof(Atack))]
[RequireComponent(typeof(Rotator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(InputReader))]
[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(CollisionDetector))]
[RequireComponent(typeof(TriggerCollector))]

public class Player : MonoBehaviour
{
    private Health _health;
    private Mana _mana;
    private Atack _attack;
    private Rotator _rotator;
    private Mover _mover;
    private InputReader _inputReader;
    private CollisionDetector _collisionDetector;
    private TriggerCollector _triggerCollector;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _mana = GetComponent<Mana>();
        _attack = GetComponent<Atack>();
        _rotator = GetComponent<Rotator>();
        _mover = GetComponent<Mover>();
        _inputReader = GetComponent<InputReader>();
        _collisionDetector = GetComponent<CollisionDetector>();
        _triggerCollector = GetComponent<TriggerCollector>();
    }

    private void OnEnable()
    {
        _triggerCollector.CoinTouched += CollectCoin;
        _triggerCollector.FirstAidTouched += Heal;

        _collisionDetector.EnemyTouched += Attack;

        _health.IsOver += Die;
    }

    private void OnDisable()
    {
        _triggerCollector.CoinTouched -= CollectCoin;
        _triggerCollector.FirstAidTouched -= Heal;

        _collisionDetector.EnemyTouched -= Attack;

        _health.IsOver -= Die;
    }

    private void Update()
    {
        if (_inputReader.DirectionX != 0)
        {
            _rotator.Rotate(_inputReader.DirectionX);
            _mover.MoveOnX(_inputReader.DirectionX);
        }

        if (_inputReader.GetIsJump() && _collisionDetector.IsGrounded)
        {
            _mover.Jump();
        }
    }

    private void CollectCoin(Coin coin)
    {
        coin.Collect();
    }

    private void Heal(FirstAid firstAid)
    {
        _health.Increse(firstAid.HealingAmount);
        firstAid.Collect();
    }

    public void TakeDamage(float damage)
    {
        _health.Decrease(damage);
    }

    private void Die()
    {
        this.gameObject.SetActive(false);
    }

    private void Attack(Enemy enemy)
    {
        enemy.TakeDamage(_attack.GetValue());
    }
}
