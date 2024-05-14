using UnityEngine;

public class TopDownAimRotation : MonoBehaviour
{
    [SerializeField] private SpriteRenderer armRenderer; // 팔의 SpriteRenderer 컴포넌트
    [SerializeField] private Transform armPivot; // 팔의 회전 중심점(Transform)

    [SerializeField] private SpriteRenderer characterRenderer; // 캐릭터의 SpriteRenderer 컴포넌트

    private TopDownController _controller; // TopDownController 컴포넌트 참조 변수

    private void Awake()
    {
        _controller = GetComponent<TopDownController>(); // TopDownController 컴포넌트 가져오기
    }

    void Start()
    {
        // 마우스의 위치가 들어오는 OnLookEvent에 등록하는 것
        // 마우스의 위치를 받아서 팔을 돌리는 데 활용할 것임.
        _controller.OnLookEvent += OnAim; // OnLookEvent 이벤트에 OnAim 메서드를 등록
    }

    // 시점 이동 이벤트 핸들러
    public void OnAim(Vector2 newAimDirection)
    {
        // OnLook
        RotateArm(newAimDirection); // 팔을 회전시키는 함수 호출
    }

    // 팔을 회전시키는 함수
    private void RotateArm(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // 주어진 방향을 각도로 변환
        characterRenderer.flipX = Mathf.Abs(rotZ) > 90f; // 캐릭터의 이미지를 좌우 반전시키는 것이 필요한지 확인
        armPivot.rotation = Quaternion.Euler(0, 0, rotZ); // 팔을 주어진 각도로 회전시킴
    }
}
