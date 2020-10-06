using UnityEngine.Events;
using UnityEngine;
using TMPro;

public class Clock : MonoBehaviour
{
    public float clockSpeed, startTime, endTime;
    private float _currentTime;

    private UnityEvent onTimeUp;
    public UnityEvent OnTimeUp => onTimeUp;

    private TextMeshProUGUI _clockText;
    private void Awake()
    {
        _clockText = GetComponent<TextMeshProUGUI>();
    }
    private void Start()
    {
        _currentTime = startTime;
    }

    void Update()
    {
        _currentTime += Time.deltaTime * clockSpeed;

        int minutes = (int)(_currentTime % 60);
        int hours = (int)(_currentTime / 60) % 24;

        _clockText.text = string.Format("{0:00}:{1:00}", hours, minutes);

        if (_currentTime == endTime)
        {
            TimeUp();
        }
    }

    public void TimeUp()
    {
        onTimeUp?.Invoke();
    }
}
