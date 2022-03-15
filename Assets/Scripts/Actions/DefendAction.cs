using UnityEngine;

namespace Scenes.Actions
{
    public class DefendAction: Action
    {
        private int player;
        public Liaison liaison;


        public DefendAction(int player, Liaison liaison)
        {
            this.player = player;
            this.liaison = liaison;
            arrow = liaison.Create(liaison.attackArrowPrefab).GetComponent<SpriteRenderer>();
            tip = liaison.Create(liaison.attackTipPrefab).GetComponent<SpriteRenderer>();
        }
        
        public override void Specify(int x, int y)
        {
        }
        public override void Fail(GridController gc)
        {
            liaison.Delete(arrow.gameObject);
            liaison.Delete(tip.gameObject);
        }

        public override void Execute(Unit unit, GridController gc)
        {
            unit.isPrepared = 2;
        }
        public override void Render(int x, int y, int delay)
        {
            tip.color = Orchestrator.Instance.GetColor(delay + 1);
            Transform transform = tip.transform;
            transform.position = new Vector3(x, y);
        }
    }
}