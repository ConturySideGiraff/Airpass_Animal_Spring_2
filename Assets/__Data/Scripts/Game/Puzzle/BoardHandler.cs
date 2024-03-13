using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace CSG.Puzzle
{
    public class BoardHandler : MonoBehaviour
    {
        [SerializeField] private Text _subjectText;
        [Space] 
        [SerializeField] private PeaceMoveHandler _peaceMoveHandler;
        [SerializeField] private BoardPointEnd _pointEnd;
        [Space]
        [SerializeField] private GameObject _animalFrame;
        [SerializeField] private GameObject _flowerFrame;
        [Space]
        [SerializeField] private List<BoardData> _boardDataList = new List<BoardData>(7);

        private Dictionary<int, BoardUnitData> _boardUnitDataDic;
        
        public void Init()
        {
            _boardDataList = _boardDataList.OrderBy(d => d.Index).ToList();

            _boardUnitDataDic = new Dictionary<int, BoardUnitData>();
            
            foreach (BoardData boardData in _boardDataList)
            {
                _boardUnitDataDic.Add(boardData.Index, new BoardUnitData(boardData));
            }
        }

        public void OnSelect(int index)
        {
            BoardUnitData data = _boardUnitDataDic[index];   

            if (ReferenceEquals(data, null))
            {
                return;
            }

            _subjectText.text = data.ScriptData.CodeName;

            switch (data.ScriptData.Species)
            {
                case Species.Animal:
                    _animalFrame.SetActive(true);
                    _flowerFrame.SetActive(false);
                    break;
                
                case Species.Flower:
                    _animalFrame.SetActive(false);
                    _flowerFrame.SetActive(true);
                    break;
            }

            BoardData scriptData = data.ScriptData;
            
            _peaceMoveHandler.Set(scriptData.Species, scriptData.ColorSpriteList);
            _pointEnd.BoardImageSet(scriptData.Species, data.ScriptData.BlackSpriteList);
            
            gameObject.SetActive(true);
        }
    }
}
