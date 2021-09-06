using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInputManager : MonoBehaviour
{
    private bool _validClick;

    private Vector3 _lastPosClicked;
    public static PlayerInputManager Instance { get; private set; }

    public bool LasserAttackPressed { get; private set; }
    public bool MissileAttackPressed { get; private set; }
    public bool CancelPrimaryAttackPressed { get; private set; }
    public Vector3 TargetPositionToMove { get; private set; }

    public bool ClickedPosChanged => _lastPosClicked != TargetPositionToMove;
    public ITargetable CurrentTarget { get; private set; }

    private void Awake()
    {
        if (Instance != null)
            Destroy(this);

        Instance = this;
        DontDestroyOnLoad(this);
    }
    private void Update()
    {
        ReadAttackKeys();

        _lastPosClicked = TargetPositionToMove;

        if (Input.GetMouseButtonDown(0))
            _validClick = VerifyValidClick();

        if (Input.GetMouseButtonUp(0))
            _validClick = true;


        if (_validClick == false)
            return;

        ReadMoveInput();
    }
    private bool VerifyValidClick()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return false;
        var hit = GetHitFromRaycast();

        var targetableObject = hit.collider.GetComponent<ITargetable>();
        if (targetableObject != null)
        {
            if (targetableObject.CanBeTargetable)
            {
                CurrentTarget = targetableObject;
                return false;
            }
        }
        return true;
    }
    private void ReadMoveInput()
    {
        if (Input.GetMouseButton(0))
        {
            var hit = GetHitFromRaycast();
            TargetPositionToMove = hit.point;
        }
    }
    private void ReadAttackKeys()
    {
        LasserAttackPressed = Input.GetKeyDown(KeyCode.LeftControl);

        MissileAttackPressed = Input.GetButton("Fire2");

        CancelPrimaryAttackPressed = Input.GetKeyDown(KeyCode.Escape);
    }
    private RaycastHit GetHitFromRaycast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out var hit, 100f);
        return hit;
    }
    Vector2 mouseClickPosition;
    private void LateUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out var hit, 100))
        {
            mouseClickPosition = hit.point;
        }
    }
}