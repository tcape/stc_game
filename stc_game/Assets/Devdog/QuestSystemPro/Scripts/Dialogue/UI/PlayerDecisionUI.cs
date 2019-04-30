﻿using UnityEngine;
using UnityEngine.UI;

namespace Devdog.QuestSystemPro.Dialogue.UI
{
    public class PlayerDecisionUI : MonoBehaviour
    {
        public Button button;
        public Text text;

        public PlayerDecision decision { get; protected set; }

        public void Repaint(PlayerDecision dec, Edge edge, bool canUse)
        {
            decision = dec;
            // original code: text.text = decision.option.message;
            text.text = "OK";
            button.interactable = canUse;
        }
    }
}
