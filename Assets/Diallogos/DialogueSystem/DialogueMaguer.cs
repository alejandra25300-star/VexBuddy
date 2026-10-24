using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DialogueSystemJadsa
{
    public class DialogueManager : MonoBehaviour
    {
        public static DialogueManager Instance { get; private set; }

        [Header("Dialogue UI")]
        [SerializeField] private RectTransform dialogBox;
        [SerializeField] private Image characterPhoto;
        [SerializeField] private TextMeshProUGUI characterName;
        [SerializeField] private TextMeshProUGUI dialogArea;
        [SerializeField] private Button continueButton;

        private Queue<DialogueTurn> dialogueTurnsQueue;
        private bool advanceRequested;
        private bool isDialogueRunning;

        private void Awake()
        {
            Instance = this;

            if (continueButton != null)
                continueButton.onClick.AddListener(AdvanceDialogue);

            HideDialogBox();
        }

        public void StartDialogue(DialogueRoundSO dialogue)
        {
            if (dialogue == null)
                return;

            StopAllCoroutines();
            isDialogueRunning = false;

            dialogueTurnsQueue =
                new Queue<DialogueTurn>(dialogue.DialogueTurnsList);

            if (dialogueTurnsQueue.Count == 0)
            {
                HideDialogBox();
                return;
            }

            StartCoroutine(DialogueCoroutine());
        }

        private IEnumerator DialogueCoroutine()
        {
            isDialogueRunning = true;
            ShowDialogBox();

            while (dialogueTurnsQueue.Count > 0)
            {
                DialogueTurn currentTurn = dialogueTurnsQueue.Dequeue();

                SetCharacterInfo(currentTurn.Character);

                if (dialogArea != null)
                    dialogArea.text = currentTurn.DialogueLine;

                advanceRequested = false;

                yield return new WaitUntil(() => advanceRequested);
            }

            HideDialogBox();
            isDialogueRunning = false;
        }

        public void AdvanceDialogue()
        {
            if (isDialogueRunning)
                advanceRequested = true;
        }

        public void ShowDialogBox()
        {
            if (dialogBox != null)
                dialogBox.gameObject.SetActive(true);
        }

        public void HideDialogBox()
        {
            if (dialogBox != null)
                dialogBox.gameObject.SetActive(false);
        }

        public void SetCharacterInfo(DialogueCharacterSO character)
        {
            if (character == null)
                return;

            if (characterPhoto != null)
                characterPhoto.sprite = character.ProfilePhoto;

            if (characterName != null)
                characterName.text = character.Name;
        }

        private void OnDestroy()
        {
            if (continueButton != null)
                continueButton.onClick.RemoveListener(AdvanceDialogue);
        }
    }
}