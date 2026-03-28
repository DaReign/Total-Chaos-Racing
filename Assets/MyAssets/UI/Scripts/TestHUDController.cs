using UnityEngine;
using UnityEngine.UIElements;
using UnityStandardAssets.Vehicles.Car;

public class TestHUDController : UIScreenBase
{
    // HUD elements
    Label _speedValue;
    Label _lapLabel;
    Label _positionLabel;
    VisualElement _hullBar;
    Label _hullText;
    Button _missileBtn;
    Button _homingBtn;
    Button _mineBtn;
    Button _backToTrackBtn;

    // Driving controls
    Button _steerLeftBtn;
    Button _steerRightBtn;
    Button _gasBtn;
    Button _brakeBtn;

    // Virtual joystick
    VisualElement _joystickArea;
    VisualElement _joystickBase;
    VisualElement _joystickHandle;
    bool _joystickActive;
    float _joystickCenterX;

    // Dirty flags (HUD data)
    bool _speedDirty, _lapDirty, _posDirty, _hullDirty, _ammoDirty;
    float _cachedSpeed;
    int _cachedLap, _cachedTotalLaps;
    int _cachedPosition;
    int _cachedHull, _cachedHullMax;
    int _cachedMissiles, _cachedHoming, _cachedMines;

    // Player references (cached lazily — car spawns after HUD)
    GameObject _player;
    CarUserControl _carUserControl;
    MissileFire _missileFire;
    MinePlacer _minePlacer;
    CarStats _carStats;
    bool _playerResolved;

    // Weapon cooldown tracking
    float _missileCooldownEnd;
    float _homingCooldownEnd;
    float _mineCooldownEnd;

    bool EnsurePlayer()
    {
        if (_playerResolved) return _player != null;
        _player = GameObject.FindWithTag("Player");
        if (_player == null) return false;
        _carUserControl = _player.GetComponent<CarUserControl>();
        _missileFire = _player.GetComponent<MissileFire>();
        _minePlacer = _player.GetComponent<MinePlacer>();
        _carStats = _player.GetComponent<CarStats>();
        _playerResolved = true;
        return true;
    }

    protected override void QueryElements()
    {
        // HUD
        _speedValue = Root.Q<Label>("speed-value");
        _lapLabel = Root.Q<Label>("lap-label");
        _positionLabel = Root.Q<Label>("position-label");
        _hullBar = Root.Q("hull-bar-fill");
        _hullText = Root.Q<Label>("hull-text");
        _missileBtn = Root.Q<Button>("missile-btn");
        _homingBtn = Root.Q<Button>("homing-btn");
        _mineBtn = Root.Q<Button>("mine-btn");
        _backToTrackBtn = Root.Q<Button>("back-to-track-btn");

        // Driving controls
        _steerLeftBtn = Root.Q<Button>("steer-left-btn");
        _steerRightBtn = Root.Q<Button>("steer-right-btn");
        _gasBtn = Root.Q<Button>("gas-btn");
        _brakeBtn = Root.Q<Button>("brake-btn");

        // Virtual joystick
        _joystickArea = Root.Q("joystick-area");
        _joystickBase = Root.Q("joystick-base");
        _joystickHandle = Root.Q("joystick-handle");

        // Player lookup is deferred — car might not exist yet
        EnsurePlayer();

        WireDrivingControls();
        WireJoystick();
        WireWeaponButtons();
        DisableOldCanvas();
    }

    void WireDrivingControls()
    {
        // IMPORTANT: Must use TrickleDown — Button's Clickable calls StopImmediatePropagation
        // on PointerDown/Up in BubbleUp phase, killing any other BubbleUp handlers.
        _steerLeftBtn?.RegisterCallback<PointerDownEvent>(e => { if (EnsurePlayer()) _carUserControl.turnLeftOn(); }, TrickleDown.TrickleDown);
        _steerLeftBtn?.RegisterCallback<PointerUpEvent>(e => { _carUserControl?.turnLeftOff(); }, TrickleDown.TrickleDown);
        _steerLeftBtn?.RegisterCallback<PointerLeaveEvent>(e => { _carUserControl?.turnLeftOff(); });

        _steerRightBtn?.RegisterCallback<PointerDownEvent>(e => { if (EnsurePlayer()) _carUserControl.turnRightOn(); }, TrickleDown.TrickleDown);
        _steerRightBtn?.RegisterCallback<PointerUpEvent>(e => { _carUserControl?.turnRightOff(); }, TrickleDown.TrickleDown);
        _steerRightBtn?.RegisterCallback<PointerLeaveEvent>(e => { _carUserControl?.turnRightOff(); });

        _gasBtn?.RegisterCallback<PointerDownEvent>(e => { if (EnsurePlayer()) _carUserControl.accOn(); }, TrickleDown.TrickleDown);
        _gasBtn?.RegisterCallback<PointerUpEvent>(e => { _carUserControl?.accOff(); }, TrickleDown.TrickleDown);
        _gasBtn?.RegisterCallback<PointerLeaveEvent>(e => { _carUserControl?.accOff(); });

        _brakeBtn?.RegisterCallback<PointerDownEvent>(e => { if (EnsurePlayer()) _carUserControl.BrakeOn(); }, TrickleDown.TrickleDown);
        _brakeBtn?.RegisterCallback<PointerUpEvent>(e => { _carUserControl?.BrakeOff(); }, TrickleDown.TrickleDown);
        _brakeBtn?.RegisterCallback<PointerLeaveEvent>(e => { _carUserControl?.BrakeOff(); });
    }

