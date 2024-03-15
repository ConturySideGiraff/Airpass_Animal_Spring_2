using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace CSG.Puzzle
{
    public class BoardHandler : MonoBehaviour
    {
        [SerializeField] private Text _subjectText;
        [Space] 
        [SerializeField] private PieceMoveHandler pieceMoveHandler;
        [SerializeField] private PieceFocusHandler pieceFocusHandler;
        [SerializeField] private BoardPointTouchHandler pointTouchHandler;
        [SerializeField] private BoardPointEnd _pointEnd;
        [Space]
        [SerializeField] private GameObject _animalFrame;
        [SerializeField] private GameObject _flowerFrame;
        [Space]
        [SerializeField] private List<BoardData> _boardDataList = new List<BoardData>(7);

        private static Dictionary<int, BoardRuntimeData> _boardRuntimeDataDic;

        public static int CurrentIndex { get; private set; }
        
        public void Init()
        {
            _boardDataList = _boardDataList.OrderBy(d => d.Index).ToList();

            _boardRuntimeDataDic = new Dictionary<int, BoardRuntimeData>();
            
            foreach (BoardData boardData in _boardDataList)
            {
                _boardRuntimeDataDic.Add(boardData.Index, new BoardRuntimeData(boardData));
            }
        }

        public void OnSelect(int index)
        {
            CurrentIndex = index;
            
            BoardRuntimeData data = _boardRuntimeDataDic[index];   

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
            BoardRuntimeData runtimeData = _boardRuntimeDataDic[index];
            
            pieceMoveHandler.Set(scriptData.Species, scriptData.ColorSpriteList, runtimeData);
            pieceFocusHandler.Set(scriptData.Species);
            
            pointTouchHandler.Set(scriptData.Species);
            _pointEnd.BoardImageSet(scriptData.Species, data.ScriptData.BlackSpriteList);
            
            gameObject.SetActive(true);
        }

        public static bool PieceCorrect(int index)
        {
            _boardRuntimeDataDic[CurrentIndex].correctIndexArr[index] = true;

            foreach (bool b in _boardRuntimeDataDic[CurrentIndex].correctIndexArr)
            {
                if (!b)
                {
                    return false;
                }
            }

            return true;
        }

        public static bool IsPieceCorrect(int index) => _boardRuntimeDataDic[CurrentIndex].correctIndexArr[index];

        public static bool BoardClear()
        {
            _boardRuntimeDataDic[CurrentIndex].IsClear = true;

            foreach (var boardRuntimeData in _boardRuntimeDataDic.Values)
            {
                if (!boardRuntimeData.IsClear)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
