﻿using Assets.Scripts.CharacterBehavior.Combat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.MonoBehaviors
{
    public class CharacterStatsSaver : Saver
    {
        public Stats characterStats;
        private AbilityManager manager;


        private void Awake()
        {
            characterStats = GetComponent<CharacterStats>().stats;
            manager = GetComponent<AbilityManager>();
            if (gameObject.CompareTag("Player"))
            {
                uniqueIdentifier = "myStats";
            }
            key = SetKey();
        }

        public override void Load()
        {
            if (SceneController.Instance.previousSceneName.Equals(GameStrings.Scenes.TownScene) && SceneController.Instance.currentSceneName.Equals(GameStrings.Scenes.TownScene))
            {
                return;
            }
            // loading from persistent scene's GameCharacter
            characterStats = PersistentScene.Instance.GameCharacter.Stats;

            //Stats stats = new Stats();
            //if (saveData.Load(key, ref stats))
            //{
            //    characterStats = stats;
            //}
        }

        public override void Save()
        {
            manager = GetComponent<AbilityManager>();
            manager.RemoveAllEffects();

            characterStats = GetComponent<CharacterStats>().stats;
            //saveData.Save(key, characterStats);

            // Saving to persistent scene's GameCharacter
            PersistentScene.Instance.SaveGameCharacterStats(characterStats);
        }

        protected override string SetKey()
        {
            return uniqueIdentifier;
        }
    }
}
