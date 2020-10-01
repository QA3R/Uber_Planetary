using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Febucci.UI;
using UberPlanetary.ScriptableObjects;

public class DialogueController : MonoBehaviour
{
    // Dialogue Box objects
    public TextMeshProUGUI custName;
    public TextMeshProUGUI dialogueBox;
    public Image custFace;

    public TextAnimator textAnimator;
    private TextAnimatorPlayer textAnimatorPlayer;

    [SerializeField]
    private CustomerSO customerSO;
    [SerializeField]
    private DialogueSO dialogueSO;
    private Dialogue dialogue;

    private void Awake()
    {
        textAnimatorPlayer = GetComponent<TextAnimatorPlayer>();
        dialogue = GetComponent<Dialogue>();
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            PlayNextLine();
        }
    }
    public void InitiateDialogue()
    {
        custName.text = customerSO.CustomerName;
        custFace.color = new Color(1, 1, 1, 1);
        custFace.sprite = customerSO.CustomerFace;
        textAnimatorPlayer.ShowText(dialogueSO.lines[0]);
        

    }
    public void PlayNextLine()
    {
        dialogue.nextText.SetActive(false);
        textAnimatorPlayer.ShowText(dialogueSO.lines[dialogue.LineIndex]);
    }
    public void InterruptDialogue()
    {

    }
    

}
