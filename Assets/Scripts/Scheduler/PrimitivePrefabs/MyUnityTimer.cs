using UnityEngine;
using UnityEngine.Events;

namespace Scheduler
{
    public class MyUnityTimer : MonoBehaviour
    {
        public enum TimerMode
        {
            SingleShot,
            Continuous,
        }
        public UnityEvent OnFinish {get; private set;} = new();
        public bool IsRunning { get; private set; } = false;

        private double _targetTime, _currentTime;
        private TimerMode _mode;

        public void StartTimer(double targetTime, TimerMode mode = TimerMode.SingleShot)
        {
            _mode = mode;
            _targetTime = targetTime;
            _currentTime = 0;
            IsRunning = true;
        }
        
        public void StopTimer() => IsRunning = false;
        
        void FixedUpdate()
        {
            if (!IsRunning) return;
            _currentTime += Time.deltaTime;
            if (!(_currentTime >= _targetTime)) return;
            OnFinish.Invoke();
            _currentTime = 0;
            IsRunning = _mode == TimerMode.Continuous;
        }
    }
}
