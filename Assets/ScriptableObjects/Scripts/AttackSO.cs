using UnityEngine; // 유니티 엔진의 기본 기능을 사용하기 위한 네임스페이스

[CreateAssetMenu(fileName = "DefaultAttackSO", menuName = "TopDownController/Attacks/Default", order = 0)]

public class AttackSO : ScriptableObject
{
    [Header("Attack Info")]
    public float size;
    public float delay;
    public float power;
    public float speed;
    public LayerMask target;

    [Header("Knock Back Info")]
    public bool isOnknockback;
    public float konckbackPower;
    public float konckbacktime;
}
