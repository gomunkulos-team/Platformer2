using UnityEngine;

public class FirstAid : MonoBehaviour
{
    [SerializeField] private float _healingAmount;

    public float HealingAmount => _healingAmount;

    public void Collect()
    {
        this.gameObject.SetActive(false);
    }
}
