using System;
using System.Collections.Generic;

/// <summary>
/// Static event broker. Game logic fires events, UI subscribes.
/// Both legacy Canvas and UI Toolkit can listen.
/// </summary>
public static class RaceEvents
{
    // Speed
    public static event Action<float> OnSpeedChanged;
    public static void FireSpeedChanged(float speed) => OnSpeedChanged?.Invoke(speed);

    // Laps
    public static event Action<int, int> OnLapChanged;
    public static void FireLapChanged(int current, int total) => OnLapChanged?.Invoke(current, total);

    // Position / Leaderboard
    public static event Action<int> OnPlayerPositionChanged;
    public static void FirePlayerPositionChanged(int position) => OnPlayerPositionChanged?.Invoke(position);

    public static event Action<List<LeaderboardEntry>> OnLeaderboardChanged;
    public static void FireLeaderboardChanged(List<LeaderboardEntry> entries) => OnLeaderboardChanged?.Invoke(entries);

    // Hull
    public static event Action<int, int> OnHullChanged;
    public static void FireHullChanged(int current, int max) => OnHullChanged?.Invoke(current, max);

    // Weapons
    public static event Action<bool> OnWeaponsEnabled;
    public static void FireWeaponsEnabled(bool enabled) => OnWeaponsEnabled?.Invoke(enabled);

    public static event Action<int> OnMissileCountChanged;
    public static void FireMissileCountChanged(int count) => OnMissileCountChanged?.Invoke(count);

    public static event Action<int> OnHomingMissileCountChanged;
    public static void FireHomingMissileCountChanged(int count) => OnHomingMissileCountChanged?.Invoke(count);

    public static event Action<int> OnMineCountChanged;
    public static void FireMineCountChanged(int count) => OnMineCountChanged?.Invoke(count);

    // Race state
    public static event Action OnRaceStarted;
    public static void FireRaceStarted() => OnRaceStarted?.Invoke();

    public static event Action<int, int, int> OnRaceFinished;
    public static void FireRaceFinished(int position, int points, int money) => OnRaceFinished?.Invoke(position, points, money);

    // Weapon fire feedback
    public static event Action OnMissileFired;
    public static void FireMissileFired() => OnMissileFired?.Invoke();

    public static event Action OnMinePlaced;
    public static void FireMinePlaced() => OnMinePlaced?.Invoke();

    /// <summary>Call on scene unload to prevent stale subscriptions.</summary>
    public static void ClearAll()
    {
        OnSpeedChanged = null;
        OnLapChanged = null;
        OnPlayerPositionChanged = null;
        OnLeaderboardChanged = null;
        OnHullChanged = null;
        OnWeaponsEnabled = null;
        OnMissileCountChanged = null;
        OnHomingMissileCountChanged = null;
        OnMineCountChanged = null;
        OnRaceStarted = null;
        OnRaceFinished = null;
        OnMissileFired = null;
        OnMinePlaced = null;
    }
}

public struct LeaderboardEntry
{
    public string Name;
    public int Position;
    public int Hull;
    public bool IsPlayer;
    public bool IsWrecked;
    public bool IsFinished;
}
