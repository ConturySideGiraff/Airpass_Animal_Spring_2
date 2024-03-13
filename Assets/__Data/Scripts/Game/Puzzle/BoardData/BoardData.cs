using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CSG.Puzzle
{
    [CreateAssetMenu(menuName = "CSG/SO/Peace", fileName = "BoardData_")]
    public class BoardData : ScriptableObject
    {
        [SerializeField] private string codeName;
        [SerializeField] private int index;
        [SerializeField] private string enName;
        [SerializeField] private Species species;
        [SerializeField] private List<Sprite> blackSpriteList = new List<Sprite>();
        [SerializeField] private List<Sprite> colorSpriteList = new List<Sprite>();

        public string CodeName => codeName;
        public int Index => index;
        public Species Species => species;
        public List<Sprite> BlackSpriteList => blackSpriteList;

        public List<Sprite> ColorSpriteList => colorSpriteList;
    }
}