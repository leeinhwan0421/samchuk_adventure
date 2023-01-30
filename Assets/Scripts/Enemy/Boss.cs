using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Boss : Enemy
{
    [Header("Attack Range")]
    [SerializeField] private GameObject visibleAttackRange;
    [SerializeField] private Collider2D attackCollider;
    private void SetVisibleAttackRange2D(int isActive) => visibleAttackRange.SetActive(isActive == 0 ? false : true);
    private void SetAttackCollider2D(int isActive) => attackCollider.enabled = isActive == 0 ? false : true;

    private void OpenClearPanel()
    {
        GameManager.Instance.uiManager.clear.SetActive(true);
    }
}
