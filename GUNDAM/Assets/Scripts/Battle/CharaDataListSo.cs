using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace app {
    [CreateAssetMenu(
  fileName = "CharaListData",
  menuName = "ScriptableObject/CharaListData",
  order = 0)
]
    public class CharaDataListSo : ScriptableObject
    {
        [System.Serializable]
        public class CharaData
        {
            public CHARA_ID ID = CHARA_ID.NONE;
            public GameObject Prefab = null;
        }

        public List<CharaData> CharacterDatas = new List<CharaData>();
    }
}
