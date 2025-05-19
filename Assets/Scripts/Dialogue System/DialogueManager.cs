using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class DialogueManager : MonoBehaviour
{
    #region Singleton
    //public instance of self where there is only one in memory 
    public static DialogueManager instance;
    private void Awake()
    {
        //if its empty
        if (instance == null)
        {
            //fill the empty spot
            instance = this;
        }
        //otherwise if the instance reference has something there and the thing that is there is not this one
        else if (instance != null && instance != this)
        {
            //destroy this one
            Destroy(this);
        }
    }
    #endregion
    #region Dialogue GUI Elements
    [SerializeField] GameObject _dialogueBox;
    [SerializeField] Text _dialogueText;
    [SerializeField] Image _displayPicture;
    [SerializeField] Text _name;
    [SerializeField] Text _inputDisplay;
    #endregion
    #region Variables
    //lines to read
    [SerializeField] string[] _dialogueLines;
    //current line
    [SerializeField] int _currentIndex = 0;
    #endregion
    void Start()
    {
        OnDeactive();
    }
    public void OnActive(string[] lines, string name, Sprite face)
    {
        _dialogueBox.SetActive(true);
        _dialogueLines = lines;
        _currentIndex = 0;
        _displayPicture.sprite = face;
        _name.text = name;
        _dialogueText.text = _dialogueLines[_currentIndex];
        if (_currentIndex < _dialogueLines.Length - 1)
        {
            _inputDisplay.text = "Next";
        }
        else
        {
            _inputDisplay.text = "Bye!";
        }
    }
    void OnDeactive()
    {
        _dialogueBox.SetActive(false);
        _inputDisplay.text = "Next";
        _currentIndex = 0;
    }
    public void Input()
    {
        //not at end of dialogue and not the second last
        if (_currentIndex < _dialogueLines.Length - 2)
        {
            _inputDisplay.text = "Next";
            _currentIndex++;
        }
        //not the second last
        else if (_currentIndex < _dialogueLines.Length - 1)
        {
            _inputDisplay.text = "Bye!";
            _currentIndex++;
        }
        //at end
        else
        {
            OnDeactive();
        }
        _dialogueText.text = _dialogueLines[_currentIndex];
    }
}