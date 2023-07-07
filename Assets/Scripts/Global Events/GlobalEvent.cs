using System;

public class GlobalEvent
{
    public static event Action StartGame;
    public static event Action<int> WaypointAchieved;
    public static event Action NewEnemy;
    public static event Action DieEnemy;
    
    ~GlobalEvent()
    {
        WaypointAchieved = null;
        NewEnemy = null;
        DieEnemy = null;
        StartGame = null;
    }

    public static void InvokeWaypointAchieved(int numberWaypoint) => WaypointAchieved?.Invoke(numberWaypoint);
    public static int InvokeNewEnemy(EnemyView enemyView)
    {
        NewEnemy?.Invoke();

        return GameService.Singleton.IndexEnemy.Get(enemyView);
    }
    public static void InvokeDieEnemy(int index)
    {
        DieEnemy?.Invoke();
        GameService.Singleton.IndexEnemy.Return(index);
    }

    public static void InvokeStartGame() => StartGame?.Invoke();

   
}