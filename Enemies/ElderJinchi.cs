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
    public class ElderJinchi : Enemy
    {
        public ElderJinchi(int _maxHealth, int _attackPower, int _magicalPower, int _criticalRate, int _criticalPower, int _defense, int _magicalResistance,
            LootType _loot, int _amount) : base(_maxHealth, _attackPower, _magicalPower, _criticalRate, _criticalPower, _defense, _magicalResistance, _loot, _amount)
        {
            name = "金池长老";

            xPosition = 14;
            yPosition = 1;
        }

        public override void Die(RichTextBox _richTextBox2)
        {
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
