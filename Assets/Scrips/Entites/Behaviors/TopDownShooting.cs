using System; // C# 기본 라이브러리
using UnityEngine;
using Random = UnityEngine.Random; // 유니티 엔진의 기본 기능을 사용하기 위한 네임스페이스

public class TopDownShooting : MonoBehaviour
{
    private TopDownController controller; // TopDownController 컴포넌트를 참조하기 위한 변수

    [SerializeField] private Transform projectileSpawnPosition; // 발사체 생성 위치를 지정하기 위한 변수
    private Vector2 aimDirection = Vector2.right; // 발사체의 발사 방향을 저장하기 위한 변수, 초기값은 오른쪽


    public GameObject testPrefab; // 테스트용 발사체 프리팹

    private ObjectPool pool;

    private void Awake()
    {
        controller = GetComponent<TopDownController>(); // TopDownController 컴포넌트를 가져옴
        pool = GameObject.FindObjectOfType<ObjectPool>();
    }

    private void Start()
    {
        controller.OnAttackEvent += OnShoot; // 공격 이벤트에 OnShoot 메서드를 등록
        controller.OnLookEvent += OnAim; // 시점 이동 이벤트에 OnAim 메서드를 등록
    }

    // 시점 이동 이벤트 핸들러
    private void OnAim(Vector2 direction)
    {
        aimDirection = direction; // 시점 이동 이벤트로부터 받은 방향을 aimDirection 변수에 저장
    }

    // 공격 이벤트 핸들러
    private void OnShoot(AttackSO attackSO)
    {
        RangedAttackSO rangedAttackSo = attackSO as RangedAttackSO;
        if (rangedAttackSo == null) return;

        float projecttilesAngleSpace = rangedAttackSo.multipleProjectilesAngel;
        int numberOfProjectilesPershot = rangedAttackSo.numberofProjectilesPerShot;

        // 발사체들의 각도를 계산하여 한 발당 여러 개의 발사체를 발사
        float minAngle = -(numberOfProjectilesPershot / 2f) * projecttilesAngleSpace + 0.5f * rangedAttackSo.multipleProjectilesAngel;
        for (int i = 0; i < numberOfProjectilesPershot; i++)
        {
            float angle = minAngle + i * projecttilesAngleSpace; // 발사체 각도 계산
            float randomSpread = Random.Range(-rangedAttackSo.spread, rangedAttackSo.spread); // 랜덤한 산개를 추가하여 정확도 조절
            angle += randomSpread;
            CreateProjectile(rangedAttackSo, angle); // 발사체 생성 및 초기화
        }

    }

    // 발사체 생성 함수
    private void CreateProjectile(RangedAttackSO rangedAttackSO, float angle)
    {
        GameObject obj = pool.SpawnFromPool(rangedAttackSO.bulletNameTag);
        obj.transform.position = projectileSpawnPosition.position; // 발사 위치로 이동
        ProjectileController attackController = obj.GetComponent<ProjectileController>(); // 발사체 컨트롤러 컴포넌트 가져오기
        attackController.InitializeAttack(RotateVector2(aimDirection, angle), rangedAttackSO); // 발사체 초기화 및 발사 방향 설정
    }

    // 2D 벡터를 주어진 각도만큼 회전하는 함수
    //RotateVector2 함수는 발사체의 초기 방향인 aimDirection 벡터를 주어진 각도(angle)만큼 회전하여 새로운 발사 방향을 얻습니다.
    //발사체는 플레이어가 조준하는 방향으로 발사될 수 있게 됩니다.
    private static Vector2 RotateVector2(Vector2 v, float angle)
    {
        return Quaternion.Euler(0f, 0f, angle) * v;
    }
}
