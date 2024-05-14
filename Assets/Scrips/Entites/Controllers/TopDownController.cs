using System; // C# 기본 라이브러리
using System.Collections; // 배열과 리스트와 같은 컬렉션 관련 기능 제공
using System.Collections.Generic; // 제네릭 컬렉션(자료구조) 관련 기능 제공
using UnityEngine; // 유니티 엔진의 기본 기능을 사용하기 위한 네임스페이스

public class TopDownController : MonoBehaviour // MonoBehaviour 클래스를 상속받는 TopDownController 클래스 정의
{
    public event Action<Vector2> OnMoveEvent; // 이동 이벤트를 위한 Action 델리게이트 정의
    public event Action<Vector2> OnLookEvent; // 시점 이동 이벤트를 위한 Action 델리게이트 정의
    public event Action<AttackSO> OnAttackEvent; // 공격 이벤트를 위한 Action 델리게이트 정의

    protected bool IsAttacking { get; set; } // 공격 중인지 여부를 나타내는 속성

    private float timeSinceLastAttack = float.MaxValue; // 마지막 공격 이후로 경과한 시간을 나타내는 변수

   // 프로텍티를 한 이유 : 나만 바꾸고 싶지만 가져가는건 내 상속받는 클래스들도 볼 수 있게
    protected CharacterStatHandler stats {  get; private set; }

    protected virtual void Awake()
    {
        stats = GetComponent<CharacterStatHandler>();
    }

    private void Update()
    {
        HandleAttackDelay(); // 공격 딜레이 처리 함수 호출
    }

    private void HandleAttackDelay()
    {
        // 마지막 공격 이후 시간이 delay보다 짧으면 시간 업데이트
        if (timeSinceLastAttack <= stats.CurrentStat.attackSO.delay)
        {
            timeSinceLastAttack += Time.deltaTime; // 경과 시간 누적
        }
        // 공격 중이고 마지막 공격 이후 시간이 0.2초 이상이면 공격 이벤트 호출
        else if (IsAttacking && timeSinceLastAttack >= stats.CurrentStat.attackSO.delay)
        {
            timeSinceLastAttack = 0f; // 시간 초기화
            CallAttackEvent(stats.CurrentStat.attackSO); // 공격 이벤트 호출
        }
    }

    // 이동 이벤트 호출 함수
    public void CallMoveEvent(Vector2 direction)
    {
        OnMoveEvent?.Invoke(direction); // 이동 이벤트 호출, 이벤트가 null이 아니면 Invoke 메서드 실행
    }

    // 시점 이동 이벤트 호출 함수
    public void CallLookEvent(Vector2 direction)
    {
        OnLookEvent?.Invoke(direction); // 시점 이동 이벤트 호출, 이벤트가 null이 아니면 Invoke 메서드 실행
    }

    // 공격 이벤트 호출 함수
    private void CallAttackEvent(AttackSO attackSO)
    {
        OnAttackEvent?.Invoke(attackSO); // 공격 이벤트 호출, 이벤트가 null이 아니면 Invoke 메서드 실행
    }
}
