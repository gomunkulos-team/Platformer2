using System;
using System.Collections;
using UnityEngine;


public class VampiricAura : MonoBehaviour
{
    private float _activeTime = 6;
    private float _rechargeTime = 4;
    private bool _readyToActivate;

    private VampiricAuraDamager _vampiricDamager;

    public event Action<float> DrainHealth;
    public event Action<float> ActivateDrain;
    public event Action<float> RechargeDrain;

    public bool ReadyToActivate => _readyToActivate;

    private void Awake()
    {
        _vampiricDamager = GetComponentInChildren<VampiricAuraDamager>();
        _vampiricDamager.gameObject.SetActive(false);
        _readyToActivate = true;
    }

    private void OnEnable()
    {
        _vampiricDamager.DrainIsSuccessful += Drain;
    }

    private void OnDisable()
    {
        _vampiricDamager.DrainIsSuccessful -= Drain;
    }

    public void ActivateAura()
    {
        _vampiricDamager.gameObject.SetActive(true);
        _readyToActivate = false;
        ActivateDrain?.Invoke(_activeTime);
        StartCoroutine(ActiveTime());
    }

    private void RechargeAura()
    {
        _vampiricDamager.gameObject.SetActive(false);
        RechargeDrain?.Invoke(_rechargeTime);
        StartCoroutine(RechargeTime());
    }

    private IEnumerator ActiveTime()
    {
        WaitForSeconds wait = new WaitForSeconds(_activeTime);

        yield return wait;
        RechargeAura();
    }

    private IEnumerator RechargeTime()
    {
        WaitForSeconds wait = new WaitForSeconds(_rechargeTime);

        yield return wait;
        _readyToActivate = true;
    }

    private void Drain(float value)
    {
        DrainHealth?.Invoke(value);
    }
}