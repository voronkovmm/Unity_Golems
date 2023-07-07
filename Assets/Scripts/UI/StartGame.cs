using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    [SerializeField] private Button _startGame;
    [SerializeField] private TMP_Text _tmp;

    [Header("Animation")]
    [SerializeField] private float speedSize = 0.5f;

    private void Awake()
    {
        _startGame.onClick.AddListener(OnStartGame);
        
        StartCoroutine(TextAnimation());
    }

    private void OnStartGame()
    {
        StopCoroutine(TextAnimation());
        GlobalEvent.InvokeStartGame();
        _startGame.gameObject.SetActive(false);
    }

    private IEnumerator TextAnimation()
    {
        float startSize = _tmp.fontSize;
        float endSize = _tmp.fontSize + 10;
        
        float firstValue = startSize;
        float secondValue = endSize;
        
        float timer = 0;

        while (true) 
        {
            timer += speedSize * Time.deltaTime;

            _tmp.fontSize = Mathf.Lerp(firstValue, secondValue, timer);

            if(timer >= 1)
            {
                firstValue = _tmp.fontSize;
                secondValue = (firstValue == endSize) ? startSize : endSize;
                timer = 0;
            }

            yield return null;
        }
    }
}
