using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Damage))]
[RequireComponent(typeof(Rotator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(InputReader))]
[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(CollisionDetector))]
[RequireComponent(typeof(TriggerCollector))]
[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(VampiricAura))]

public class Player : MonoBehaviour
{
    private Health _health;
    private Damage _attack;
    private Rotator _rotator;
    private Mover _mover;
    private InputReader _inputReader;
    private CollisionDetector _collisionDetector;
    private TriggerCollector _triggerCollector;
    private PlayerAnimator _animator;
    private VampiricAura _vampiricAura;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _attack = GetComponent<Damage>();
        _rotator = GetComponent<Rotator>();
        _mover = GetComponent<Mover>();
        _inputReader = GetComponent<InputReader>();
        _collisionDetector = GetComponent<CollisionDetector>();
        _triggerCollector = GetComponent<TriggerCollector>();
        _animator = GetComponent<PlayerAnimator>();
        _vampiricAura = GetComponent<VampiricAura>();
    }

    private void OnEnable()
    {
        _triggerCollector.CoinTouched += CollectCoin;
        _triggerCollector.FirstAidTouched += CollectFirstAid;

        _collisionDetector.EnemyTouched += Attack;

        _vampiricAura.DrainHealth += Heal;

        _health.IsOver += Die;
    }

    private void OnDisable()
    {
        _triggerCollector.CoinTouched -= CollectCoin;
        _triggerCollector.FirstAidTouched -= CollectFirstAid;

        _collisionDetector.EnemyTouched -= Attack;

        _health.IsOver -= Die;
    }

    private void Update()
    {
        if (_inputReader.DirectionX != 0)
        {
            _rotator.Rotate(_inputReader.DirectionX);
            _mover.MoveOnX(_inputReader.DirectionX);
            _animator.ControlAnimation(_inputReader.DirectionX);
        }

        if (_inputReader.GetIsJump() && _collisionDetector.IsGrounded)
        {
            _mover.Jump();
        }

        if (_inputReader.GetIsMouseClick() && _vampiricAura.ReadyToActivate)
        {
            _vampiricAura.ActivateAura();
        }
    }

    private void CollectCoin(Coin coin)
    {
        coin.Collect();
    }

    private void Heal(float value)
    {
        _health.Increse(value);
    }

    private void CollectFirstAid(FirstAid firstAid)
    {
        firstAid.Collect();
        Heal(firstAid.HealingAmount);
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
