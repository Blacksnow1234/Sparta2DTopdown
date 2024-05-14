using UnityEngine; // 유니티 엔진의 기본 네임스페이스

public class TopDownMovement : MonoBehaviour // MonoBehaviour 클래스를 상속받는 TopDownMovement 클래스 정의
{
    private TopDownController controller; // TopDownController 스크립트를 참조할 변수
    private Rigidbody2D movementRigidbody; // Rigidbody2D 컴포넌트를 저장할 변수
    private CharacterStatHandler characterStatHandler;

    private Vector2 movementDirection = Vector2.zero; // 이동 방향을 저장할 변수, 초기값은 Vector2.zero

    private void Awake()
    {
        controller = GetComponent<TopDownController>(); // 현재 게임 오브젝트에서 TopDownController 컴포넌트를 가져와서 변수에 할당
        movementRigidbody = GetComponent<Rigidbody2D>(); // 현재 게임 오브젝트에서 Rigidbody2D 컴포넌트를 가져와서 변수에 할당
        characterStatHandler = GetComponent<CharacterStatHandler>();
    }

    private void Start()
    {
        controller.OnMoveEvent += Move; // 이동 이벤트가 발생했을 때 Move 메서드를 호출하도록 이벤트에 추가
    }

    private void Move(Vector2 direction)
    {
        movementDirection = direction; // 이동 이벤트에서 받은 방향 값을 movementDirection 변수에 저장
    }

    private void FixedUpdate() // 물리 업데이트 관련 함수, 물리 업데이트마다 호출됨
    {
        ApplyMovement(movementDirection); // 이동 방향을 적용하는 함수 호출
    }

    private void ApplyMovement(Vector2 direction)
    {
        direction = direction * characterStatHandler.CurrentStat.speed; // 이동 방향에 스칼라 값을 곱하여 이동 속도 조절
        movementRigidbody.velocity = direction; // Rigidbody2D의 속도를 이동 방향으로 설정하여 실제로 이동
    }
}

// 이 스크립트는 TopDownController에서 받은 이동 이벤트를 처리하여 실제로 플레이어를 움직이는 역할을 합니다