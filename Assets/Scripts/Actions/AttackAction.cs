using System;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

namespace Scenes.Actions
{
    public class AttackAction : Action
    {
        private int x;
        private int y;
        private int player;
        public Liaison liaison;
        
        public override string ToString()
        {
            return $"AttackAction (at ({x}, {y}), made by player {player})";
        }

        public AttackAction(int x, int y, int player, Liaison liaison)
        {
            this.x = x;
            this.y = y;
            this.player = player;
            this.liaison = liaison;
            arrow = liaison.Create(liaison.arrowPrefab).GetComponent<SpriteRenderer>();
            tip = liaison.Create(liaison.attackTipPrefab).GetComponent<SpriteRenderer>();
            
        }
        public override void Fail(GridController gc)
        {
            liaison.Delete(arrow.gameObject);
            liaison.Delete(tip.gameObject);
        }

        public override void Specify(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override void Execute(Unit unit, GridController gc)
        {
            unit.battleReady = true;
            Unit otherUnit = gc.GETUnit(x, y);
            if (otherUnit == null)
            {
                unit.currentStamina = 0; // penalty for attacking at the wrong time
                gc.MoveUnit(unit.x, unit.y, x, y);
                liaison.Delete(arrow.gameObject);
                liaison.Delete(tip.gameObject);
                return;
            }

            if (otherUnit.owner == unit.owner)
            {
                Fail(gc);
                return;
            }

            if (unit.CanAttack(x, y))
            {
                unit.Attack(x, y, otherUnit);
            }
            liaison.Delete(arrow.gameObject);
            liaison.Delete(tip.gameObject);
        }

        public override void Render(int x, int y, int delay)
        {
            arrow.color = Orchestrator.Instance.GetOpaqueColor(delay);
            tip.color = Orchestrator.Instance.GetOpaqueColor(delay + 1);
            float magnitude = new Vector2(this.x - x, this.y - y).magnitude;
            Transform arrowTransform = arrow.transform;
            arrowTransform.localScale = new Vector3(magnitude, 0.1f, 1);
            arrowTransform.position = new Vector3((x + this.x) / 2f, (y + this.y) / 2f);
            Vector3 angle = Vector3.forward * (Mathf.Atan2(this.y - y, this.x - x) * 180f / (float) Math.PI);
            arrowTransform.eulerAngles = angle;
            Transform tipTransform = tip.transform;
            tipTransform.position = new Vector3(this.x, this.y);
            angle.z -= 90;
            tipTransform.eulerAngles = angle;
            tipTransform.localScale = new Vector3(0.5f, 0.5f);
        }
    }
}