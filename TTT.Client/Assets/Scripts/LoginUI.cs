using UnityEngine;
using UnityEngine.UI;

public class LoginUI : MonoBehaviour
{
    private Button _loginButton;
    private Button _sendButton;

    void Start()
    {
        _loginButton = transform.Find("Connect").GetComponent<Button>();
        _loginButton.onClick.AddListener(Connect);

        _sendButton = transform.Find("Send").GetComponent<Button>();
        _sendButton.onClick.AddListener(Send);
    }

    void Connect()
    {
        NetworkClient.Instance.Connect();
    }

    void Send()
    {
        NetworkClient.Instance.SendServer("Hello there!");
    }
}
