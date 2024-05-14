using UnityEngine;

internal class ProjectileController : MonoBehaviour
{
    [SerializeField] private LayerMask levelCollisionLayer; // 발사체가 충돌할 레이어 마스크

    private bool isReady; // 발사체가 준비되었는지 여부를 나타내는 플래그

    private Rigidbody2D rigidbody; // Rigidbody2D 컴포넌트
    private SpriteRenderer spriteRenderer; // SpriteRenderer 컴포넌트
    private TrailRenderer trailRenderer; // TrailRenderer 컴포넌트

    private RangedAttackSO attackData; // 발사체에 대한 데이터
    private float currentDuration; // 현재 발사체가 존재한 시간
    private Vector2 direction; // 발사체의 방향

    private bool fxOnDestroy = true; // 발사체가 파괴될 때 파티클 효과를 생성할지 여부

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>(); // 자식 오브젝트에서 SpriteRenderer 컴포넌트 가져오기
        rigidbody = GetComponent<Rigidbody2D>(); // Rigidbody2D 컴포넌트 가져오기
        trailRenderer = GetComponent<TrailRenderer>(); // TrailRenderer 컴포넌트 가져오기
    }

    private void Update()
    {
        if (!isReady)
        {
            return;
        }

        currentDuration += Time.deltaTime;

        // 발사체의 수명이 지나면 파괴
        if (currentDuration > attackData.duration)
        {
            DestroyProjectile(transform.position, false);
        }

        rigidbody.velocity = direction * attackData.speed; // 발사체에 속도 적용
    }

    // 발사체 초기화 함수
    public void InitializeAttack(Vector2 direction, RangedAttackSO attackData)
    {
        this.attackData = attackData;
        this.direction = direction;

        UpdateProjectileSprite(); // 발사체의 스프라이트 업데이트
        trailRenderer.Clear(); // TrailRenderer 초기화
        currentDuration = 0; // 발사체 수명 초기화
        spriteRenderer.color = attackData.projectileColor; // 발사체의 색상 설정

        transform.right = this.direction; // 발사체를 주어진 방향으로 회전

        isReady = true; // 발사체가 준비됨
    }

    // 발사체 제거 함수
    private void DestroyProjectile(Vector3 position, bool createFX)
    {
        if (createFX)
        {
            // TODO : ParticleSystem에 대해서 배우고, 무기 NameTag로 해당하는 FX 가져오기
        }
        gameObject.SetActive(false); // 발사체 비활성화
    }

    // 발사체 스프라이트 업데이트 함수
    private void UpdateProjectileSprite()
    {
        transform.localScale = Vector3.one * attackData.size; // 발사체의 스케일 설정
    }

    // 충돌 처리 함수
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 레벨 충돌 레이어와 충돌했을 경우
        if (IsLayerMatched(levelCollisionLayer.value, collision.gameObject.layer))
        {
            // 발사체가 레벨과 충돌한 지점에서 약간 뒤로 이동하여 파괴
            Vector2 destroyPosition = collision.ClosestPoint(transform.position) - direction * 0.2f;
            DestroyProjectile(destroyPosition, fxOnDestroy);
        }
        // 목표 레이어와 충돌했을 경우
        else if (IsLayerMatched(attackData.target.value, collision.gameObject.layer))
        {
            // TODO: 데미지를 준다.
            DestroyProjectile(collision.ClosestPoint(transform.position), fxOnDestroy);
        }
    }

    // 레이어 일치 여부 확인 함수
    private bool IsLayerMatched(int value, int layer)
    {
        return value == (value | 1 << layer); // 레이어 마스크 비트 연산으로 일치 여부 확인
    }
}
