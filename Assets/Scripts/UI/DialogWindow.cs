using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogWindow : MonoBehaviour
{

    #region Variables
    internal string[] PagesText;

    private int index;

    [Header("NPC Profil Pic")]
    public Image ProfilImage;
    [Header("NPC Name")]
    public TextMeshProUGUI ProfilName;
    [Header("NPC Dialog")]
    [SerializeField]
    private TextMeshProUGUI _textComponent;
    [SerializeField]
    private float _textSpeed;

    #endregion

    #region Unity Func

    private void Start()
    {
        _textComponent.text = string.Empty;
        StartDialog();
    }

    private void Update()
    {
        if (GameManager.Instance.SkipButtonPresses)
        {
            if (_textComponent.text == PagesText[index])
            {
                GoToNextLine();
            }
            else
            {
                StopAllCoroutines();
                _textComponent.text = PagesText[index];
            }
        }
    }

    #endregion

    #region Built Func

    private void StartDialog()
    {
        index = 0;
        StartCoroutine(TypeLines());
    }

    IEnumerator TypeLines()
    {
        foreach (char c in PagesText[index].ToCharArray())
        {
            _textComponent.text += c;
            yield return new WaitForSeconds(_textSpeed);
        }
    }

    private void GoToNextLine()
    {
        if (index < PagesText.Length - 1)
        {
            index++;
            _textComponent.text = string.Empty;
            StartCoroutine(TypeLines());
        }
        else
        {
            GameManager.Instance.PlayerCannotTurn = false;
            gameObject.SetActive(false);
        }
    }

    #endregion

}
