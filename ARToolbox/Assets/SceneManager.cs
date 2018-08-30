using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SceneManager : MonoBehaviour
{




}






//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.Windows.Speech;
//using System.Linq;
//using System;

//public class SceneManager : MonoBehaviour
//{
//    KeywordRecognizer keywordRecognizer;
//    Dictionary<string, System.Action> keywords = new Dictionary<string, Action>();

//    private void Start()
//    {
//        keywords.Add("Hello", () => { HelloCalled(); });
//        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());
//        keywordRecognizer.OnPhraseRecognized += PhraseRecognized;
//        keywordRecognizer.Start();
//        Debug.Log("Started");
//    }

//    void HelloCalled()
//    {
//        Debug.Log("HELLO");
//    }

//    void PhraseRecognized(PhraseRecognizedEventArgs args)
//    {
//        System.Action keywordAction;
//        if (keywords.TryGetValue(args.text, out keywordAction))
//        {
//            keywordAction.Invoke();
//        }
//    }

//}

