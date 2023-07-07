using System.Collections.Generic;

public class IndexEnemy
{
    private int[] _indexNumbers = new int[4];

    public const int AWAILABLE = 0;
    public const int NOT_AWAILABLE = -1;

    private Queue<EnemyView> waitingsFreePlace = new();

    public int Get(EnemyView enemyView)
    {
        for (int i = 0; i < _indexNumbers.Length; i++)
        {
            if (_indexNumbers[i] == NOT_AWAILABLE) continue;

            _indexNumbers[i] = NOT_AWAILABLE;

            return i;
        }

        waitingsFreePlace.Enqueue(enemyView);

        return NOT_AWAILABLE;
    }

    public void Return(int index)
    {
        if (index == NOT_AWAILABLE) return;

        if(waitingsFreePlace.Count != 0)
        {
            EnemyView enemyView = waitingsFreePlace.Dequeue();
            enemyView.UpdatePositionIndex(index);
            return;
        }
        else
        {
            _indexNumbers[index] = AWAILABLE;
        }
    }
}