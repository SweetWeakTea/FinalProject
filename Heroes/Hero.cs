using System;
using System.Drawing;
using System.Windows.Forms;

namespace FinalProject.Heroes
{
    public class Hero : Entity
    {
        public int magicPoints;
        public int shield;

        public delegate void DoDamageHandler(int _value, RichTextBox _richTextBox);
        public event DoDamageHandler DoDamage;

        public Hero(int _maxHealth, int _attackPower, int _magicalPower, int _criticalRate, int _criticalPower, int _defense, int _magicalResistance,
            int _magicPoints) : base(_maxHealth, _attackPower, _magicalPower, _criticalRate, _criticalPower, _defense, _magicalResistance)
        {
            magicPoints = _magicPoints;
            DoDamage += DoLifeSteal; // 攻击吸血
            DoDamage += DoShield; // 攻击获得护盾
        }

        public void PrintAttributes(RichTextBox _richTextBox1) => _richTextBox1.AppendText($"{name} 生命值：{currentHealth} 攻击：{attackPower} 法术攻击：{magicalPower} 暴击率：{criticalRate} 暴伤：{criticalPower} 防御：{defense} 法抗：{magicalResistance} 法力：{magicPoints} 护盾：{shield}" + Environment.NewLine);

        public virtual void DoShield(int _shield, RichTextBox _richTextBox2)
        {
            if (magicPoints >= 20)
            {
                magicPoints -= 20;
                _richTextBox2.SelectionColor = Color.Red;
                _richTextBox2.AppendText(name + "消耗了20点法力" + Environment.NewLine);
            }
            else
                return;

            shield += _shield;
            _richTextBox2.SelectionColor = Color.Green;
            _richTextBox2.AppendText(name + "获得了20点护盾" + Environment.NewLine);
        }

        public virtual void DoLifeSteal(int _heal, RichTextBox _richTextBox2)
        {
            currentHealth += _heal;
            _richTextBox2.SelectionColor = Color.Green;
            _richTextBox2.AppendText(name + "恢复了" + _heal + "点生命" + Environment.NewLine);
        }

        public override void Die(RichTextBox _richTextBox2)
        {
            base.Die(_richTextBox2);
        }

        public override void DoAttack(Entity _target)
        {
            base.DoAttack(_target);

            DoDamage?.Invoke((int)(maxHealth * .2f), Form2.instance.richTextBox2);
        }

        public override void DoMagicalAttack(Entity _target, Color _color)
        {
            if (magicPoints >= 20)
            {
                magicPoints -= 20;
                Form2.instance.richTextBox2.SelectionColor = Color.Red;
                Form2.instance.richTextBox2.AppendText(name + "消耗了20点法力" + Environment.NewLine);
            }
            else
                return;

            base.DoMagicalAttack(_target, _color);
        }

        public override void TakeAttack(int _damage)
        {
            int finalDamage = _damage - defense <= 0 ? 1 : _damage - defense;
            if (shield > finalDamage)
            {
                shield -= finalDamage;
                Form2.instance.richTextBox2.AppendText(name + "还剩下" + shield + "点护盾" + Environment.NewLine);
                return;
            }
            else if (shield > 0 && shield <= finalDamage)
            {
                shield = 0;
                Form2.instance.richTextBox2.AppendText(name + "的护盾被击破" + Environment.NewLine);
                return;
            }

            base.TakeAttack(_damage);
        }

        public override void TakeMagicalAttack(int _damage)
        {
            base.TakeMagicalAttack(_damage);
        }
    }
}
