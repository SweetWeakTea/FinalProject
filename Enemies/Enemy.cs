using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace FinalProject.Enemies
{
    public enum LootType
    {
        Experience,
        Coin,
    }

    public class Enemy : Entity
    {
        protected LootType loot;
        protected int amount;

        public Enemy(int _maxHealth, int _attackPower, int _magicalPower, int _criticalRate, int _criticalPower, int _defense, int _magicalResistance,
            LootType _loot, int _amount) : base(_maxHealth, _attackPower, _magicalPower, _criticalRate, _criticalPower, _defense, _magicalResistance)
        {
            loot = _loot;
            amount = _amount;
        }

        public void PrintAttributes(RichTextBox _richTextBox1) => _richTextBox1.AppendText($"{name} 生命值：{currentHealth} 攻击：{attackPower} 法术攻击：{magicalPower} 暴击率：{criticalRate} 暴伤：{criticalPower} 防御：{defense} 法抗：{magicalResistance}" + Environment.NewLine);

        public override void Die(RichTextBox _richTextBox2)
        {
            _richTextBox2.SelectionColor = Color.Green;
            _richTextBox2.AppendText("掉落 " + amount + " " + loot.ToString() + Environment.NewLine);
            _richTextBox2.SelectionColor = Color.Green;
            base.Die(_richTextBox2);
        }

        public override void DoAttack(Entity _target)
        {
            base.DoAttack(_target);
        }

        public override void DoMagicalAttack(Entity _target, Color _color)
        {
            base.DoMagicalAttack(_target, _color);
        }

        public override void TakeAttack(int _damage)
        {
            base.TakeAttack(_damage);
        }

        public override void TakeMagicalAttack(int _damage)
        {
            base.TakeMagicalAttack(_damage);
        }
    }
}
