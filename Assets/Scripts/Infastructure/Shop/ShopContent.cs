using System;
using Infastructure.Character;
using Infastructure.Data;
using Infastructure.Services.PersistentProgress;
using TMPro;
using UnityEngine;

namespace Infastructure.Shop
{
    public class ShopContent : MonoBehaviour, ISaveProgress
    {
        private SkinItem[] _skinItems;

        private CharacterTypeId _maleCharacter;
        private CharacterTypeId _femaleCharacter;

        private void Awake()
        {
            _skinItems = GetComponentsInChildren<SkinItem>();

            foreach (SkinItem skinItem in _skinItems)
                skinItem.SelectButton.onClick.AddListener(() => SelectSkin(skinItem.CharacterTypeId, skinItem.SexId));
        }

        private void OnDestroy()
        {
            foreach (SkinItem skinItem in _skinItems)
                skinItem.SelectButton.onClick.RemoveListener(() =>
                    SelectSkin(skinItem.CharacterTypeId, skinItem.SexId));
        }

        public void LoadProgress(PlayerProgress progress)
        {
            Debug.Log("Loaded");
            
            SelectSkin(progress.MaleSkin.CharacterTypeId, SexId.Male);
            SelectSkin(progress.FemaleSkin.CharacterTypeId, SexId.Female);

        }

        public void UpdateProgress(PlayerProgress progress)
        {
            Debug.Log("Updated");

            progress.MaleSkin.CharacterTypeId = _maleCharacter;
            progress.FemaleSkin.CharacterTypeId = _femaleCharacter;
        }

        private void SelectSkin(CharacterTypeId characterTypeId, SexId sexId)
        {
            SetAllSkinsDefaultBySex(sexId);
            SelectSkinBySex(characterTypeId, sexId);
        }

        private void SelectSkinBySex(CharacterTypeId characterTypeId, SexId sexId)
        {
            foreach (SkinItem skinItem in _skinItems)
            {
                if (skinItem.CharacterTypeId == characterTypeId)
                {
                    if (sexId == SexId.Male)
                        _maleCharacter = skinItem.CharacterTypeId;
                    else
                        _femaleCharacter = skinItem.CharacterTypeId;

                    skinItem.SelectButton.GetComponentInChildren<TextMeshProUGUI>().text = "Selected";
                    break;
                }
            }
        }

        private void SetAllSkinsDefaultBySex(SexId sexId)
        {
            foreach (SkinItem skinItem in _skinItems)
            {
                if (skinItem.SexId == sexId)
                    skinItem.SelectButton.GetComponentInChildren<TextMeshProUGUI>().text = "Select";
            }
        }
    }
}