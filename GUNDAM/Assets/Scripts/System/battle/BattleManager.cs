using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace app.Battle
{
    public class BattleManager : SingletonBehaviour<BattleManager>
    {
        public class Team
        {
            public uint Cost = 0;
        }

        private Team[] Teams = new Team[2] { new Team(), new Team() };

        public CharaDataListSo CharaDataListSo = null;

        public void LoadCharacterDatas(List<CHARA_ID> chara_IDs)
        {
            foreach (var id in chara_IDs)
            {
                var charaData = CharaDataListSo.CharacterDatas.Find(x => x.ID == id);
                if (charaData == null) continue;

                Instantiate(charaData.Prefab);
            }
        }

        public void SetUpBattle()
        {
            Teams[0].Cost = 6000;
            Teams[1].Cost = 6000;
            LoadCharacterDatas(new List<CHARA_ID>() { CHARA_ID.CHARA001 });
        }

        public bool IsBattleFinish()
        {
            foreach(var team in Teams)
            {
                if (team.Cost == 0)
                {
                    return true;
                }
            }

            return false;
        }
        #region Behaviour継承

        //protected override void doAwake()
        //{
        //
        //}

        //protected override void doStart()
        //{
        //
        //}

        //protected override void doUpdate()
        //{
        //
        //}

        #endregion

        #region 継承禁止
        // 使用禁止。doAwake継承
        protected override void Awake()
        {
            base.Awake(); 
        }

        // 使用禁止。doStart継承
        protected override void Start()
        {
            base.Start();
        }

        // 使用禁止。doUpdate継承
        protected override void Update()
        {
            base.Update();
        }
        #endregion
    }
}