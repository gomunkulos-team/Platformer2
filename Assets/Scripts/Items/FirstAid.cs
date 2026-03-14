using UnityEngine;

public class FirstAid : MonoBehaviour
{
    [SerializeField] private float _healingAmount;

    public float HealingAmount { get { return _healingAmount; } private set { } }

    public void Collect()
    {
        this.gameObject.SetActive(false);
    }
}
