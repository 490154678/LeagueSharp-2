﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;

namespace ezEvade
{
    class SpecialEvadeSpell
    {
        private static Obj_AI_Hero myHero { get { return ObjectManager.Player; } }

        public static void LoadSpecialSpell(EvadeSpellData spellData)
        {
            if (spellData.spellName == "EkkoEAttack")
            {
                spellData.useSpellFunc = UseEkkoE2;
            }

            if (spellData.spellName == "EkkoR")
            {
                spellData.useSpellFunc = UseEkkoR;
            }
        }

        public static bool UseEkkoE2(EvadeSpellData evadeSpell)
        {
            if (myHero.HasBuff("ekkoeattackbuff"))
            {
                var posInfo = EvadeHelper.GetBestPositionTargetedDash(evadeSpell);
                if (posInfo != null && posInfo.target != null)
                {
                    EvadeCommand.Attack(evadeSpell, posInfo.target);
                    //DelayAction.Add(50, () => myHero.IssueOrder(GameObjectOrder.MoveTo, posInfo.position.To3D()));
                    return true;
                }
            }

            return false;
        }

        public static bool UseEkkoR(EvadeSpellData evadeSpell)
        {
            foreach (var obj in ObjectManager.Get<Obj_AI_Minion>())
            {
                if (obj != null && obj.IsValid && obj.Name == "Ekko" && obj.IsAlly)
                {
                    Vector2 blinkPos = obj.ServerPosition.To2D();
                    if (!blinkPos.CheckDangerousPos(10))
                    {
                        EvadeCommand.CastSpell(evadeSpell);
                        //DelayAction.Add(50, () => myHero.IssueOrder(GameObjectOrder.MoveTo, posInfo.position.To3D()));
                        return true;
                    }

                }
            }

            return false;
        }
    }
}
