using UnityEngine;

namespace Scenes.Actions
{
    public class MoveAction : Action
    {
        private int x;
        private int y;
        private int player;
        public Liaison liaison;

        public MoveAction(int x, int y, int player, Liaison liaison)
        {
            this.x = x;
            this.y = y;
            this.player = player;
            this.liaison = liaison;
            arrow = liaison.Create(liaison.attackArrowPrefab).GetComponent<SpriteRenderer>();
            tip = liaison.Create(liaison.attackTipPrefab).GetComponent<SpriteRenderer>();
        }
        
        public override void Specify(int x, int y)
        {
            this.x = x;
            this.y = y;
            liaison.Delete(arrow.gameObject);
            liaison.Delete(tip.gameObject);
        }
        public override void Fail(GridController gc)
        {
         
            liaison.Delete(arrow.gameObject);
            liaison.Delete(tip.gameObject);   
        }

        public override void Execute(Unit unit, GridController gc)
        {
            Unit otherUnit = gc.GETUnit(x, y);
            if (otherUnit == null)
            {
                gc.MoveUnit(unit.x, unit.y, x, y);
                return;
            }

            if (otherUnit.owner == unit.owner)
            {
                Fail(gc);
            }
            liaison.Delete(arrow.gameObject);
            liaison.Delete(tip.gameObject);
        }
        public override void Render(int x, int y, int delay)
        {
            arrow.color = Orchestrator.Instance.GetColor(delay);
            tip.color = Orchestrator.Instance.GetColor(delay + 1);
            Vector2 change = new Vector2(this.x - x, this.y - y);
            float magnitude = change.magnitude;
            Quaternion rotation = Quaternion.Euler(change);
            Transform arrowTransform = arrow.transform;
            arrowTransform.localScale = new Vector3(magnitude, 1, 1);
            arrowTransform.rotation = rotation;
            arrowTransform.position = new Vector3(x, y);
            Transform tipTransform = tip.transform;
            tipTransform.position = new Vector3(this.x, this.y);
        }
    }
}