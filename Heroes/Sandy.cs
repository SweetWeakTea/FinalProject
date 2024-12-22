using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalProject.Heroes
{
    public class Sandy : Hero
    {
        public Sandy(int _maxHealth, int _attackPower, int _magicalPower, int _criticalRate, int _criticalPower, int _defense, int _magicalResistance,
            int _magicPoints) : base(_maxHealth, _attackPower, _magicalPower, _criticalRate, _criticalPower, _defense, _magicalResistance, _magicPoints)
        {
            name = "沙悟净";

            xPosition = 0;
            yPosition = 3;
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
