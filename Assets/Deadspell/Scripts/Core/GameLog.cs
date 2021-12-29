using System.Collections;
using System.Collections.Generic;
using System.Text;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class GameLog : MonoBehaviour
{
    public const int MAX_LOG_LINES = 6;
    public const int MAX_STORED_LOG_LINES = 50;
    
    private static GameLog s_instance;
    public string[] DefaultLogLines;
    [ShowInInspector]
    private List<string> _log = new List<string>();
    private TextMeshProUGUI _logText;

    void Awake()
    {
        s_instance = this;
        _logText = GetComponent<TextMeshProUGUI>();
        for (int i = 0; i < DefaultLogLines.Length; i++)
        {
            AddLog(DefaultLogLines[i]);
        }
    }

    private void AddLog(string newLine)
    {
        _log.Add(newLine);

        while (_log.Count > MAX_STORED_LOG_LINES)
        {
            _log.RemoveAt(0);
        }
        
        StringBuilder sb = new StringBuilder();
        int start = Mathf.Max(0, _log.Count - (MAX_LOG_LINES + 1));
        for (int i = start; i < _log.Count; i++)
        {
            sb.Append(":: ");
            sb.Append(_log[i]);
            if (i != _log.Count - 1)
            {
                sb.Append("\n");
            }
        }
        _logText.SetText(sb);
    }

    public static void Log(string text)
    {
        if (s_instance != null)
            s_instance.AddLog(text);
    }
}
