using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class LanguageDataDownloader : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
#if UNITY_EDITOR
        StartCoroutine(GetLanguageData("LangData"));
#endif
    }

    // Update is called once per frame
    void Update()
    {
    }

    private static IEnumerator GetLanguageData(string file_name)
    {
        const string url =
            "https://docs.google.com/spreadsheets/d/1hesSc4zZhZUb4E3G5mNXeo4dm9i3jVvcZo0996BWf6g/export?gid=2037201555&format=tsv";
        using (var www = UnityWebRequest.Get(url))
        {
            yield return www.Send();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
//                print(www.downloadHandler.text);
                var savePath = $"{Application.dataPath}/{file_name}.csv";
                File.WriteAllBytes(savePath, www.downloadHandler.data);
            }
        }
    }
}