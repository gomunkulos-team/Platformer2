using UnityEngine;

public class ManaCrystal : MonoBehaviour
{
    [SerializeField] private float _restoreAmount;

    public float RestoreAmount { get { return _restoreAmount; } private set { } }

    public void Collect()
    {
        this.gameObject.SetActive(false);
    }
}
