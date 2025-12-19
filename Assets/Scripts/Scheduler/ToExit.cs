using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ToExit : MonoBehaviour
{
    private Button toExitButton;
    void Awake()
    {
        toExitButton = GetComponent<Button>();
    }
    void Start()
    {
        toExitButton.onClick.AddListener(() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1));
    }

    void OnDestroy()
    {
        toExitButton.onClick.RemoveAllListeners();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
