using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CSG.Puzzle
{
    [Serializable]
    public class BoardUnitData
    {
        private int[] _startPointIndexArr;
        private int[] _correctIndexArr;
        
        private BoardData _boardData;

        public BoardData ScriptData => _boardData;

        public BoardUnitData(BoardData boardData)
        {
            _boardData = boardData;
            
            _correctIndexArr = new int[6];

            switch (_boardData.Species)
            {
                case Species.Animal:
                    _startPointIndexArr = Util.GetNoneOverlapNumbers(6, 6);
                    break;

                case Species.Flower:
                    _startPointIndexArr = Util.GetNoneOverlapNumbers(4, 4);
                    _correctIndexArr = new int[4];
                    break;
            }

            for (int i = 0; i < _correctIndexArr.Length; i++)
            {
                _correctIndexArr[i] = i;
            }
        }
    }
}
