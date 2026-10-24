using System.Collections.Generic;
using UnityEngine;

namespace DialogueSystemJadsa
{
    [CreateAssetMenu(
        fileName = "New Dialogue",
        menuName = "Scriptable Objects/Dialogue Round"
    )]
    public sealed class DialogueRoundSO : ScriptableObject
    {
        [Header("Dialogue Turns")]
        [SerializeField]
        private List<DialogueTurn> dialogueTurnsList = new();

        public IReadOnlyList<DialogueTurn> DialogueTurnsList
            => dialogueTurnsList;
    }
}