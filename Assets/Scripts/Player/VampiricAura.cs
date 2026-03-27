using System.Collections;
using UnityEngine;

public class VampiricAura : MonoBehaviour
{
    private float _damage = 4;
    private float _activeTime = 6;
    private float _rechargeTime = 4;
    private float _timeInterval = 1;

    private Enemy _enemyPrefab;

    public float ActiveTime => _activeTime;
    public float RechargeTime => _rechargeTime;
    public float Damage => _damage;

    private IEnumerator WaitTime(float time)
    {
        WaitForSeconds wait = new WaitForSeconds(time);
        float waitingTime = 0;

        while(waitingTime < time)
        {
            waitingTime += _timeInterval;
            yield return wait;
        }
    }
}