using UnityEngine;

namespace Scenes.Actions
{
    public class DefendAction: Action
    {
        private int player;
        public Liaison liaison;
        
        public override string ToString()
        {
            return $"DefendAction (made by player {player})";
        }

        public DefendAction(int player, Liaison liaison)
        {
            this.player = player;
            this.liaison = liaison;
            arrow = liaison.Create(liaison.arrowPrefab).GetComponent<SpriteRenderer>();
            tip = liaison.Create(liaison.defendTipPrefab).GetComponent<SpriteRenderer>();
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
            tip.color = Orchestrator.Instance.GetOpaqueColor(delay + 1);
            Transform transform = tip.transform;
            transform.position = new Vector3(x, y);
        }
    }
}