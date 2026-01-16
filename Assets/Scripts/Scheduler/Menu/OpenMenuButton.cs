using Scheduler.Menu;
using UnityEngine;
using UnityEngine.UI;

public class OpenMenuButton : MonoBehaviour
{
    [SerializeField] private MenuBase menu;
    
    private Button _button;

    void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(Open);
    }

    protected virtual void Open() => menu.Open();
}
