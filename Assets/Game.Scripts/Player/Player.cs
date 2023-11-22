using System;
using Cysharp.Threading.Tasks;
using Game.Scripts.Common;
using Unity.Mathematics;
using UnityEngine;

namespace QtNameSpace
{
    public class Player : ABasePresenter, IHeart
    {
        [SerializeField] private HurtAnim hurtAnimPrefab;
        
        
        public override void InitModel()
        {
            var statModel = GameConfig.CharacterStatContainer.Selected.At(GameConfig.Instance.SelectedLevel);
            var skinModel = GameConfig.CharacterSkinContainer.SkinOfHeroSelected;
            var skillModel = GameConfig.CharacterSkillContainer.Selected.skillInfo;
            
            GameModel.Instance.AddOrUpdateModel(new PlayerInfo
            {
                stat = statModel,
                skin = skinModel,
                skill = skillModel,
            });
            GetModel<PlayerInfo>().Restore();
        }

        public void TakeDamage(int damage)
        {
            Bounce();
            
            var model = GetModel<PlayerInfo>().stat;
            model.health.Decrement(damage);

            if (hurtAnimPrefab != null)
            {
                var anim =
                    Instantiate(hurtAnimPrefab, transform.position, quaternion.identity)
                        .GetComponent<HurtAnim>();
                anim.SetDamage(damage);
            }

            if (model.Alive() is false)
            {
                Release();
                ObserverEvent.EmitEvent(GameEventID.PlayerDead, 1f);
                Destroy(gameObject);
            }
            else
                ObserverEvent.EmitEvent(GameEventID.PlayerHurt);
        }

        private void Awake()
        {
            InitModel();
            elements.ForEach(item => item.Init());
            elements.ForEach(item => item.UpdateView());
        }

        private void Bounce()
        {
            var isLeft = transform.rotation.eulerAngles.y != 0;
            var rig2D = GetComponent<Rigidbody2D>();
            rig2D.AddForce(isLeft ? Vector2.right : Vector2.left, ForceMode2D.Impulse);
        }
    }
}