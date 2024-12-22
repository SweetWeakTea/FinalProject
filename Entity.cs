using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalProject
{
    public class Entity : IAttackRangeCheck
    {
        public string name;

        public int xPosition;
        public int yPosition;

        protected int maxHealth; // 最大生命值
        public int currentHealth; // 当前生命值

        public int attackPower; // 物理攻击
        public int magicalPower; // 法术攻击
        public int criticalRate; // 暴击率
        public int criticalPower; // 暴伤

        public int defense; // 防御
        public int magicalResistance; // 法抗

        public bool isDead;

        public Random random = new Random();

        public Entity(int _maxHealth, int _attackPower, int _magicalPower, int _criticalRate, int _criticalPower, int _defense, int _magicalResistance)
        {
            maxHealth = _maxHealth;
            currentHealth = _maxHealth;
            attackPower = _attackPower;
            magicalPower = _magicalPower;
            criticalRate = _criticalRate;
            criticalPower = _criticalPower;
            defense = _defense;
            magicalResistance = _magicalResistance;
        }

        public virtual void Die(RichTextBox _richTextBox2)
        {
            isDead = true;
            _richTextBox2.AppendText(this.name + "战败了" + Environment.NewLine);
        }

        public virtual void DoAttack(Entity _target)
        {
            int number = random.Next(0, 100);

            if (number < criticalRate)
                _target.TakeAttack((int)(attackPower * criticalPower * .01f));
            else
                _target.TakeAttack(attackPower);
        }

        public virtual void DoMagicalAttack(Entity _target, Color _color)
        {
            Form2.instance.richTextBox2.SelectionColor = _color;
            _target.TakeMagicalAttack(magicalPower);
        }

        public virtual void TakeAttack(int _damage)
        {
            int finalDamage = _damage - defense <= 0 ? 1 : _damage - defense;
            currentHealth -= finalDamage;
            Form2.instance.richTextBox2.AppendText(name + "受到了" + finalDamage + "点物理伤害，还剩下" + (currentHealth >= 0 ? currentHealth : 0) + "点生命" + Environment.NewLine);

            if (currentHealth <= 0)
                Die(Form2.instance.richTextBox2);
        }

        public virtual void TakeMagicalAttack(int _damage)
        {
            int finalDamage = _damage - magicalResistance <= 0 ? 1 : _damage - magicalResistance;
            currentHealth -= finalDamage;
            Form2.instance.richTextBox2.AppendText(name + "受到了" + finalDamage + "点法术伤害，还剩下" + currentHealth + "点生命" + Environment.NewLine);

            if (currentHealth <= 0)
                Die(Form2.instance.richTextBox2);
        }

        public bool ADRange(Entity _target)
        {
            return Math.Abs(this.xPosition - _target.xPosition) + Math.Abs(this.yPosition - _target.yPosition) <= 1;
        }

        public bool APRange(Entity _target)
        {
            int dx = Math.Abs(this.xPosition - _target.xPosition);
            int dy = Math.Abs(this.yPosition - _target.yPosition);

            // 目标在攻击者的周围8格内
            return (dx == 1 && dy == 0) || (dx == 0 && dy == 1) || (dx == 1 && dy == 1);
        }
    }
}
