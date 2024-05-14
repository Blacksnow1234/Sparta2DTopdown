using UnityEngine; // 유니티 엔진의 기본 네임스페이스
using UnityEngine.InputSystem; // 유니티 Input System 네임스페이스

public class PlayerInputController : TopDownController // TopDownController 클래스를 상속받는 PlayerInputController 클래스 정의
{
    private Camera camera; // 카메라 객체를 저장할 변수

    protected override void Awake()
    {
        base.Awake();
        camera = Camera.main; // mainCamera 태그가 붙은 카메라를 가져와서 변수에 할당
    }

    // 이동 입력에 대한 처리 함수
    public void OnMove(InputValue value)
    {
        Vector2 moveInput = value.Get<Vector2>().normalized; // 입력된 벡터 값을 정규화하여 이동 입력값으로 설정
        CallMoveEvent(moveInput); // 이동 이벤트 호출
        // 실제 움직임 처리는 여기서 직접 이루어지지 않고 PlayerMovement 클래스에서 처리됨
    }

    // 시점 이동 입력에 대한 처리 함수
    public void OnLook(InputValue value)
    {
        Vector2 newAim = value.Get<Vector2>(); // 입력된 벡터 값을 가져옴
        Vector2 worldPos = camera.ScreenToWorldPoint(newAim); // 입력된 벡터를 화면 상의 좌표로 변환 (카메라 좌표를 기준으로 월드 좌표계로 바꾸어라)
        newAim = (worldPos - (Vector2)transform.position).normalized; // 플레이어 위치를 기준으로 입력된 벡터를 정규화하여 시점 이동값으로 설정 (transform.position 에서 worldPos 가 어떤방향에 있나)

        CallLookEvent(newAim); // 시점 이동 이벤트 호출
    }

    public void OnFire(InputValue value)
    {
        IsAttacking = value.isPressed;
        Debug.Log(IsAttacking);
    }
}
