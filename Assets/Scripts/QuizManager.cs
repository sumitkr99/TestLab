using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    public QuickDataScriptableObject data;
    public SoundManager soundManager;
    public GameplayManager gameplayManager;
    public TMP_Text quizTimer;
    public TMP_Text question;
    public List<Button> options;

    public Sprite unselectedSprite, wrongSprite, rightSprite;
    // public GameObject coinCashParticle;

    public List<string> questionsList = new List<string>();
    public List<string> options0List = new List<string>();
    public List<string> options1List = new List<string>();
    public List<string> options2List = new List<string>();
    public List<int> correctOptionIndexList = new List<int>();

    private int _randomQuestionIndex;
    private int _correctOption;
    private bool _isAnswered;
    private float _quizExpireTimer;

    public delegate void QuizQuestionDelegate(string ques, string op1, string op2, string op3, string ans);

    public QuizQuestionDelegate quizQuestionDelegate;


    // Start is called before the first frame update
    private void Start()
    {
        quizQuestionDelegate = OnQuizDelegate;
        StartCoroutine(API.GetQuizQuestionsEnumerator(quizQuestionDelegate, SetLocalQuizQuestion));


        foreach (var btn in options)
        {
            btn.onClick.AddListener(delegate { OnOptionClick(btn); });
        }

        quizTimer.text = data.quizDuration.ToString();
        // coinCashParticle.SetActive(false);
    }

    private void OnQuizDelegate(string ques, string op1, string op2, string op3, string ans)
    {
        print(ques);
        question.text = ques;
        options[0].GetComponentInChildren<TMP_Text>().text = op1;
        options[1].GetComponentInChildren<TMP_Text>().text = op2;
        options[2].GetComponentInChildren<TMP_Text>().text = op3;
        _correctOption = int.Parse(ans.Substring(ans.Length - 1)) - 1;
    }

    private void SetLocalQuizQuestion()
    {
        _randomQuestionIndex = Random.Range(0, questionsList.Count);
        question.text = questionsList[_randomQuestionIndex];
        options[0].GetComponentInChildren<TMP_Text>().text = options0List[_randomQuestionIndex];
        options[1].GetComponentInChildren<TMP_Text>().text = options1List[_randomQuestionIndex];
        options[2].GetComponentInChildren<TMP_Text>().text = options2List[_randomQuestionIndex];
        _correctOption = correctOptionIndexList[_randomQuestionIndex];
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnOptionClick(Button btn)
    {
        _isAnswered = true;
        DisableAllOptions();
        if (options.IndexOf(btn) == _correctOption)
        {
            PlaySoundImmediately(soundManager.rightOption);

            Invoke(nameof(RightAnswer), 1);
            btn.GetComponent<Image>().sprite = rightSprite;
        }
        else
        {
            PlaySoundImmediately(soundManager.wrongOption);
            Invoke(nameof(WrongAnswer), 1);
            btn.GetComponent<Image>().sprite = wrongSprite;
        }
    }

    private void DisableAllOptions()
    {
        foreach (var btn in options)
        {
            btn.interactable = false;
        }
    }

    public void StartQuiz()
    {
        StartCoroutine(QuizTimer(data.quizDuration));
        Invoke(nameof(PlayQuizOnSound), data.quizShowTime);
    }

    private void EndTimer()
    {
        DisableAllOptions();
        if (!_isAnswered)
        {
            PlaySoundImmediately(soundManager.timesUp);
            Invoke(nameof(WrongAnswer), 1);
        }
    }

    private void RightAnswer()
    {
        gameplayManager.OnRightAnswer();
    }

    private void WrongAnswer()
    {
        gameplayManager.OnWrongAnswer();
    }

    private IEnumerator QuizTimer(float duration)
    {
        yield return new WaitForSeconds(data.duration + data.quizShowTime);
        _quizExpireTimer = duration;
        var lastTime = _quizExpireTimer.ToString();
        while (_quizExpireTimer > 0f)
        {
            _quizExpireTimer -= Time.deltaTime;
            if (_quizExpireTimer > 0)
            {
                var minutes = Mathf.Floor(_quizExpireTimer / 60).ToString("00");
                var seconds = (_quizExpireTimer % 60).ToString("0");
                // quizTimer.text = $"{minutes}:{seconds}";
                quizTimer.text = $"{seconds}";

                if (lastTime != seconds && !_isAnswered)
                {
                    lastTime = seconds;
                    PlaySoundImmediately(soundManager.wheelTick);
                }
            }

            yield return null;
        }

        EndTimer();
    }

    private void PlayQuizOnSound()
    {
        soundManager.gameMusicAS.Stop();
        PlaySoundImmediately(soundManager.quizOn);
    }

    private void PlaySoundImmediately(GameObject soundObject)
    {
        if (PlayerPrefs.GetString("SoundState").Equals(false.ToString())) return;
        Instantiate(soundObject);
    }
}