    void WireJoystick()
    {
        if (_joystickArea == null || _joystickBase == null || _joystickHandle == null) return;

        _joystickArea.RegisterCallback<PointerDownEvent>(e =>
        {
            _joystickActive = true;
            _joystickCenterX = _joystickBase.worldBound.center.x;
            UpdateJoystickHandle(e.position.x);
            _joystickArea.CapturePointer(e.pointerId);
        });

        _joystickArea.RegisterCallback<PointerMoveEvent>(e =>
        {
            if (!_joystickActive) return;
            UpdateJoystickHandle(e.position.x);
        });

        _joystickArea.RegisterCallback<PointerUpEvent>(e =>
        {
            ResetJoystick();
            _joystickArea.ReleasePointer(e.pointerId);
        });

        _joystickArea.RegisterCallback<PointerLeaveEvent>(e =>
        {
            ResetJoystick();
        });
    }

    void UpdateJoystickHandle(float pointerX)
    {
        if (!EnsurePlayer()) return;

        float baseWidth = _joystickBase.resolvedStyle.width;
        float halfBase = baseWidth * 0.5f;
        float handleWidth = _joystickHandle.resolvedStyle.width;

        // Calculate offset from center (–1..1)
        float offset = (pointerX - _joystickCenterX) / halfBase;
        offset = Mathf.Clamp(offset, -1f, 1f);

        // Move handle visually
        float maxTravel = halfBase - handleWidth * 0.5f;
        _joystickHandle.style.left = halfBase + offset * maxTravel - handleWidth * 0.5f;

        // Set steering axis on CarUserControl
        _carUserControl.touchSteerAxis = offset;
    }

    void ResetJoystick()
    {
        _joystickActive = false;
        if (_joystickHandle != null)
        {
            float baseWidth = _joystickBase.resolvedStyle.width;
            float handleWidth = _joystickHandle.resolvedStyle.width;
            _joystickHandle.style.left = (baseWidth - handleWidth) * 0.5f;
        }
        if (_carUserControl != null)
            _carUserControl.touchSteerAxis = 0f;
    }

    void WireWeaponButtons()
    {
        _missileBtn?.RegisterCallback<ClickEvent>(e => FireMissile());
        _homingBtn?.RegisterCallback<ClickEvent>(e => FireHomingMissile());
        _mineBtn?.RegisterCallback<ClickEvent>(e => PlaceMine());
        _backToTrackBtn?.RegisterCallback<ClickEvent>(e => BackToTrack());
    }

    void FireMissile()
    {
        if (!EnsurePlayer() || _missileFire == null || _carStats == null) return;
        if (Time.time < _missileCooldownEnd) return;
        if (_carStats.Missiles <= 0) return;

        _missileFire.missile(0);
        float delay = 3.0f - (_carStats.Car * 0.2f) - (_carStats.Gun * 0.1f);
        _missileCooldownEnd = Time.time + delay;
    }

    void FireHomingMissile()
    {
        if (!EnsurePlayer() || _missileFire == null || _carStats == null) return;
        if (Time.time < _homingCooldownEnd) return;
        if (_carStats.HomingMissiles <= 0) return;

        _missileFire.missile(1);
        float delay = 3.0f - (_carStats.Car * 0.2f) - (_carStats.Gun * 0.1f);
        _homingCooldownEnd = Time.time + delay;
    }

    void PlaceMine()
    {
        if (!EnsurePlayer() || _minePlacer == null || _carStats == null) return;
        if (Time.time < _mineCooldownEnd) return;
        if (_carStats.Mines <= 0) return;

        _minePlacer.mine();
        float delay = 3.0f - (_carStats.Car * 0.2f) - (_carStats.Gun * 0.1f);
        _mineCooldownEnd = Time.time + delay;
    }

