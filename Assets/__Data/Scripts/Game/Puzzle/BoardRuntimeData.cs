using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace CSG.Puzzle
{
    [Serializable]
    public class BoardRuntimeData
    {
        public bool IsClear { get; set; }
        public int[] startPointIndexArr { get; private set; }
        public bool[] correctIndexArr { get; private set; }
        
        private BoardData _boardData;

        public BoardData ScriptData => _boardData;

        public BoardRuntimeData(BoardData boardData)
        {
            _boardData = boardData;
            
            correctIndexArr = new bool[6];

            switch (_boardData.Species)
            {
                case Species.Animal:
                    startPointIndexArr = Util.GetNoneOverlapNumbers(6, 6);
                    correctIndexArr = new bool[6];
                    break;

                case Species.Flower:
                    startPointIndexArr = Util.GetNoneOverlapNumbers(4, 4);
                    correctIndexArr = new bool[4];
                    break;
            }
        }
    }
}