    void BackToTrack()
    {
        if (!EnsurePlayer()) return;
        var global = GameObject.Find("Global");
        if (global == null) return;

        var waypointsList = global.GetComponent<WaypointsList>();
        var human = _player.GetComponent<Human>();
        if (waypointsList == null || human == null) return;

        Transform target;
        if (human.currentWaypoint - 1 > 0)
            target = waypointsList.listOfWaypoints[human.currentWaypoint - 1].transform;
        else
            target = waypointsList.listOfWaypoints[waypointsList.listOfWaypoints.Count - 1].transform;

        _player.transform.position = new Vector3(target.position.x, target.position.y + 50, target.position.z);
        _player.transform.localEulerAngles = new Vector3(0, 0, 0);
        _player.transform.LookAt(human.Target);
    }

    void DisableOldCanvas()
    {
        // Disable old MobileSingleStickControl canvas
        var oldMobileCanvas = GameObject.Find("MobileSingleStickControl");
        if (oldMobileCanvas != null) oldMobileCanvas.SetActive(false);

        // Disable old SpeedText
        var speedText = GameObject.Find("SpeedText");
        if (speedText != null) speedText.SetActive(false);
    }

    protected override void SubscribeEvents()
    {
        RaceEvents.OnSpeedChanged += OnSpeed;
        RaceEvents.OnLapChanged += OnLap;
        RaceEvents.OnPlayerPositionChanged += OnPosition;
        RaceEvents.OnHullChanged += OnHull;
        RaceEvents.OnMissileCountChanged += OnMissiles;
        RaceEvents.OnHomingMissileCountChanged += OnHoming;
        RaceEvents.OnMineCountChanged += OnMines;
    }

    protected override void UnsubscribeEvents()
    {
        RaceEvents.OnSpeedChanged -= OnSpeed;
        RaceEvents.OnLapChanged -= OnLap;
        RaceEvents.OnPlayerPositionChanged -= OnPosition;
        RaceEvents.OnHullChanged -= OnHull;
        RaceEvents.OnMissileCountChanged -= OnMissiles;
        RaceEvents.OnHomingMissileCountChanged -= OnHoming;
        RaceEvents.OnMineCountChanged -= OnMines;
    }

    void OnSpeed(float s)    { _cachedSpeed = s; _speedDirty = true; }
    void OnLap(int c, int t) { _cachedLap = c; _cachedTotalLaps = t; _lapDirty = true; }
    void OnPosition(int p)   { _cachedPosition = p; _posDirty = true; }
    void OnHull(int c, int m){ _cachedHull = c; _cachedHullMax = m; _hullDirty = true; }
    void OnMissiles(int n)   { _cachedMissiles = n; _ammoDirty = true; }
    void OnHoming(int n)     { _cachedHoming = n; _ammoDirty = true; }
    void OnMines(int n)      { _cachedMines = n; _ammoDirty = true; }

    void Update()
    {
        if (!EnsurePlayer()) return;

        // Keyboard / WASD / Gamepad input (works alongside touch controls)
        float kbSteer = 0f;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))  kbSteer -= 1f;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) kbSteer += 1f;

        bool kbGas   = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        bool kbBrake = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);

        // Gamepad: left stick horizontal + triggers/buttons
        float gpSteer = Input.GetAxis("Horizontal");
        bool  gpGas   = Input.GetAxis("Vertical") > 0.1f;
        bool  gpBrake = Input.GetAxis("Vertical") < -0.1f;

        // Combine keyboard + gamepad
        float steer = Mathf.Abs(kbSteer) > 0.01f ? kbSteer : gpSteer;
        bool  gas   = kbGas  || gpGas;
        bool  brake = kbBrake || gpBrake;

        _carUserControl.kbSteerAxis = steer;
        _carUserControl.kbGas = gas;
        _carUserControl.kbBrake = brake;
    }

    void LateUpdate()
    {
        if (!EnsureElements()) return;

        if (_speedDirty)
        {
            _speedValue.text = _cachedSpeed.ToString("F0");
            _speedDirty = false;
        }
        if (_lapDirty)
        {
            _lapLabel.text = $"Lap {_cachedLap}/{_cachedTotalLaps}";
            _lapDirty = false;
        }
        if (_posDirty)
        {
            _positionLabel.text = $"P{_cachedPosition}";
            _posDirty = false;
        }
        if (_hullDirty)
        {
            float pct = _cachedHullMax > 0 ? (float)_cachedHull / _cachedHullMax : 0;
            _hullBar.style.width = Length.Percent(pct * 100);
            _hullText.text = _cachedHull.ToString();
            _hullDirty = false;
        }
        if (_ammoDirty)
        {
            _missileBtn.text = $"Missile: {_cachedMissiles}";
            _homingBtn.text = $"Homing: {_cachedHoming}";
            _mineBtn.text = $"Mine: {_cachedMines}";
            _ammoDirty = false;
        }
    }
}
